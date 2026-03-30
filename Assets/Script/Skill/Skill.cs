using UnityEngine;
using System.Collections;

public abstract class Skill : MonoBehaviour
{

    protected byte position_query;

    public abstract int Get_Value();

    public abstract bool Is_On_Ball_Skill();

    public abstract string Get_Name();

    public abstract byte Get_Position_Query();

    public abstract string Get_Animation_Type();

    public bool Get_Position_By_Index(int index){
        byte position_query = Get_Position_Query(); 
        while (index > 0)
        {
            position_query /= 2;
            index--;
        }
        return (position_query % 2) == 1;
    }

    public static byte Make_Position_Query(params bool[] query)
    {
        byte result = 0;

        for (int i = 0; i < query.Length; i++)
        {
            byte query_int= (byte)((query[i] == true) ? 1 : 0);
            result += (byte)(query_int << i);
        }
        return result;
    }

    protected Basketball_Game_Manager gm => Basketball_Game_Manager.Get_Game_Manager();
    
    protected Skill_Database database => GetComponent<Skill_Database>();


    public abstract Player_Action Get_Action();

    public abstract IEnumerator Act();

    public bool Can_Skill_Use(Basketball_Player player)
    {
        bool is_turn_enough = gm.current_turn >= Get_Value();
        bool is_position_right = Get_Position_By_Index(gm.Get_Index_Of_Player(player));
        bool is_same_situation = Is_On_Ball_Skill() == player.On_Ball();

        return is_turn_enough && is_position_right && is_same_situation;

    }
}


using System.Collections.Generic;
using UnityEngine;

public class Skill_Database : MonoBehaviour
{
    public Basketball_Game_Manager gm;
    private static Skill_Database script;
    public static Skill_Database Get_Database() => script;

    public int Get_Value_Of_Skill(Player_Action action)
    {
        Skill skill = Get_Skill(action);
       
        return skill.Get_Value();
    }

    public string Get_Name_Of_Skill(Player_Action action)
    {
        Skill skill = Get_Skill(action);
        
        return skill.Get_Name();
    }

    
    private void Awake()
    {
        script = this;
    }


    public void Act_And_Calculate_Turn(Player_Action action)
    {
        Skill[] skills = GetComponents<Skill>();
        ref int turn = ref gm.current_turn;

        foreach (var skill in skills)
        {
            if (action == skill.Get_Action() && turn >= Get_Value_Of_Skill(action))
            {
                turn -= Get_Value_Of_Skill(action);
                gm.Delete_Player_Card_UI();
                StartCoroutine(skill.Act());
                return;
            }
        }
    }

    public Skill Get_Skill(Player_Action action)
    {
        Skill[] skills = GetComponents<Skill>();
        foreach (var skill in skills)
        {
            if (skill.Get_Action() == action)
                return skill;
        }
        return null;
    }


}

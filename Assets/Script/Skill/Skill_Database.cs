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
            if (Can_Act_Action(action , turn , skill))
            {
                Act_Skill(skill);
                
                return;
            }
        }
    }

    private void Act_Skill(Skill skill)
    {
        ref int turn = ref gm.current_turn;
        
        turn -= skill.Get_Value();
        gm.Delete_Player_Card_UI();
        StartCoroutine(skill.Act());
    }
    

    private bool Can_Act_Action(Player_Action action, int turn , Skill skill)
    {
        Basketball_Player skill_player = gm.Get_Skill_Player();
        bool is_same_action = action == skill.Get_Action();
        bool is_turn_enough = turn >= skill.Get_Value();
        bool is_position_right = skill.Get_Position_By_Index(gm.Get_Index_Of_Player(skill_player));

        return is_same_action && is_turn_enough && is_position_right;
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

    public bool Can_Act_Action(Player_Action action)
    {
        Skill skill = Get_Skill(action);
        int turn = gm.current_turn;
        return Can_Act_Action(action, turn, skill);

    }

}

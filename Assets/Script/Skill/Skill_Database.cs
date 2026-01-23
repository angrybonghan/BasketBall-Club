using System.Collections.Generic;
using UnityEngine;

public class Skill_Database : MonoBehaviour
{
    public Basketball_Game_Manager gm;
    private static Skill_Database script;
    public static Skill_Database Get_Database() => script;

    public List<KeyValuePair<Player_Action, int>> skills_values = new()
    {
        new(Player_Action.Pass , 3),
        new(Player_Action.Shoot , 2),
        new(Player_Action.Move , 1)
    };

    public List<KeyValuePair<Player_Action, string>> skills_names = new()
    {
        new(Player_Action.Pass , "pass"),
        new(Player_Action.Shoot , "shoot"),
        new(Player_Action.Move , "move")
    };

    public int Get_Value_Of_Skill(Player_Action action)
    {
        foreach (var skill_value_pair in skills_values)
        {
            if (action == skill_value_pair.Key)
                return skill_value_pair.Value;
        }
        return -1;
    }

    public string Get_Name_Of_Skill(Player_Action action)
    {
        foreach (var skill_name_pair in skills_names)
        {
            if (action == skill_name_pair.Key)
                return skill_name_pair.Value;
        }
        return "";
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



}

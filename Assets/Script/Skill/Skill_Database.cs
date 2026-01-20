using System.Collections.Generic;
using UnityEngine;

public class Skill_Database : MonoBehaviour
{
    public Basketball_Game_Manager game_manager;
    private static Skill_Database script;
    public static Skill_Database Get_Database() => script;
    public List<KeyValuePair<Player_Action, int>> skills_values = new()
    {
        new(Player_Action.Pass , 3),
        new(Player_Action.Shoot , 2),
        new(Player_Action.Off_Ball_Move , 1)
    };

    public int Get_Value_Of_Skill(Player_Action action)
    {
        foreach (var skill_value in skills_values)
        {
            if (action == skill_value.Key)
                return skill_value.Value;
        }
        return -1;
    }


    
    private void Awake()
    {
        script = this;
    }


    public void Act_And_Calculate_Turn(Player_Action action)
    {
        Skill[] skills = GetComponents<Skill>();
        ref int turn = ref game_manager.current_turn;

        foreach (var skill in skills)
        {
            if (action == skill.Get_Action() && turn >= Get_Value_Of_Skill(action))
            {
                StartCoroutine(skill.Act());
                turn -= Get_Value_Of_Skill(action);
                return;
            }
        }

    }



}

using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public partial class Basketball_Player : MonoBehaviour
{
    private Basketball_Game_Manager gm => Basketball_Game_Manager.Get_Game_Manager();
    public Team team;
    public List<Skill_Weight> skill_weights;
    private List<int> other_player_index_list;

    public void Set_Name(string name)
    {
        Show_Player_Name text_box = GetComponent<Show_Player_Name>();
        text_box.Set_Text(name);
    }

    private void Set_Color()
    {
        if (select_mode)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
            return;
        }

        if (on_ball)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
            return;
        }

        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }
    private void Start()
    {
        Set_Other_Player_List();
    }

    private void Update()
    {
        Set_Color();
    }

    private void OnMouseUp()
    {
        if (Is_Defender() || team.is_ai)
            return;

        if (select_mode)
        {
            gm.Set_Selected_Player(this);
            return;
        }

        if (gm.Get_Select_Mode())
            return;

        gm.Delete_Player_Card_UI();
        Show_Player_Card();
    }

    private void Set_Other_Player_List()
    {
        other_player_index_list = new();
        foreach (var player in team.players)
        {
            if (player == this)
                continue;
            int player_index = gm.Get_Index_Of_Player(player);
            other_player_index_list.Add(player_index);
        }

    }

    public IEnumerator Action_By_Ai()
    {
        Skill_Database skill_database = Skill_Database.Get_Database();
        Skill_Weight skill_weight = Get_Skill_Weight_By_Random();
        int target_player_index = 0;
        if (skill_weight == null)
        {
            Debug.Log("no_weight");
            yield return new WaitForSeconds(1);
            gm.Next_Round();

            yield break;
        }

        if (skill_weight.target_player_index == -1)
        {
            int random_value = UnityEngine.Random.Range(0, other_player_index_list.Count);
            target_player_index = other_player_index_list[random_value];
        }
        else
            target_player_index = skill_weight.target_player_index;

        yield return StartCoroutine(skill_database.Act_Skill_By_Ai(skill_weight.player_action, this, target_player_index));

    }

    private Skill_Weight Get_Skill_Weight_By_Random()
    {
        int max_weight = 0;
        List<Skill_Weight> available_skill_weights = Get_Available_Skill_Weights();
        foreach (var skill_weight in available_skill_weights)
        {
            max_weight += skill_weight.weight;
        }

        int random_value = UnityEngine.Random.Range(0, max_weight);

        foreach (var skill_weight in available_skill_weights)
        {
            if (random_value < skill_weight.weight)
            {
                return skill_weight;
            }
            random_value -= skill_weight.weight;
        }

        return null;
    }

    private List<Skill_Weight> Get_Available_Skill_Weights()
    {
        Skill_Database skill_database = Skill_Database.Get_Database();
        List<Skill_Weight> available_skill_weights = new List<Skill_Weight>();

        foreach (var skill_weight in skill_weights)
        {
            Skill skill = skill_database.Get_Skill(skill_weight.player_action);
            if (skill.Can_Skill_Use(this) == false)
                continue;

            available_skill_weights.Add(skill_weight);
        }
        return available_skill_weights;
    }




}

[Serializable]
public class Skill_Weight
{
    public Player_Action player_action;
    public int weight;
    public int target_player_index;

}





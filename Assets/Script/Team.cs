using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

[Serializable]
public class Team
{
    public List<Basketball_Player> players;
    public List<Basketball_Player> basic_player_position;
    public Basketball_Game_Manager gm => Basketball_Game_Manager.Get_Game_Manager();
    public int score;
    public bool is_attack;
    public bool is_ai;
    public GameObject team_object;
    public List<Order> order_list;
    [SerializeField] List<GameObject> players_prefeb;



    public void Set_Attack()
    {
        foreach (var player in players)
        {
            player.Set_Attacker(true);
        }
        is_attack = true;

    }

    public void Set_Defender()
    {
        foreach (var player in players)
        {
            player.Set_Attacker(false);
        }
        is_attack = false;

    }
    
    public List<Basketball_Player> Set_Player_Prefeb()
    {
        List<Basketball_Player> result = new();
        for (int i = 0; i < players_prefeb.Count; i++)
        {
            GameObject player_gameObject = GameObject.Instantiate(players_prefeb[i], team_object.transform);
            Basketball_Player player = player_gameObject.GetComponent<Basketball_Player>();

            Set_New_Player(player, i);

            result.Add(player);
            players.Add(player);
            basic_player_position.Add(player);
        }
        return result;
    }

    private void Set_New_Player(Basketball_Player player, int i)
    {
        player.Set_Name((i + 1).ToString());
        player.transform.localPosition = new Vector2((i-2)*2.5f , 0);
        player.team = this;
    }

    public void Update_Player_Display()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.localPosition = new Vector2((i - 2) * 2.5f, 0);
        }
    }

    public IEnumerator Act_Order_List()
    {

        while (true)
        {
            Basketball_Player player = gm.Get_On_Ball_Player();


            yield return gm.StartCoroutine(player.Action_By_Ai());
            yield return new WaitForSeconds(0.3f);

            if (is_attack == false)
                break;
        }

        Debug.Log("finish");

    }

    private void Act_Skill_By_Order(Order order)
    {
        Basketball_Player player = gm.Get_Player_By_Index(order.player_index, this);
        if (player.On_Ball())
        {

        }

    }

}



public class Order
{
    public int player_index;
    public int player_action_index;
    public int follower_index;

}


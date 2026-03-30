using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class Team
{
    public List<Basketball_Player> players;
    public List<Basketball_Player> basic_player_position;
    public int score;
    public bool is_attack;
    public GameObject team_object;
    [SerializeField] List<GameObject> players_prefeb;



    public void Set_Attack()
    {
        foreach (var player in players)
        {
            player.Set_Attacker(true);
        }

    }

    public void Set_Defender()
    {
        foreach (var player in players)
        {
            player.Set_Attacker(false);
        }

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

}

using UnityEngine;
using System.Collections.Generic;

public partial class Basketball_Game_Manager : MonoBehaviour
{

    public List<Basketball_Player> Get_Players_By_Range(Team team ,int start, int end)
    {
        List<Basketball_Player> result = new();
        for (int i = start; i <= end; i++)
        {
            result.Add(team.players[i]);
        }

        return result;
    }

    public List<Basketball_Player> Get_Players(Team team , Player_Filter player_filter)
    {

        List<Basketball_Player> result = new();

        foreach (var player in team.players)
        {
            if (player_filter(player) == false)
                continue;
            result.Add(player);
        }

        return result;


    }

    public List<Basketball_Player> Get_Off_Ball_Players_In_Attack_Team()
    {
        return Get_Players(attack_team ,(player) => player.Off_Ball());
    }

    public Basketball_Player Get_On_Ball_Player()
    {
        foreach (var player in attack_team.players)
        {
            if (player.On_Ball() == true)
                return player;
        }
        return null;
    }
    
    public int Get_Index_Of_Player(Basketball_Player player)
    {
        Team team = player.team;
        for(int i = 0; i < Get_Attack_Player_Count(); i++)
        {
            if (player == team.players[i])
                return i;
        }

        return -1;
    }

    public List<Basketball_Player> Get_Near_Players(Basketball_Player player , int range)
    {
        Team team = player.team;
        List<Basketball_Player> result = new();
        int main_player_index = Get_Index_Of_Player(player);

       List<Basketball_Player> left_side_players = Get_Left_Side_Player(team ,main_player_index , range);
       List<Basketball_Player> right_side_players = Get_Right_Side_Player(team ,main_player_index, range);


        result.AddRange(left_side_players);
        result.AddRange(right_side_players);

        return result;
    }

    private List<Basketball_Player> Get_Left_Side_Player(Team team, int main_player_index, int range)
    {
        int start = Mathf.Max(0, main_player_index - range);
        int end = Mathf.Max(-1, main_player_index - 1);

        List<Basketball_Player> left_side_players = Get_Players_By_Range(team , start, end);
        return left_side_players;
    }

    private List<Basketball_Player> Get_Right_Side_Player(Team team, int main_player_index, int range)
    {
        int max_index = Get_Attack_Player_Count();

        int start = Mathf.Min(max_index, main_player_index + 1);
        int end = Mathf.Min(max_index -1, main_player_index + range);

        List<Basketball_Player> right_side_players = Get_Players_By_Range(team, start, end);

        return right_side_players;
    }


}

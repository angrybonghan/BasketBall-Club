using UnityEngine;
using System.Collections.Generic;

public partial class Basketball_Game_Manager : MonoBehaviour
{

    public List<Basketball_Player> Get_Players_By_Range(int start, int end)
    {
        List<Basketball_Player> result = new();
        for (int i = start; i <= end; i++)
        {
            result.Add(attack_players[i]);
        }

        return result;
    }

    public List<Basketball_Player> Get_Players(Player_Filter player_filter)
    {

        List<Basketball_Player> result = new();

        foreach (var player in attack_players)
        {
            if (player_filter(player) == false)
                continue;
            result.Add(player);
        }

        return result;


    }

    public List<Basketball_Player> Get_Off_Ball_Players()
    {
        return Get_Players((player) => player.On_Ball() == false);
    }

    public Basketball_Player Get_On_Ball_Player()
    {
        foreach (var player in attack_players)
        {
            if (player.On_Ball() == true)
                return player;
        }
        return null;
    }
    
    public int Get_Index_Of_Player(Basketball_Player player)
    {
        for(int i = 0; i < Get_Attack_Player_Count(); i++)
        {
            if (player == attack_players[i])
                return i;
        }

        return -1;
    }

    public List<Basketball_Player> Get_Near_Players(Basketball_Player player , int range)
    {
        List<Basketball_Player> result = new();
        int main_player_index = Get_Index_Of_Player(player);

       List<Basketball_Player> left_side_players = Get_Left_Side_Player(main_player_index , range);
       List<Basketball_Player> right_side_players = Get_Right_Side_Player(main_player_index, range);


        result.AddRange(left_side_players);
        result.AddRange(right_side_players);

        return result;
    }

    private List<Basketball_Player> Get_Left_Side_Player(int main_player_index, int range)
    {
        int left_start = Mathf.Max(0, main_player_index - range);
        int left_end = Mathf.Max(-1, main_player_index - 1);
        List<Basketball_Player> left_side_players = Get_Players_By_Range(left_start, left_end);
        return left_side_players;
    }

    private List<Basketball_Player> Get_Right_Side_Player(int main_player_index, int range)
    {
        int max_index = Get_Attack_Player_Count();

        int right_start = Mathf.Min(max_index, main_player_index + 1);
        int right_end = Mathf.Min(max_index -1, main_player_index + range);
        List<Basketball_Player> right_side_players = Get_Players_By_Range(right_start, right_end);

        return right_side_players;
    }


}

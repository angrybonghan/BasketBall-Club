using UnityEngine;

public partial class Basketball_Game_Manager : MonoBehaviour
{
    public void Move(Basketball_Player player , int move_range)
    {
        int player_index = Get_Index_Of_Player(player);
        move_range = Mathf.Min(attack_players.Count - player_index - 1, move_range);
        move_range = Mathf.Max(-player_index, move_range);

        if (move_range > 0)
            Move_Right(player, move_range);
        else
            Move_Left(player, -move_range);

        Update_Player_Display();
        
    }

    private void Move_Right(Basketball_Player player, int move_range)
    {
        int player_index = Get_Index_Of_Player(player);
        for (int i = 0; i < move_range; i++)
        {
            Move_Player_Right(player_index + i);
        }
        attack_players[player_index + move_range] = player;
        attack_players[player_index + move_range].Move_Animation();
    }

    private void Move_Player_Right(int player_index)
    {
        attack_players[player_index] = attack_players[player_index + 1];
        attack_players[player_index].Move_Animation();
    }

    private void Move_Left(Basketball_Player player, int move_range)
    {
        int player_index = Get_Index_Of_Player(player);

        for (int i = 0; i < move_range; i++)
        {
            Move_Player_Left(player_index -i);
        }
        attack_players[player_index - move_range] = player;
        attack_players[player_index - move_range].Move_Animation();
    }

    private void Move_Player_Left(int player_index)
    {
        attack_players[player_index ] = attack_players[player_index - 1];
        attack_players[player_index].Move_Animation();
    }


}

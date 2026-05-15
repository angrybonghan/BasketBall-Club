using System.Collections;
using UnityEngine;

public partial class Basketball_Game_Manager : MonoBehaviour
{
    public IEnumerator Move(Basketball_Player player , int move_range)
    {
        Team team = player.team;
        int player_index = Get_Index_Of_Player(player);
        move_range = Mathf.Min(team.players.Count - player_index - 1, move_range);
        move_range = Mathf.Max(-player_index, move_range);

        if (move_range > 0)
            Move_Right(player, move_range);
        else
            Move_Left(player, -move_range);

        yield return StartCoroutine(team.Update_Player_Display());
        
    }

    private void Move_Right(Basketball_Player player, int move_range)
    {
        Team team = player.team;
        int player_index = Get_Index_Of_Player(player);
        for (int i = 0; i < move_range; i++)
        {
            Move_Player_Right(team,player_index + i);
        }
        team.players[player_index + move_range] = player;
        team.players[player_index + move_range].Move_Animation();
    }

    private void Move_Player_Right(Team team , int player_index)
    {
        team.players[player_index] = team.players[player_index + 1];
        team.players[player_index].Move_Animation();
    }

    private void Move_Left(Basketball_Player player, int move_range)
    {
        Team team = player.team;
        int player_index = Get_Index_Of_Player(player);

        for (int i = 0; i < move_range; i++)
        {
            Move_Player_Left(team ,player_index -i);
        }
        team.players[player_index - move_range] = player;
        team.players[player_index - move_range].Move_Animation();
    }

    private void Move_Player_Left(Team team,int player_index)
    {
        team.players[player_index ] = team.players[player_index - 1];
        team.players[player_index].Move_Animation();
    }


}

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Pass_Skill : Skill
{
    public override Player_Action Get_Action() => Player_Action.Pass;

    public override IEnumerator Act()
    {
        Basketball_Game_Manager gm = database.game_manager;
        Basketball_Player on_ball_player = gm.Get_On_Ball_Player();
        List<Basketball_Player> off_ball_players = gm.Get_Off_Ball_Players();


        Basketball_Player target_player = null;

        yield return StartCoroutine(gm.Select_Player(off_ball_players , (result) =>
        {
            target_player = result;
        }));

        gm.Pass(on_ball_player, target_player);
        yield return null;

    }


    private Basketball_Player Get_Random_Target_Player(List<Basketball_Player> off_ball_players)
    {

        int max_index = off_ball_players.Count;
        int random_index = Random.Range(0, max_index);

        return off_ball_players[random_index];
    }


}

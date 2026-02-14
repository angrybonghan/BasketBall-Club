using UnityEngine;
using System.Collections;

public class Layup_Skill : Skill
{
    public override string Get_Name() => "layup";

    public override Player_Action Get_Action() => Player_Action.Layup;

    public override int Get_Value() => 5;

    public override byte Get_Position_Query()
    {
        if (position_query == 0)
            position_query = Make_Position_Query(false, false, true, true, true);

        return position_query;
    }

    public override IEnumerator Act()
    {
        Basketball_Player on_ball_player = gm.Get_On_Ball_Player();
        int shoot_score = 2 + on_ball_player.Get_Score_Value();
        float shoot_possibility = (on_ball_player.Get_Shoot_Value() + 10) / 100f;
        int on_ball_player_index = gm.Get_Index_Of_Player(on_ball_player);

        if (Is_Not_Player_Rightmost(on_ball_player_index))
            gm.Move(on_ball_player, 1);

        yield return StartCoroutine(gm.Shoot_Coroutine(on_ball_player, shoot_score, shoot_possibility));
    }

    private bool Is_Not_Player_Rightmost(int on_ball_player_index)
    {

        return on_ball_player_index != 4;
    }
}

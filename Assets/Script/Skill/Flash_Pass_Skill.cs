using UnityEngine;
using System.Collections;

public class Flash_Pass_Skill : Skill
{
    public override string Get_Name() => "flash pass";

    public override Player_Action Get_Action() => Player_Action.Flash_Pass;

    public override int Get_Value() => 4;

    public override string Get_Animation_Type() => "pass";
    

    public override byte Get_Position_Query()
    {
        if (position_query == 0)
            position_query = Make_Position_Query(true, true, true, true, true);
        return position_query;
    }


    public override IEnumerator Act()
    {
        Basketball_Player on_ball_player = gm.Get_On_Ball_Player();
        float pass_value = (on_ball_player.Get_Pass_Value()-35) / 100f;


        yield return StartCoroutine(gm.Pass_Coroutine(on_ball_player, pass_range:5, pass_value , (target_player) =>
        {
            target_player.additional_stat.score_value += 1;
            target_player.additional_stat.shoot_value += 10;
        }));
    }


}

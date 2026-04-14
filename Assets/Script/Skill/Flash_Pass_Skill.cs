using UnityEngine;
using System.Collections;

public class Flash_Pass_Skill : Skill
{
    public override string Get_Name() => "flash pass";

    public override Player_Action Get_Action() => Player_Action.Flash_Pass;

    public override bool Is_On_Ball_Skill() => true;

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

    public override IEnumerator Act_By_Ai(int target_index)
    {
        Basketball_Player on_ball_player = gm.Get_On_Ball_Player();
        float pass_value = (on_ball_player.Get_Pass_Value()-35) / 100f;
        Basketball_Player target_player = gm.Get_Player_By_Index_In_Attacker(target_index);

        yield return new WaitForSeconds(1);

        StartCoroutine(gm.Pass(on_ball_player, target_player, (target) =>
        {
            target_player.additional_stat.score_value += 1;
            target_player.additional_stat.shoot_value += 10;
        }));

    }

}

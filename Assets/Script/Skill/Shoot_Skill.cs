using System.Collections;
using UnityEngine;

public class Shoot_Skill : Skill
{
    public override Player_Action Get_Action() => Player_Action.Shoot;

    public override string Get_Name() => "shoot";

    public override bool Is_On_Ball_Skill() => true;

    public override int Get_Value() => 0;

    public override string Get_Animation_Type() => "shoot";

    public override byte Get_Position_Query()
    {
        if (position_query == 0)
            position_query = Make_Position_Query(true, true, true, false, false);

        return position_query;
    }

    public override IEnumerator Act()
    {
        Basketball_Player on_ball_player = gm.Get_On_Ball_Player();
        int shoot_score = 2;
        float shoot_possibility = on_ball_player.Get_Shoot_Value() / 100f;


        yield return StartCoroutine(gm.Shoot_Coroutine(on_ball_player, shoot_score, shoot_possibility));


        
    }




}

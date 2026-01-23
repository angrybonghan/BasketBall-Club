using System.Collections;
using UnityEngine;

public class Shoot_Skill : Skill
{
    public override Player_Action Get_Action() => Player_Action.Shoot;

    public override IEnumerator Act()
    {
        Basketball_Player on_ball_player = gm.Get_On_Ball_Player();

        bool success = Check_Shoot_Success(on_ball_player);
        on_ball_player.Set_On_Ball(false);

        if (success)
        {
            gm.score += 2;
            gm.Next_Round();
            yield break;
        }

        gm.Rebound();


        yield return null;
    }


    private bool Check_Shoot_Success(Basketball_Player on_ball_player)
    {
        float shoot_possibility = on_ball_player.shoot_value / 100f;
        float random_value = Random.Range(0f, 1f);

        if (shoot_possibility > random_value)
            return true;
        return false;
    }




}

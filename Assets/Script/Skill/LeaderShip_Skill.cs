using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeaderShip_Skill : Skill
{
    public override string Get_Name() => "leadership";

    public override Player_Action Get_Action() => Player_Action.Leadership;

    public override int Get_Value() => 1;
    

    public override byte Get_Position_Query()
    {
        if (position_query == 0)
            position_query = Make_Position_Query(true, true, true, true, true);
        return position_query;
    }


    public override IEnumerator Act()
    {
        List<Basketball_Player> attack_players = gm.attack_players;
        foreach (var player in attack_players)
        {
            player.additional_stat.score_value += 1;
            player.additional_stat.pass_value += 5;
        }


        yield break;
    }


}

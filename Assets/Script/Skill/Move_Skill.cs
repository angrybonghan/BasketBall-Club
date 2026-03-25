using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move_Skill : Skill
{
    public override Player_Action Get_Action() => Player_Action.Move;

    public override string Get_Name() => "move";

    public override int Get_Value() => 2;

    public override string Get_Animation_Type() => "move";

    public override byte Get_Position_Query()
    {
        if (position_query == 0)
            position_query = Make_Position_Query(true, true, true, true, true);
        return position_query;
    }


    public override IEnumerator Act()
    {
        Basketball_Player main_player = gm.Get_Skill_Player();
        Basketball_Player target_player = null;

        List<Basketball_Player> near_players = gm.Get_Near_Players(main_player, 1);

        yield return StartCoroutine(gm.Select_Player(near_players, (player) => target_player = player));

        int main_player_index = gm.Get_Index_Of_Player(main_player);
        int target_player_index = gm.Get_Index_Of_Player(target_player);

        int move_range = target_player_index - main_player_index;

        gm.Move(main_player, move_range);

    }

}

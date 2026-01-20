using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move_Skill : Skill
{
    public override Player_Action Get_Action() => Player_Action.Off_Ball_Move;

    public override IEnumerator Act()
    {
        Basketball_Player main_player = gm.Get_Skill_Player();
        Basketball_Player target_player = null;

        List<Basketball_Player> near_players = gm.Get_Near_Players(main_player, 1);

        yield return StartCoroutine(gm.Select_Player(near_players, (player) => target_player = player));

        int main_player_index = gm.Get_Index_Of_Player(main_player);
        int target_player_index = gm.Get_Index_Of_Player(target_player);

        gm.attack_players[main_player_index] = target_player;
        target_player.transform.position = new Vector2((main_player_index - 2) * 2.5f, 0);

        gm.attack_players[target_player_index] = main_player;
        main_player.transform.position = new Vector2((target_player_index - 2) * 2.5f, 0);
    }
}

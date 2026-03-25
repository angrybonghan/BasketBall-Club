using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public partial class Basketball_Game_Manager : MonoBehaviour
{
    public void Pass(Basketball_Player passing_player, Basketball_Player passed_player)
    {

        passing_player.Pass_Animation();

        passing_player.Set_On_Ball(false);

        passed_player.Set_On_Ball(true);
    }

    public void Pass(Basketball_Player passing_player, Basketball_Player passed_player,float pass_possible)
    {
        if (Is_Pass_Success( pass_possible))
        {
            Pass(passing_player, passed_player);
            return;
        }

        Next_Round();
    }

    private bool Is_Pass_Success(float pass_possible) {
        
        float random_value = Random.Range(0.0f, 1f);
        
        return random_value <= pass_possible;

    }

    public IEnumerator Pass_Coroutine(Basketball_Player passing_player , int pass_range, float pass_possible, System.Action<Basketball_Player> action_to_target_player = null)
    {
        List<Basketball_Player> passable_players = Get_Near_Players(passing_player,pass_range);

        Basketball_Player target_player = null;

        yield return StartCoroutine(Select_Player(passable_players , (result) => target_player = result));


        Check_Action_And_Do_Action(target_player, action_to_target_player);

        Pass(passing_player, target_player,pass_possible);
    }

    private void Check_Action_And_Do_Action(Basketball_Player target_player, System.Action<Basketball_Player> action_to_target_player)
    {
        if(action_to_target_player != null)
            action_to_target_player(target_player);
    }


}

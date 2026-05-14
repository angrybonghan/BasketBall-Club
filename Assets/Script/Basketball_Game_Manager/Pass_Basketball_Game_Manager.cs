using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public partial class Basketball_Game_Manager : MonoBehaviour
{
    public IEnumerator Pass(Basketball_Player passing_player, Basketball_Player passed_player, float duration_time = 0.3f , float spin_degree = 1080,System.Action<Basketball_Player> action = null)
    {
    

        Ball_Animation_Of_Pass(passing_player.transform.position + new Vector3(0,0.7f,0), passed_player.transform.position + new Vector3(0,0.7f,0), duration_time , spin_degree);

        passing_player.Pass_Animation();

        passing_player.Set_On_Ball(false);

        Check_Action_And_Do_Action(passed_player, action);
        yield return new WaitForSeconds(duration_time);


        passed_player.Set_On_Ball(true);
    }

    public void Pass(Basketball_Player passing_player, Basketball_Player passed_player,float pass_possible, float duration_time = 0.3f , float spin_degree = 1080,System.Action<Basketball_Player> action = null)
    {
        if (Is_Pass_Success( pass_possible))
        {
            StartCoroutine(Pass(passing_player, passed_player,duration_time:duration_time , spin_degree));
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



        Pass(passing_player, target_player,pass_possible:pass_possible , action:action_to_target_player);
    }

    private void Check_Action_And_Do_Action(Basketball_Player target_player, System.Action<Basketball_Player> action_to_target_player)
    {
        if(action_to_target_player != null)
            action_to_target_player(target_player);
    }


}

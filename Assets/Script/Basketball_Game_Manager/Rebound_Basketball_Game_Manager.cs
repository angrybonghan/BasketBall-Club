using System.Collections;
using UnityEngine;

public partial class Basketball_Game_Manager : MonoBehaviour
{

    public IEnumerator Rebound_Coroutine()
    {
        Basketball_Player rebound_player = Get_Rebound_Player();
        float time;
        Ball_Animation_Of_Rebound(goal_post.transform.position, rebound_player.transform.position + new Vector3(0.1f, 1, 0), out time, 1080);
        
        yield return new WaitForSeconds(time);

        Rebound(rebound_player);

    }

    public void Rebound(Basketball_Player rebound_player)
    {

        if (rebound_player.Is_Attacker())
        {
            if (current_turn < 16)
                current_turn = 16;
            rebound_player.Set_On_Ball(true);
            return;
        }
        Next_Round();

    }

    public void Rebound()
    {
        Basketball_Player rebound_player = Get_Rebound_Player();

        Rebound(rebound_player);
    }

    private Basketball_Player Get_Rebound_Player()
    {
        int max_value = Get_Max_Rebound_Value();
        int random = Random.Range(0, max_value);

        foreach (var player in all_players)
        {
            random -= player.Get_Rebound_Value();
            if (random < 0)
                return player;
        }

        return null;
    }

    private int Get_Max_Rebound_Value()
    {
        int result = 0;
        foreach (var player in all_players)
        {
            result += player.Get_Rebound_Value();
        }
        
        return result;
    }
    

}

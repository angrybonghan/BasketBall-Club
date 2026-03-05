using UnityEngine;

public partial class Basketball_Game_Manager : MonoBehaviour
{
    public void Rebound()
    {
        Basketball_Player rebound_player = Get_Rebound_Player();

        if (rebound_player.Is_Attacker())
        {
            current_turn = 16;
            rebound_player.Set_On_Ball(true);
            return;
        }
        Next_Round();

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

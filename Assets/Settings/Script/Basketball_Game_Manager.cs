using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Basketball_Game_Manager : MonoBehaviour
{
    public static Basketball_Game_Manager script;
    public GameObject player_hand_ui_object;
    public List<Basketball_Player> attack_players;
    public bool doing_game;

    private void Awake()
    {
        script = this;
    }

    private void Start()
    {

    }

    public List<Basketball_Player> Get_Off_Ball_Players()
    {
        List<Basketball_Player> result = new();

        foreach (var player in attack_players)
        {
            if (player.On_Ball() == true)
                continue;
            result.Add(player);
        }

        return result;
    }

    public void Pass(Basketball_Player passing_player, Basketball_Player passed_player)
    {
        passing_player.Set_On_Ball(false);

        passed_player.Set_On_Ball(true);
    }



    public void Delete_Player_Card_UI()
    {
        Transform[] card_transforms = player_hand_ui_object.GetComponentsInChildren<Transform>(); 
        foreach (var card in card_transforms )
        {
            Destroy(card.gameObject);
        }
    }

}

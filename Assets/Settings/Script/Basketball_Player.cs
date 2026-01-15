using UnityEngine;
using System.Collections.Generic;

public class Basketball_Player : MonoBehaviour
{
    private bool on_ball;
    public bool On_Ball() => on_ball;



    [Header("Other_Value")]
    public int passed_value;

    public List<Attacker_Action> player_action;

    public List<GameObject> action_cards_prefeb;


    public void Set_On_Ball(bool on_ball)
    {
        this.on_ball = on_ball;

    }

    public void Pass_Random()
    {
        Basketball_Game_Manager manager = Basketball_Game_Manager.script;

        List<Basketball_Player> other_basketball_players = manager.Get_Off_Ball_Players();

        int random_player_index = Random.Range(0, other_basketball_players.Count);
        Basketball_Player passed_player = other_basketball_players[random_player_index];

        manager.Pass(this, passed_player);
    }

    public void Set_Player_Card_UI()
    {
        Basketball_Game_Manager manager = Basketball_Game_Manager.script;

        foreach (var action in player_action)
        {
            if (action == Attacker_Action.Shoot)
            {
                GameObject card = Instantiate(action_cards_prefeb[0], manager.player_hand_ui_object.transform);
            }
            if (action == Attacker_Action.Pass)
            {
                GameObject card = Instantiate(action_cards_prefeb[1], manager.player_hand_ui_object.transform);
            }
        }
    }


    private void Start()
    {
        Set_Player_Card_UI();
    }

    private void Update()
    {
        if (on_ball)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
            return;
        }

        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
    }


}


public enum Attacker_Action {
    None,
    Off_Ball_Move,
    Shoot,
    Pass
}

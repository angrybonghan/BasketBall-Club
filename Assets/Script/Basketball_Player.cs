using UnityEngine;
using System.Collections.Generic;

public class Basketball_Player : MonoBehaviour
{
    private Basketball_Game_Manager gm => Basketball_Game_Manager.Get_Game_Manager();

    [SerializeField] bool on_ball;
    [SerializeField] bool attacker;
    public bool select_mode;
    public bool On_Ball() => on_ball;
    public void Set_On_Ball(bool on_ball) => this.on_ball = on_ball;

    public bool Is_Attacker() => attacker;
    public bool Is_Defender() => !attacker;

    public void Set_Attacker(bool attacker) => this.attacker = attacker;

    [Header("Other_Value")]
    public int passed_value;
    public int rebound_value;

    public int shoot_value;

    public List<Player_Action> on_ball_actions;
    public List<Player_Action> off_ball_actions;

    public GameObject action_card_prefeb;

    
    

    public void Show_Player_Card()
    {
        gm.Delete_Player_Card_UI();
        gm.Set_Skill_Player(this);

        if (on_ball)
        {
            Show_On_Ball_Card();
            return;
        }
        Show_Off_Ball_Card();
    }

    private void Show_On_Ball_Card() => Show_Card(on_ball_actions);

    private void Show_Off_Ball_Card() => Show_Card(off_ball_actions);

    private void Show_Card(List<Player_Action> player_actions)
    {

        foreach (var action in player_actions)
        {
            GameObject card = Instantiate(action_card_prefeb, gm.player_hand_ui_object.transform);
            Card_Script card_script = card.GetComponent<Card_Script>();


            card_script.Set(action);
        }
    }


    private void Update()
    {
        Set_Color();
        
    }

    private void Set_Color()
    {
        if (select_mode)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
            return;
        }

        if (on_ball)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
            return;
        }

        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
    }

    private void OnMouseUp()
    {
        if (Is_Defender())
            return;

        if (select_mode)
        {
            gm.Set_Selected_Player(this);
            return;
        }

        if (gm.Get_Select_Mode())
            return;

        gm.Delete_Player_Card_UI();
        Show_Player_Card();
    }
}


public enum Player_Action {
    None,
    Move,
    Shoot,
    Pass
}

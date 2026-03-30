using UnityEngine;
using System.Collections.Generic;

public partial class Basketball_Player : MonoBehaviour
{
    [SerializeField] bool on_ball;
    [SerializeField] bool attacker;
    public bool select_mode;
    public bool On_Ball() => on_ball;

    public bool Off_Ball() => !on_ball;

    public void Set_On_Ball(bool on_ball) {
        Set_Ball_Condition_Animation(on_ball);
        this.on_ball = on_ball;
    }

    public bool Is_Attacker() => attacker;
    public bool Is_Defender() => !attacker;

    public void Set_Attacker(bool attacker) => Set_Player_Attacker_Or_Defender(attacker);


    [Header("Stat")]
    [SerializeField] private Player_Stat standard_stat;

    public Player_Stat additional_stat;

    public List<Player_Action> actions;

    public GameObject action_card_prefeb;


    public int Get_Pass_Value()
    {
        return standard_stat.pass_value + additional_stat.pass_value;
    }

    public int Get_Shoot_Value()
    {
        return standard_stat.shoot_value + additional_stat.shoot_value;
    }

    public int Get_Score_Value()
    {
        return standard_stat.score_value + additional_stat.score_value;
    }

    public int Get_Rebound_Value()
    {
        return standard_stat.rebound_value + additional_stat.rebound_value;
    }

    private void Set_Player_Attacker_Or_Defender(bool attacker)
    {
        if (attacker)
        {
            Set_To_Attacker();
            return;
        }
        Set_To_Defender();
    }

    private void Set_To_Attacker()
    {

        GetComponent<SpriteRenderer>().flipX = false;

        GetComponent<SpriteRenderer>().color = new Color(1,1,1);

        attacker = true;
    }

    private void Set_To_Defender()
    {
        attacker = false;

        GetComponent<SpriteRenderer>().flipX = true;

        GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
    }
}

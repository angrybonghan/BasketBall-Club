using UnityEngine;
using System.Collections.Generic;

public partial class Basketball_Player : MonoBehaviour
{
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


}

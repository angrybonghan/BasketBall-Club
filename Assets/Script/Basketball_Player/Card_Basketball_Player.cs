using UnityEngine;
using System.Collections.Generic;

public partial class Basketball_Player : MonoBehaviour
{
    public void Show_Player_Card()
    {
        gm.Delete_Player_Card_UI();
        gm.Set_Skill_Player(this);

        Show_Card();

    }
    


    private void Show_Card()
    {

        Skill_Database database = Skill_Database.Get_Database(); 
        foreach (var action in actions)
        {
            Skill skill = database.Get_Skill(action);
            if (skill.Can_Skill_Use(this) == false)
            {
                continue;
            }
            
            
            GameObject card = Instantiate(action_card_prefeb, gm.player_hand_ui_object.transform);
            Card_Script card_script = card.GetComponent<Card_Script>();


            card_script.Set(action);
        }
    }


}

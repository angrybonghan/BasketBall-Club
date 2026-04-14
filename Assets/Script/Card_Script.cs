using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card_Script : MonoBehaviour
{

    [SerializeField] TMP_Text card_name;
    Player_Action action;
    Basketball_Player player;


    public void Set(Player_Action action, Basketball_Player player)
    {
        Skill_Database skill_database = Skill_Database.Get_Database();

        this.action = action;
        this.player = player;

        Set_Color();

        card_name.text = skill_database.Get_Name_Of_Skill(action);
    }

    private void Set_Color()
    {
        Skill_Database skill_database = Skill_Database.Get_Database();

        if (skill_database.Can_Act_Action(player, action))
        {
            GetComponent<Image>().color = new Color(1, 1, 1);
        }
        else
        {
            GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

    public void Act()
    {
        Skill_Database skill_database = Skill_Database.Get_Database();

        skill_database.Act_Skill(action, player);
    }

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(Click);

    }


    private void Click()
    {
        Act();
    }
}

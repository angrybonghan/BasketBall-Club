using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card_Script : MonoBehaviour
{

    [SerializeField] TMP_Text card_name;
    Player_Action action;


    public void Set(Player_Action action)
    {
        Skill_Database skill_database = Skill_Database.Get_Database();

        this.action = action;

        card_name.text = skill_database.Get_Name_Of_Skill(action);
    }

    public void Act()
    {
        Skill_Database skill_database = Skill_Database.Get_Database();

        skill_database.Act_And_Calculate_Turn(action);
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

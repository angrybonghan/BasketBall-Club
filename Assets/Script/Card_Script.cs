using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card_Script : MonoBehaviour
{

    [SerializeField] TMP_Text card_name;
    Player_Action action;


    public void Set(Player_Action action)
    {
        this.action = action;
        if (action == Player_Action.Pass)
            card_name.text = "pass";
        if (action == Player_Action.Shoot)
            card_name.text = "shoot";
        if (action == Player_Action.Off_Ball_Move)
            card_name.text = "move";
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

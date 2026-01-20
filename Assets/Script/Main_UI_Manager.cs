using TMPro;
using UnityEngine;

public class Main_UI_Manager : MonoBehaviour
{
    private Basketball_Game_Manager gm => Basketball_Game_Manager.Get_Game_Manager();
    

    [SerializeField] private TMP_Text turn_text; 
    [SerializeField] private TMP_Text score_text; 
    

    // Update is called once per frame
    void Update()
    {
        turn_text.text = gm.current_turn.ToString();
        score_text.text = gm.score.ToString();
    }
}

using TMPro;
using UnityEngine;

public class Main_UI_Manager : MonoBehaviour
{
    private Basketball_Game_Manager game_manager;
    [SerializeField] private TMP_Text turn_text; 
    [SerializeField] private TMP_Text score_text; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        game_manager = GetComponent<Basketball_Game_Manager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        turn_text.text = game_manager.current_turn.ToString();
        score_text.text = game_manager.score.ToString();
    }
}

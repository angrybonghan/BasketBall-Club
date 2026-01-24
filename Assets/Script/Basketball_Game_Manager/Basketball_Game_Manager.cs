using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public delegate bool Player_Filter(Basketball_Player player);

public partial class Basketball_Game_Manager : MonoBehaviour
{
    private static Basketball_Game_Manager script;
    public static Basketball_Game_Manager Get_Game_Manager() => script;
    public GameObject player_hand_ui_object;
    [HideInInspector] public List<Basketball_Player> attack_players;
    [HideInInspector] public List<Basketball_Player> defence_players;
    [HideInInspector] public List<Basketball_Player> all_players;

    [HideInInspector] public List<Basketball_Player> first_team_players;
    [HideInInspector] public List<Basketball_Player> second_team_players;

    public int current_turn = 24;
    public int score = 0;
    public int round = 0;
    private Basketball_Player skill_player;//스킬 사용한 플레이어
    [SerializeField] List<GameObject> first_team_prefeb;
    [SerializeField] List<GameObject> second_team_prefeb;

    [SerializeField] GameObject first_team_object;
    [SerializeField] GameObject second_team_object;

    public void Set_Skill_Player(Basketball_Player player) => skill_player = player;

    public Basketball_Player Get_Skill_Player() => skill_player;

    private void Awake()
    {
        script = this;
    }

    private void Start()
    {
        Set_Player();
        Next_Round();
    }

    public void Set_Player()
    {
        first_team_players = Set_Player_Prefeb(first_team_prefeb , first_team_object);

        second_team_players = Set_Player_Prefeb(second_team_prefeb, second_team_object);
    }

    private List<Basketball_Player> Set_Player_Prefeb(List<GameObject> player_prefeb , GameObject parent)
    {
        List<Basketball_Player> result = new();
        for (int i = 0; i < player_prefeb.Count; i++)
        {
            GameObject player_gameObject = Instantiate(player_prefeb[i], parent.transform);
            Basketball_Player player = player_gameObject.GetComponent<Basketball_Player>();

            player.transform.localPosition = new Vector2((i-2)*2.5f , 0);
            result.Add(player);
            all_players.Add(player);
        }
        return result;
    }

    public void Next_Round()
    {
        bool is_first_team_attack = (round % 2) == 0;
        round++;
        current_turn = 24;

        if (is_first_team_attack)
        {
            attack_players = first_team_players;
            defence_players = second_team_players;

            first_team_object.transform.position = new Vector2(0, 0);
            second_team_object.transform.position = new Vector2(0, 2.5f);
        }
        else
        {
            attack_players = second_team_players;
            defence_players = first_team_players;

            second_team_object.transform.position = new Vector2(0, 0);
            first_team_object.transform.position = new Vector2(0, 2.5f);
        }

        Set_Attacker_And_Defender();
        Set_Ball();
    }

    private void Set_Ball()
    {
        foreach (var player in attack_players)
            player.Set_On_Ball(false);

        foreach (var player in defence_players)
            player.Set_On_Ball(false);

        attack_players[0].Set_On_Ball(true);
    }

    private void Set_Attacker_And_Defender()
    {
        foreach (var player in attack_players)
            player.Set_Attacker(true);

        foreach (var player in defence_players)
            player.Set_Attacker(false);
    }

    public int Get_Attack_Player_Count() => attack_players.Count;

    public void Pass(Basketball_Player passing_player, Basketball_Player passed_player)
    {
        passing_player.Set_On_Ball(false);

        passed_player.Set_On_Ball(true);
    }

    public void Delete_Player_Card_UI()
    {
        Transform[] card_transforms = player_hand_ui_object.GetComponentsInChildren<Transform>(); 
        foreach (var card in card_transforms )
        {
            if (card == player_hand_ui_object.transform)
                continue;
            Destroy(card.gameObject);
        }
    }
}

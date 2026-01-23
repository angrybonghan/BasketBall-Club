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
        first_team_players = new();
        for (int i = 0; i < first_team_prefeb.Count; i++)
        {
            GameObject player_gameObject = Instantiate(first_team_prefeb[i], first_team_object.transform);
            Basketball_Player player = player_gameObject.GetComponent<Basketball_Player>();

            player.transform.localPosition = new Vector2((i-2)*2.5f , 0);
            first_team_players.Add(player);
            all_players.Add(player);
        }

        second_team_players = new();
        for (int i = 0; i < second_team_prefeb.Count; i++)
        {
            GameObject player_gameObject = Instantiate(second_team_prefeb[i], second_team_object.transform);
            Basketball_Player player = player_gameObject.GetComponent<Basketball_Player>();

            player.transform.localPosition = new Vector2((i-2)*2.5f , 0);
            second_team_players.Add(player);
            all_players.Add(player);
        }

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

        Set_Attacker();
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

    private void Set_Attacker()
    {
        foreach (var player in attack_players)
            player.Set_Attacker(true);

        foreach (var player in defence_players)
            player.Set_Attacker(false);
    }

    public void Rebound()
    {
        Basketball_Player rebound_player = Get_Rebound_Player();

        if (rebound_player.Is_Attacker())
        {
            current_turn = 16;
            rebound_player.Set_On_Ball(true);
            return;
        }
        Next_Round();

    }

    private Basketball_Player Get_Rebound_Player()
    {
        int max_value = Get_Max_Rebound_Value();
        int random = Random.Range(0, max_value);

        foreach (var player in all_players)
        {
            random -= player.rebound_value;
            if (random < 0)
                return player;
        }

        return null;
    }

    private int Get_Max_Rebound_Value()
    {
        int result = 0;
        foreach (var player in all_players)
        {
            result += player.rebound_value;
        }
        
        return result;
    }
    

    public int Get_Attack_Player_Count() => attack_players.Count;

    public int Get_Index_Of_Player(Basketball_Player player)
    {
        for(int i = 0; i < Get_Attack_Player_Count(); i++)
        {
            if (player == attack_players[i])
                return i;
        }

        return -1;
    }

    public List<Basketball_Player> Get_Near_Players(Basketball_Player player , int range)
    {
        List<Basketball_Player> result = new();
        int main_player_index = Get_Index_Of_Player(player);

       List<Basketball_Player> left_side_players = Get_Left_Side_Player(main_player_index , range);
       List<Basketball_Player> right_side_players = Get_Right_Side_Player(main_player_index, range);


        result.AddRange(left_side_players);
        result.AddRange(right_side_players);

        return result;
    }

    private List<Basketball_Player> Get_Left_Side_Player(int main_player_index, int range)
    {
        int left_start = Mathf.Max(0, main_player_index - range);
        int left_end = Mathf.Max(-1, main_player_index - 1);
        List<Basketball_Player> left_side_players = Get_Players_By_Range(left_start, left_end);
        return left_side_players;
    }

    private List<Basketball_Player> Get_Right_Side_Player(int main_player_index, int range)
    {
        int max_index = Get_Attack_Player_Count();

        int right_start = Mathf.Min(max_index, main_player_index + 1);
        int right_end = Mathf.Min(max_index -1, main_player_index + range);
        List<Basketball_Player> right_side_players = Get_Players_By_Range(right_start, right_end);

        return right_side_players;
    }

    public List<Basketball_Player> Get_Players_By_Range(int start, int end)
    {
        List<Basketball_Player> result = new();
        for (int i = start; i <= end; i++)
        {
            result.Add(attack_players[i]);
        }

        return result;
    }

    public List<Basketball_Player> Get_Players(Player_Filter player_filter)
    {

        List<Basketball_Player> result = new();

        foreach (var player in attack_players)
        {
            if (player_filter(player) == false)
                continue;
            result.Add(player);
        }

        return result;


    }

    public List<Basketball_Player> Get_Off_Ball_Players()
    {
        return Get_Players((player) => player.On_Ball() == false);
    }

    public Basketball_Player Get_On_Ball_Player()
    {
        foreach (var player in attack_players)
        {
            if (player.On_Ball() == true)
                return player;
        }
        return null;
    }

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

    private bool select_mode;
    private Basketball_Player selected_player;
    public bool Get_Select_Mode() => select_mode;

    public void Set_Selected_Player(Basketball_Player player) => selected_player = player;

    public IEnumerator Select_Player(List<Basketball_Player> players , System.Action<Basketball_Player> on_complete)
    {
        Delete_Player_Card_UI();
        select_mode = true;
        selected_player = null;
        foreach (var player in players)
            player.select_mode = true;


        while (selected_player == null)
            yield return null;


        select_mode = false;
        on_complete(selected_player);
        foreach (var player in players)
            player.select_mode = false;

    }


}

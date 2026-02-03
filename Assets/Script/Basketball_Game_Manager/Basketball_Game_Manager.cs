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

            player.Set_Name((i + 1).ToString());
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
        Set_Player_Stat_Clear();
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

    private void Set_Player_Stat_Clear()
    {
        foreach (var player in all_players)
            player.additional_stat = new Player_Stat();
    }

    public int Get_Attack_Player_Count() => attack_players.Count;

    public void Pass(Basketball_Player passing_player, Basketball_Player passed_player)
    {
        passing_player.Set_On_Ball(false);

        passed_player.Set_On_Ball(true);
    }

    public void Pass(Basketball_Player passing_player, Basketball_Player passed_player,float pass_possible)
    {
        float random_value = Random.Range(0.0f, 1f);
        if (random_value > pass_possible)
        {
            Next_Round();
            return;
        }
        Pass(passing_player, passed_player);
    }

    public IEnumerator Pass_Coroutine(Basketball_Player passing_player , int pass_range, float pass_possible, System.Action<Basketball_Player> action_to_target_player = null)
    {
        List<Basketball_Player> passable_players = Get_Near_Players(passing_player,pass_range);


        Basketball_Player target_player = null;

        yield return StartCoroutine(Select_Player(passable_players , (result) => target_player = result));

        if(action_to_target_player != null)
            action_to_target_player(target_player);

        Pass(passing_player, target_player,pass_possible);
    }

    public IEnumerator Shoot_Coroutine(Basketball_Player shooter,int shoot_score, float shoot_possibility)
    {

        bool success = Check_Shoot_Success(shoot_possibility);
        shooter.Set_On_Ball(false);

        if (success)
        {
            score += shoot_score;
            Next_Round();
            yield break;
        }

        Rebound();

        yield return null;

    }

    public void Move(Basketball_Player player , int move_range)
    {
        int player_index = Get_Index_Of_Player(player);
        move_range = Mathf.Min(attack_players.Count - player_index - 1, move_range);
        move_range = Mathf.Max(-player_index, move_range);

        if (move_range > 0)
            Move_Right(player, move_range);
        else
            Move_Left(player, -move_range);

        Update_Player_Display();
        
    }

    private void Move_Right(Basketball_Player player, int move_range)
    {
        int player_index = Get_Index_Of_Player(player);
        for (int i = 0; i < move_range; i++)
        {
            attack_players[player_index + i] = attack_players[player_index + i + 1];
        }
        attack_players[player_index + move_range] = player;
    }

    private void Move_Left(Basketball_Player player, int move_range)
    {
        int player_index = Get_Index_Of_Player(player);

        for (int i = 0; i < move_range; i++)
        {
            attack_players[player_index - i] = attack_players[player_index - i - 1];
        }
        attack_players[player_index - move_range] = player;
    }

    public void Update_Player_Display()
    {
        for (int i = 0; i < attack_players.Count; i++)
        {
            attack_players[i].transform.position = new Vector2((i - 2) * 2.5f, 0);
        }
    }


    private bool Check_Shoot_Success(float shoot_possibility)
    {
        float random_value = Random.Range(0f, 1f);

        if (shoot_possibility > random_value)
            return true;
        return false;
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

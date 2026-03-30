using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public delegate bool Player_Filter(Basketball_Player player);

public partial class Basketball_Game_Manager : MonoBehaviour
{
    
    private static Basketball_Game_Manager script;
    public static Basketball_Game_Manager Get_Game_Manager() => script;
    public GameObject player_hand_ui_object;
    [HideInInspector] public List<Basketball_Player> all_players;

    public Team first_team;
    public Team second_team;
    [HideInInspector] public Team attack_team;
    [HideInInspector] public Team defence_team;

    public int current_turn = 24;
    public int round = 0;
    private Basketball_Player skill_player;//스킬 사용한 플레이어

    [SerializeField] Vector3 attack_team_position;
    [SerializeField] Vector3 defence_team_position;

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
        all_players.AddRange(first_team.Set_Player_Prefeb());
        all_players.AddRange(second_team.Set_Player_Prefeb());
    }


    public void Next_Round()
    {
        bool is_first_team_attack = (round % 2) == 0;
        round++;
        current_turn = 24;

        if (is_first_team_attack)
        {
            attack_team = first_team;
            defence_team = second_team;

            attack_team.team_object.transform.position = attack_team_position;
            defence_team.team_object.transform.position = defence_team_position;
        }
        else
        {
            attack_team = second_team;
            defence_team = first_team;

            second_team.team_object.transform.position = attack_team_position;
            first_team.team_object.transform.position = defence_team_position;
        }

        Set_Attacker_And_Defender();
        Set_Ball();
        Set_Player_Stat_Clear();
    }

    private void Set_Ball()
    {
        foreach (var player in attack_team.players)
            player.Set_On_Ball(false);

        foreach (var player in defence_team.players)
            player.Set_On_Ball(false);

        attack_team.players[0].Set_On_Ball(true);
    }

    private void Set_Attacker_And_Defender()
    {
        attack_team.Set_Attack();
        defence_team.Set_Defender();
    }

    private void Set_Player_Stat_Clear()
    {
        foreach (var player in all_players)
            player.additional_stat = new Player_Stat();
    }

    public int Get_Attack_Player_Count() => attack_team.players.Count;

    public IEnumerator Shoot_Coroutine(Basketball_Player shooter,int shoot_score, float shoot_possibility)
    {
        shooter.Shoot_Animation();

        yield return new WaitForSeconds(2);

        bool success = Check_Shoot_Success(shoot_possibility);
        shooter.Set_On_Ball(false);

        if (success)
        {
            shooter.team.score += shoot_score;
            Next_Round();
            yield break;
        }

        Rebound();

        yield return null;

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

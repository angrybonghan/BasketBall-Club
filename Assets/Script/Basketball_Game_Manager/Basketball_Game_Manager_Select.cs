using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Basketball_Game_Manager : MonoBehaviour
{
    private bool select_mode;
    private Basketball_Player selected_player;
    public bool Get_Select_Mode() => select_mode;

    public void Set_Selected_Player(Basketball_Player player) => selected_player = player;

    public IEnumerator Select_Player(List<Basketball_Player> players , System.Action<Basketball_Player> action_for_select_player)
    {
        Set_For_Select_Mode(players);

        while (Waiting_Query())
            yield return null;

        action_for_select_player(selected_player);

        Reset_For_Select_Mode(players);
    }

    private void Set_For_Select_Mode(List<Basketball_Player> players)
    {
        Delete_Player_Card_UI();
        select_mode = true;
        selected_player = null;

        foreach (var player in players)
            player.select_mode = true;

    }

    private void Reset_For_Select_Mode(List<Basketball_Player> players)
    {

        select_mode = false;
        foreach (var player in players)
            player.select_mode = false;


    }

    private bool Waiting_Query() => selected_player == null;

}

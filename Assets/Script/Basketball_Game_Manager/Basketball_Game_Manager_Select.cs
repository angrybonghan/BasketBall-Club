using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Basketball_Game_Manager : MonoBehaviour
{
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

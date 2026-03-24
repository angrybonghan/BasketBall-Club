using UnityEngine;
using System.Collections.Generic;
using System;

public partial class Basketball_Player : MonoBehaviour
{
    private Basketball_Game_Manager gm => Basketball_Game_Manager.Get_Game_Manager();

    public void Set_Name(string name)
    {
        Show_Player_Name text_box = GetComponent<Show_Player_Name>();
        text_box.Set_Text(name);
    }

    private void Set_Color()
    {
        if (select_mode)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
            return;
        }

        if (on_ball)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
            return;
        }

        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

    private void Update()
    {
        Set_Color();
    }

    private void OnMouseUp()
    {
        if (Is_Defender())
            return;

        if (select_mode)
        {
            gm.Set_Selected_Player(this);
            return;
        }

        if (gm.Get_Select_Mode())
            return;

        gm.Delete_Player_Card_UI();
        Show_Player_Card();
    }





}





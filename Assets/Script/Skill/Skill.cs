using UnityEngine;
using System.Collections;

public abstract class Skill : MonoBehaviour
{


    private Basketball_Game_Manager game_manager;
    protected Basketball_Game_Manager gm
    {
        get
        {
            if (game_manager == null)
                game_manager = Basketball_Game_Manager.script;
            return game_manager;
        }
    }

    protected Skill_Database database
    {
        get
        {
            return GetComponent<Skill_Database>();
        }

    }

    public abstract Player_Action Get_Action();

    public abstract IEnumerator Act();
        
}


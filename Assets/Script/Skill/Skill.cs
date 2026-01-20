using UnityEngine;
using System.Collections;

public abstract class Skill : MonoBehaviour
{


    protected Basketball_Game_Manager gm => Basketball_Game_Manager.Get_Game_Manager();
    
    protected Skill_Database database => GetComponent<Skill_Database>();


    public abstract Player_Action Get_Action();

    public abstract IEnumerator Act();
        
}


using UnityEngine;
using TMPro;

public class Show_Player_Name : MonoBehaviour
{
    [SerializeField] TMP_Text text_script;
    [SerializeField] string text;

    private void Awake()
    {
        text_script.text = text;
    }


}

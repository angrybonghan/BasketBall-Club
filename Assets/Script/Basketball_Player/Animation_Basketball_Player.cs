using System.Collections;
using UnityEngine;

public partial class Basketball_Player : MonoBehaviour
{
    Animator animator;
    public float player_move_speed;

    public void Shoot_Animation()
    {
        animator.SetTrigger("shoot");
    }

    public void Move_Animation()
    {
        animator.SetTrigger("move");
    }

    public void Pass_Animation()
    {
        animator.SetTrigger("pass");
    }

    public void Set_Trigger_Animation(string trigger_type)
    {
        animator.SetTrigger(trigger_type);
    }

    public void Set_Ball_Condition_Animation(bool on_ball)
    {
        animator.SetBool("on_ball" , on_ball);
    }

    public IEnumerator Player_Move_Animation(Vector2 start_position ,Vector2 end_position)
    {
        if (start_position == end_position)
        {

            yield break;
        }
        

        float time = 0;
        Vector2 distance = end_position - start_position;
        float duration_time = (Vector2.Distance(start_position, end_position)) / player_move_speed;

        while (time < duration_time)
        {
            
            time += Time.deltaTime;
            yield return null;

            float current_x = distance.x * (time / duration_time);
            float current_y = distance.y * (time / duration_time);
            transform.localPosition = new Vector3(start_position.x + current_x, start_position.y + current_y, 0);
        }

        transform.localPosition = new Vector3(end_position.x, end_position.y, 0);
    }
}

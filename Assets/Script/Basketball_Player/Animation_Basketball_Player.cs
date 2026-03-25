using UnityEngine;

public partial class Basketball_Player : MonoBehaviour
{
    Animator animator;

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

}

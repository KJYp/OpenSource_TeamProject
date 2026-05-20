using UnityEngine;

public class UnitAnimationController : MonoBehaviour
{
    private Animator animator;
    private string currentState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void PlayAnimation(string state)
    {
        if (animator == null)
        {
            return;
        }

        if (currentState == state)
        {
            return;
        }

        currentState = state;
        animator.SetTrigger(state);
    }

    public void PlayIdle() => PlayAnimation("Idle");
    public void PlayWalk() => PlayAnimation("Walk");
    public void PlayAttack() => PlayAnimation("Attack");
    public void PlayDie() => PlayAnimation("Die");
}
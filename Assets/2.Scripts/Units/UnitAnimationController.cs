using UnityEngine;

public class UnitAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        if (animator == null)
        {
            return;
        }

        animator.SetTrigger("Idle");
    }

    public void PlayWalk()
    {
        if (animator == null)
        {
            return;
        }

        animator.SetTrigger("Walk");
    }

    public void PlayAttack()
    {
        if (animator == null)
        {
            return;
        }

        animator.SetTrigger("Attack");
    }

    public void PlayDie()
    {
        if (animator == null)
        {
            return;
        }

        animator.SetTrigger("Die");
    }
}
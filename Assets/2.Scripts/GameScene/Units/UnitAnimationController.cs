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

        animator.SetBool("IsMoving", false);
    }

    public void PlayWalk()
    {
        if (animator == null)
        {
            return;
        }

        animator.SetBool("IsMoving", true);
    }

    public void PlayAttack()
    {
        if (animator == null)
        {
            return;
        }

        animator.SetBool("IsMoving", false);
        animator.SetTrigger("Attack");
    }

    public void PlayDie()
    {
        if (animator == null)
        {
            return;
        }

        animator.SetBool("IsMoving", false);
        animator.SetTrigger("Die");
    }
}
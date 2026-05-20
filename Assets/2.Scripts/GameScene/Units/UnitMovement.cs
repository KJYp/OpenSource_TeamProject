using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private UnitStats stats;
    private UnitCombat combat;
    private UnitAnimationController animationController;

    private void Awake()
    {
        stats = GetComponent<UnitStats>();
        combat = GetComponent<UnitCombat>();
        animationController = GetComponent<UnitAnimationController>();
    }

    private void Update()
    {
        if (stats.isDead)
        {
            return;
        }

        if (combat != null && combat.HasActionTargetInRange())
        {
            if (animationController != null)
            {
                animationController.PlayIdle();
            }

            return;
        }

        MoveForward();
    }

    private void MoveForward()
    {
        float direction = stats.team == UnitTeam.Ally ? 1f : -1f;

        transform.Translate(Vector2.right * direction * stats.moveSpeed * Time.deltaTime, Space.World);

        if (animationController != null)
        {
            animationController.PlayWalk();
        }
    }
}
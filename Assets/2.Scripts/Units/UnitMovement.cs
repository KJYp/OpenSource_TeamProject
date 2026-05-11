using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private UnitStats stats;
    private UnitCombat combat;

    private void Awake()
    {
        stats = GetComponent<UnitStats>();
        combat = GetComponent<UnitCombat>();
    }

    private void Update()
    {
        if (stats.isDead)
        {
            return;
        }

        if (combat != null && combat.HasTargetInRange())
        {
            return;
        }

        MoveForward();
    }

    private void MoveForward()
    {
        float direction = stats.team == UnitTeam.Ally ? 1f : -1f;

        transform.Translate(Vector2.right * direction * stats.moveSpeed * Time.deltaTime);
    }
}
using UnityEngine;

public class UnitCombat : MonoBehaviour
{
    private UnitStats stats;
    private UnitHealth currentTarget;
    private UnitAnimationController animationController;

    private float lastAttackTime;

    private void Awake()
    {
        stats = GetComponent<UnitStats>();
        animationController = GetComponent<UnitAnimationController>();
    }

    private void Update()
    {
        if (stats.isDead)
        {
            return;
        }

        FindTarget();

        if (currentTarget != null)
        {
            TryAttack();
        }
    }

    private void FindTarget()
    {
        currentTarget = null;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, stats.attackRange);

        foreach (Collider2D hit in hits)
        {
            UnitStats targetStats = hit.GetComponent<UnitStats>();
            UnitHealth targetHealth = hit.GetComponent<UnitHealth>();

            if (targetStats == null || targetHealth == null)
            {
                continue;
            }

            if (targetStats.isDead)
            {
                continue;
            }

            if (targetStats.team == stats.team)
            {
                continue;
            }

            currentTarget = targetHealth;
            break;
        }
    }

    private void TryAttack()
    {
        if (Time.time - lastAttackTime < stats.attackCooldown)
        {
            return;
        }

        lastAttackTime = Time.time;

        Debug.Log($"{gameObject.name} attacks {currentTarget.gameObject.name}");

        if (animationController != null)
        {
            animationController.PlayAttack();
        }

        currentTarget.TakeDamage(stats.attackPower);
    }

    public bool HasTargetInRange()
    {
        FindTarget();
        return currentTarget != null;
    }

    private void OnDrawGizmosSelected()
    {
        UnitStats unitStats = GetComponent<UnitStats>();

        if (unitStats == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, unitStats.attackRange);
    }
}
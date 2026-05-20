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

        if (stats.attackType == AttackType.Healer)
        {
            FindHealTarget();

            if (currentTarget != null)
            {
                TryHeal();
            }
        }
        else
        {
            FindTarget();

            if (currentTarget != null)
            {
                TryAttack();
            }
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

    private void FindHealTarget()
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

            if (targetStats.team != stats.team)
            {
                continue;
            }

            if (targetHealth.IsFullHp())
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

        if (stats.attackType == AttackType.Melee)
        {
            currentTarget.TakeDamage(stats.attackPower);
        }
        else if (stats.attackType == AttackType.Ranged)
        {
            FireProjectile();
        }
    }

    private void TryHeal()
    {
        if (Time.time - lastAttackTime < stats.attackCooldown)
        {
            return;
        }

        lastAttackTime = Time.time;

        Debug.Log($"{gameObject.name} heals {currentTarget.gameObject.name}");

        if (animationController != null)
        {
            animationController.PlayAttack();
        }

        currentTarget.Heal(stats.healPower);
    }

    private void FireProjectile()
    {
        if (stats.projectilePrefab == null || stats.projectileSpawnPoint == null)
        {
            Debug.LogWarning($"{gameObject.name}의 Projectile 설정이 비어 있습니다.");
            return;
        }

        Vector2 direction = stats.team == UnitTeam.Ally ? Vector2.right : Vector2.left;

        GameObject projectileObject = Instantiate(
            stats.projectilePrefab,
            stats.projectileSpawnPoint.position,
            Quaternion.identity
        );

        Projectile projectile = projectileObject.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.Init(
                stats.team,
                stats.attackPower,
                stats.projectileSpeed,
                direction
            );
        }
    }

    public bool HasTargetInRange()
    {
        FindTarget();
        return currentTarget != null;
    }

    public bool HasActionTargetInRange()
    {
        if (stats.attackType == AttackType.Healer)
        {
            FindHealTarget();
        }
        else
        {
            FindTarget();
        }

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
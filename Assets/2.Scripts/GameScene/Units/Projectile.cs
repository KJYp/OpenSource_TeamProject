using UnityEngine;

public class Projectile : MonoBehaviour
{
    private UnitTeam ownerTeam;
    private float damage;
    private float speed;
    private Vector2 moveDirection;

    private bool isAreaAttack;
    private float areaRadius;
    private float areaDamageMultiplier;

    public void Init(
        UnitTeam team,
        float attackDamage,
        float projectileSpeed,
        Vector2 direction,
        bool areaAttack = false,
        float radius = 0f,
        float damageMultiplier = 1f
    )
    {
        ownerTeam = team;
        damage = attackDamage;
        speed = projectileSpeed;
        moveDirection = direction.normalized;

        isAreaAttack = areaAttack;
        areaRadius = radius;
        areaDamageMultiplier = damageMultiplier;
    }

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnitStats targetStats = collision.GetComponent<UnitStats>();
        UnitHealth targetHealth = collision.GetComponent<UnitHealth>();

        if (targetStats == null || targetHealth == null)
        {
            return;
        }

        if (targetStats.team == ownerTeam)
        {
            return;
        }

        if (targetStats.isDead)
        {
            return;
        }

        if (isAreaAttack)
        {
            ApplyAreaDamage();
        }
        else
        {
            targetHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    private void ApplyAreaDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, areaRadius);

        foreach (Collider2D hit in hits)
        {
            UnitStats targetStats = hit.GetComponent<UnitStats>();
            UnitHealth targetHealth = hit.GetComponent<UnitHealth>();

            if (targetStats == null || targetHealth == null)
            {
                continue;
            }

            if (targetStats.team == ownerTeam)
            {
                continue;
            }

            if (targetStats.isDead)
            {
                continue;
            }

            float areaDamage = damage * areaDamageMultiplier;
            targetHealth.TakeDamage(areaDamage);
        }

        Debug.Log($"{gameObject.name} area attack applied. Radius: {areaRadius}");
    }

    private void OnDrawGizmosSelected()
    {
        if (!isAreaAttack)
        {
            return;
        }

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, areaRadius);
    }
}
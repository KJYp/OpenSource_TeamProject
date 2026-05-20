using UnityEngine;

public class Projectile : MonoBehaviour
{
    private UnitTeam ownerTeam;
    private float damage;
    private float speed;
    private Vector2 moveDirection;

    public void Init(UnitTeam team, float attackDamage, float projectileSpeed, Vector2 direction)
    {
        ownerTeam = team;
        damage = attackDamage;
        speed = projectileSpeed;
        moveDirection = direction.normalized;
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

        targetHealth.TakeDamage(damage);
        Destroy(gameObject);
    }
}
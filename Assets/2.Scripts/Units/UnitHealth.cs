using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    private UnitStats stats;
    private float currentHp;

    private void Awake()
    {
        stats = GetComponent<UnitStats>();
        currentHp = stats.maxHp;
    }

    public void TakeDamage(float damage)
    {
        if (stats.isDead)
        {
            return;
        }

        currentHp -= damage;

        Debug.Log($"{gameObject.name} took {damage} damage. Current HP: {currentHp}");

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        stats.isDead = true;

        UnitMovement movement = GetComponent<UnitMovement>();
        if (movement != null)
        {
            movement.enabled = false;
        }

        UnitCombat combat = GetComponent<UnitCombat>();
        if (combat != null)
        {
            combat.enabled = false;
        }

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        UnitAnimationController animationController = GetComponent<UnitAnimationController>();
        if (animationController != null)
        {
            animationController.PlayDie();
        }

        Debug.Log($"{gameObject.name} died.");

        Destroy(gameObject, 1.5f);
    }

    public float GetCurrentHp()
    {
        return currentHp;
    }
}
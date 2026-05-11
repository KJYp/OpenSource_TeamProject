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

        Debug.Log($"{gameObject.name} died.");

        Destroy(gameObject, 1.5f);
    }

    public float GetCurrentHp()
    {
        return currentHp;
    }
}
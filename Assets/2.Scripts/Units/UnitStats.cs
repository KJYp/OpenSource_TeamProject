using UnityEngine;

public enum UnitTeam
{
    Ally,
    Enemy
}

public enum AttackType
{
    Melee,
    Ranged
}

public class UnitStats : MonoBehaviour
{
    [Header("Team")]
    public UnitTeam team;

    [Header("Attack Type")]
    public AttackType attackType = AttackType.Melee;

    [Header("Basic Stats")]
    public float maxHp = 100f;
    public float attackPower = 10f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    public float moveSpeed = 2f;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 5f;

    [Header("State")]
    public bool isDead = false;
}
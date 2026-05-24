using UnityEngine;

public enum UnitTeam
{
    Ally,
    Enemy
}

public enum AttackType
{
    Melee,
    Ranged,
    Healer
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

    [Header("Resource")]
    public int manaCost = 10;
    public int killManaReward = 0;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 5f;

    [Header("Heal")]
    public float healPower = 10f;

    [Header("State")]
    public bool isDead = false;

    [Header("Area Attack")]
    public bool isAreaAttack = false;
    public float areaRadius = 1.5f;
    public float areaDamageMultiplier = 1f;

    [Header("Unit Info")]
    public UnitType unitType;
    public int grade = 1;


    public void ApplyBalanceData(UnitGradeStats data)
    {
        if (data == null)
        {
            return;
        }

        grade = data.grade;

        maxHp = data.maxHp;
        attackPower = data.attackPower;
        attackCooldown = data.attackCooldown;
        moveSpeed = data.moveSpeed;
        attackRange = data.attackRange;

        projectileSpeed = data.projectileSpeed;

        areaRadius = data.areaRadius;
        areaDamageMultiplier = data.areaDamageMultiplier;

        healPower = data.healPower;

        manaCost = data.manaCost;
        killManaReward = data.killManaReward;
    }

}
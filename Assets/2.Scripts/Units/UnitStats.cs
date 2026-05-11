using UnityEngine;

public enum UnitTeam
{
    Ally,
    Enemy
}

public class UnitStats : MonoBehaviour
{
    [Header("Team")]
    public UnitTeam team;

    [Header("Basic Stats")]
    public float maxHp = 100f;
    public float attackPower = 10f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    public float moveSpeed = 2f;

    [Header("State")]
    public bool isDead = false;
}
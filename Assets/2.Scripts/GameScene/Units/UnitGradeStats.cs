using System;
using UnityEngine;

[Serializable]
public class UnitGradeStats
{
    [Header("Unit Info")]
    public UnitType unitType;
    public int grade = 1;

    [Header("Basic Stats")]
    public float maxHp;
    public float attackPower;
    public float attackCooldown;
    public float moveSpeed;
    public float attackRange;

    [Header("Projectile")]
    public float projectileSpeed;

    [Header("Area Attack")]
    public float areaRadius;
    public float areaDamageMultiplier = 1f;

    [Header("Heal")]
    public float healPower;

    [Header("Resource")]
    public int manaCost;
    public int killManaReward;
}
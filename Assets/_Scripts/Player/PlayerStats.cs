using UnityEngine;
using static PassiveSkillScriptableObject;

public class PlayerStats : Singleton<PlayerStats>
{
    [Header("Base Stats")]
    public float baseHP = 100f;
    public float baseDamage = 10f;
    public float baseShootCooldown = 1f;
    public float baseAttackRange = 5f;
    public float baseMoveSpeed = 5f;

    [Header("Current Stats (with bonuses)")]
    public float maxHP;
    public float damage;
    public float shootCooldown;
    public float attackRange;
    public float moveSpeed;

    private void Awake()
    {
        ResetStats();
    }

    public void ApplySkill(PassiveSkillScriptableObject skill)
    {
        switch (skill.statType)
        {
            case StatType.HP:
                maxHP += skill.value;
                break;
            case StatType.Damage:
                damage += skill.value;
                break;
            case StatType.ShootCooldown:
                shootCooldown = Mathf.Max(0.1f, shootCooldown - skill.value); // không cho cooldown âm
                break;
            case StatType.AttackRange:
                attackRange += skill.value;
                break;
            case StatType.MoveSpeed:
                moveSpeed += skill.value;
                break;
        }
    }

    public void ResetStats()
    {
        maxHP = baseHP;
        damage = baseDamage;
        shootCooldown = baseShootCooldown;
        attackRange = baseAttackRange;
        moveSpeed = baseMoveSpeed;
    }
}

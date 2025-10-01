using System;
using UnityEngine;
using static PassiveSkillScriptableObject;

public class PlayerStats : Singleton<PlayerStats>
{
    public event Action onMaxHealthChanged;
    public event Action onDamageChanged;
    public event Action onShootCooldownChanged;
    public event Action onAttackRangeChanged;
    public event Action onMoveSpeedChanged;
    public event Action onDodgeChanceChanged;
    public event Action onCritChanceChanged;

    [Header("Base Stats")]
    public float maxHP = 100f;
    public float baseDamage = 10f;
    public float baseShootCooldown = 1.5f;
    public float baseAttackRange = 20f;
    public float baseMoveSpeed = 10f;
    public float baseDodgeChance = 0f; // 0 – 100%
    public float baseCritChance = 0f;  // 0 – 100%

    public void ApplySkill(PassiveSkillScriptableObject skill)
    {
        switch (skill.statType)
        {
            case StatType.HP:
                maxHP += maxHP * (skill.bonusPercent / 100f);
                onMaxHealthChanged?.Invoke();
                break;

            case StatType.Damage:
                baseDamage += baseDamage * (skill.bonusPercent / 100f);
                onDamageChanged?.Invoke();
                break;

            case StatType.ShootCooldown:
                baseShootCooldown *= (1f - skill.bonusPercent / 100f);
                baseShootCooldown = Mathf.Max(0.15f, baseShootCooldown);
                onShootCooldownChanged?.Invoke();
                break;

            case StatType.AttackRange:
                baseAttackRange += baseAttackRange * (skill.bonusPercent / 100f);
                onAttackRangeChanged?.Invoke();
                break;

            case StatType.MoveSpeed:
                baseMoveSpeed += baseMoveSpeed * (skill.bonusPercent / 100f);
                onMoveSpeedChanged?.Invoke();
                break;

            case StatType.Dodge:
                baseDodgeChance += skill.bonusPercent; // cộng thêm tỉ lệ %
                baseDodgeChance = Mathf.Clamp(baseDodgeChance, 0f, 100f);
                onDodgeChanceChanged?.Invoke();
                break;

            case StatType.Crit:
                baseCritChance += skill.bonusPercent; // cộng thêm tỉ lệ %
                baseCritChance = Mathf.Clamp(baseCritChance, 0f, 100f);
                onCritChanceChanged?.Invoke();
                break;
        }
    }
}

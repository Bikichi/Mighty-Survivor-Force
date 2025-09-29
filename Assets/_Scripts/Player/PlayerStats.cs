using System;
using UnityEngine;
using static PassiveSkillScriptableObject;

public class PlayerStats : Singleton<PlayerStats>
{
    // Event riêng cho từng chỉ số, không có tham số
    public event Action onMaxHealthChanged;
    public event Action onDamageChanged;
    public event Action onShootCooldownChanged;
    public event Action onAttackRangeChanged;
    public event Action onMoveSpeedChanged;

    [Header("Base Stats")]
    public float maxHP = 100f;
    public float baseDamage = 10f;
    public float baseShootCooldown = 1.5f;
    public float baseAttackRange = 20f;
    public float baseMoveSpeed = 10f;

    public void ApplySkill(PassiveSkillScriptableObject skill)
    {
        switch (skill.statType)
        {
            case StatType.HP:
                maxHP += skill.value;
                onMaxHealthChanged?.Invoke(); // gọi event khi maxHP thay đổi
                break;
            case StatType.Damage:
                baseDamage += skill.value;
                onDamageChanged?.Invoke();
                break;
            case StatType.ShootCooldown:
                baseShootCooldown = Mathf.Max(0.15f, baseShootCooldown - skill.value);
                onShootCooldownChanged?.Invoke();
                break;
            case StatType.AttackRange:
                baseAttackRange += skill.value;
                onAttackRangeChanged?.Invoke();
                break;
            case StatType.MoveSpeed:
                baseMoveSpeed += skill.value;
                onMoveSpeedChanged?.Invoke();
                break;
        }
    }
}

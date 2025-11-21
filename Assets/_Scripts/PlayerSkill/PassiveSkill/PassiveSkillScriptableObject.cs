using UnityEngine;
//ScriptableObject là một loại asset trong Unity dùng để lưu dữ liệu tách biệt khỏi scene và GameObject
//Lưu data không phụ thuộc vào instance
//Giảm số lượng instance trong RAM → giúp tối ưu game.
[CreateAssetMenu(fileName = "New Passive Skill", menuName = "Skill/Passive Skill")]
public class PassiveSkillScriptableObject : ScriptableObject
{
    public string skillName;
    public string description;
    public Sprite icon;

    public enum StatType
    {
        HP,
        Damage,
        ShootCooldown,
        AttackRange,
        MoveSpeed,
        Dodge,
        Crit,
        CritMultiplier
    }

    [Header("Stat Bonus per Level")]
    public StatType statType;
    public float bonusPercent; // % tăng cho stat này
}

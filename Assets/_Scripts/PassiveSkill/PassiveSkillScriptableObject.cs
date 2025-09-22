using UnityEngine;

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
        MoveSpeed
    }

    [Header("Stat Bonus per Level")]
    public StatType statType; //loại chỉ số mà skill này tăng
    public float value; 
}

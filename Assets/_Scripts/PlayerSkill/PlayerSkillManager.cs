using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    [SerializeField] private List<SkillEntry> learnedSkills = new List<SkillEntry>();

    public SkillModuleManager skillModuleManager;

    // Expose danh sách và số lượng skill đã học
    public List<SkillEntry> LearnedSkills => learnedSkills;
    public int LearnedSkillCount => learnedSkills.Count;

    public void LearnSkill(ScriptableObject skill)
    {
        var entry = learnedSkills.Find(e => e.skill == skill);

        if (entry == null)
        {
            entry = new SkillEntry(skill, 1);
            learnedSkills.Add(entry);
        }
        else
        {
            entry.level++;
        }

        //áp dụng skill và log thông tin theo loại
        if (skill is PassiveSkillScriptableObject passiveSkill)
        {
            PlayerStats.Instance.ApplySkill(passiveSkill);

            switch (passiveSkill.statType)
            {
                case PassiveSkillScriptableObject.StatType.HP:
                    Debug.Log($"Học hoặc nâng cấp Passive Skill: {skill.name}, Level {entry.level} - Máu tối đa = {PlayerStats.Instance.maxHP}");
                    break;
                case PassiveSkillScriptableObject.StatType.Damage:
                    Debug.Log($"Học hoặc nâng cấp Passive Skill: {skill.name}, Level {entry.level} - Sát thương cơ bản = {PlayerStats.Instance.baseDamage}");
                    break;
                case PassiveSkillScriptableObject.StatType.ShootCooldown:
                    Debug.Log($"Học hoặc nâng cấp Passive Skill: {skill.name}, Level {entry.level} - Thời gian hồi bắn = {PlayerStats.Instance.baseShootCooldown}");
                    break;
                case PassiveSkillScriptableObject.StatType.AttackRange:
                    Debug.Log($"Học hoặc nâng cấp Passive Skill: {skill.name}, Level {entry.level} - Tầm đánh = {PlayerStats.Instance.baseAttackRange}");
                    break;
                case PassiveSkillScriptableObject.StatType.MoveSpeed:
                    Debug.Log($"Học hoặc nâng cấp Passive Skill: {skill.name}, Level {entry.level} - Tốc độ di chuyển = {PlayerStats.Instance.baseMoveSpeed}");
                    break;
                case PassiveSkillScriptableObject.StatType.Dodge:
                    Debug.Log($"Học hoặc nâng cấp Passive Skill: {skill.name}, Level {entry.level} - Tỉ lệ né tránh = {PlayerStats.Instance.baseDodgeChance}%");
                    break;
                case PassiveSkillScriptableObject.StatType.Crit:
                    Debug.Log($"Học hoặc nâng cấp Passive Skill: {skill.name}, Level {entry.level} - Tỉ lệ chí mạng = {PlayerStats.Instance.baseCritChance}%");
                    break;
                case PassiveSkillScriptableObject.StatType.CritMultiplier:
                    Debug.Log($"Học hoặc nâng cấp Passive Skill: {skill.name}, Level {entry.level} - Sát thương chí mạng x {PlayerStats.Instance.baseCritMultiplier}");
                    break;
            }
        }
        else if (skill is ActiveSkillScriptableObject activeSkill)
        {
            Debug.Log($"Học skill Active: {activeSkill.skillName}, Level {entry.level}");

            var skillModule = System.Array.Find(skillModuleManager.skillModules, s => s.skillName == skill.name);
            skillModuleManager.UpdateModules(skillModule, entry.level);
        }
    }
    public int GetSkillLevel(ScriptableObject skill)
    {
        var entry = learnedSkills.Find(e => e.skill == skill);
        return entry != null ? entry.level : 0;
    }
}

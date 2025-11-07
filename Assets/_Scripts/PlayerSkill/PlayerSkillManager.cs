using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    [SerializeField] private List<SkillEntry> learnedSkills = new List<SkillEntry>();

    public SkillModuleManager skillModuleManager;

    // Expose danh sách và số lượng skill đã học
    public List<SkillEntry> LearnedSkills => learnedSkills;
    public int LearnedSkillCount => learnedSkills.Count;

    public event Action<ScriptableObject> OnSkillLearned;
    public event Action<ScriptableObject, int> OnSkillLevelUp;

    [SerializeField ]private PlayerStats playerStats;

    private void Awake()
    {
        // Lấy PlayerStats từ CurrentPlayerInstance
        playerStats = ActivePlayerManager.CurrentPlayerInstance.GetComponent<PlayerStats>();
    }

    public void LearnSkill(ScriptableObject skill)
    {
        var entry = learnedSkills.Find(e => e.skill == skill);

        if (entry == null)
        {
            entry = new SkillEntry(skill, 1);
            learnedSkills.Add(entry);
            OnSkillLearned?.Invoke(skill);
        }
        else
        {
            entry.level++;
            OnSkillLevelUp?.Invoke(skill, entry.level);
        }

        //áp dụng skill và log thông tin theo loại
        if (skill is PassiveSkillScriptableObject passiveSkill)
        {
            playerStats.ApplySkill(passiveSkill); // dùng prefab-based stats
        }
        else if (skill is ActiveSkillScriptableObject activeSkill)
        {
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

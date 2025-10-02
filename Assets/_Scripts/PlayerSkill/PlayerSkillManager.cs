using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    [SerializeField] private List<SkillEntry> learnedSkills = new List<SkillEntry>();

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

        //Xử lý theo loại skill dựa vào runtime type
        if (skill is PassiveSkillScriptableObject passiveSkill)
        {
            PlayerStats.Instance.ApplySkill(passiveSkill);
        }
        else if (skill is ActiveSkillScriptableObject activeSkill)
        {
            Debug.Log($"Học skill Active: {activeSkill.skillName}");
        }

        Debug.Log($"Học hoặc nâng cấp kỹ năng: {skill.name}, Level {entry.level}");
    }

    public int GetSkillLevel(ScriptableObject skill)
    {
        var entry = learnedSkills.Find(e => e.skill == skill);
        return entry != null ? entry.level : 0;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassiveSkillEntry
{
    public PassiveSkillScriptableObject skill;
    public int level;
}

public class PlayerPassiveSkillManager : MonoBehaviour
{
    [SerializeField] private List<PassiveSkillEntry> learnedSkills = new List<PassiveSkillEntry>();

    public void LearnPassiveSkill(PassiveSkillScriptableObject skill)
    {
        //Tìm skill trong list
        var entry = learnedSkills.Find(e => e.skill == skill);

        if (entry == null)
        {
            entry = new PassiveSkillEntry { skill = skill, level = 1 };
            learnedSkills.Add(entry);
        }
        else
        {
            entry.level++;
        }

        PlayerStats.Instance.ApplySkill(skill);
        Debug.Log($"Learned {skill.skillName}, Level {entry.level}");
    }

    public int GetSkillLevel(PassiveSkillScriptableObject skill)
    {
        var entry = learnedSkills.Find(e => e.skill == skill);
        return entry != null ? entry.level : 0;
    }

    public void ResetSkills()
    {
        learnedSkills.Clear();
        PlayerStats.Instance.ResetStats();
    }
}


using UnityEngine;

[System.Serializable]
public class SkillEntry
{
    public ScriptableObject skill; // có thể là PassiveSkill hoặc ActiveSkill
    public int level;

    public SkillEntry(ScriptableObject skill, int level = 1)
    {
        this.skill = skill;
        this.level = level;
    }
}
using UnityEngine;
using UnityEngine.UI;

public class LearnedSkillSlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text levelText;

    public void Setup(SkillEntry skillEntry)
    {
        ScriptableObject skill = skillEntry.skill;

        if (skill is ActiveSkillScriptableObject activeSkill)
        {
            icon.sprite = activeSkill.icon;
        }
        else if (skill is PassiveSkillScriptableObject passiveSkill)
        {
            icon.sprite = passiveSkill.icon;
        }

        levelText.text = skillEntry.level >= 4 ? "Max " : skillEntry.level + " ";
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearnedSkillsUIController : MonoBehaviour
{
    [SerializeField] private PlayerSkillManager playerSkillManager;
    [SerializeField] private LearnedSkillSlotUI[] learnedSkillSlots; // kéo thả 5 ô UI từ Inspector

    public void ShowLearnedSkills()
    {
        List<SkillEntry> learnedSkills = playerSkillManager.LearnedSkills;

        for (int i = 0; i < learnedSkillSlots.Length; i++)
        {
            if (i < learnedSkills.Count)
            {
                learnedSkillSlots[i].gameObject.SetChildrenActive(true);
                learnedSkillSlots[i].Setup(learnedSkills[i]);
            }
            else
            {
                learnedSkillSlots[i].gameObject.SetChildrenActive(false);
            }
        }
    }
}

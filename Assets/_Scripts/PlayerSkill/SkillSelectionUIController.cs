using System.Collections.Generic;
using UnityEngine;

public class SkillSelectionUIController : MonoBehaviour
{
    [SerializeField] private SkillSelectionManager skillSelectionManager;

    /// <summary>
    /// Gọi để hiển thị skill random cho người chơi chọn
    /// </summary>
    public void ShowSkillChoices()
    {
        List<ScriptableObject> selectedSkills = skillSelectionManager.GetRandomSkillChoices();

        if (selectedSkills.Count == 0)
        {
            Debug.LogWarning("Không có skill nào để hiển thị!");
            return;
        }

        foreach (var skill in selectedSkills)
        {
            // Nếu skill là Active
            if (skill is ActiveSkillScriptableObject activeSkill)
            {
                Debug.Log($"(Active) {activeSkill.skillName} - {activeSkill.description}");
            }

            // Nếu skill là Passive
            if (skill is PassiveSkillScriptableObject passiveSkill)
            {
                Debug.Log($"(Passive) {passiveSkill.skillName} - {passiveSkill.description} - Tăng {passiveSkill.statType} {passiveSkill.bonusPercent}%");
            }
        }
    }
}

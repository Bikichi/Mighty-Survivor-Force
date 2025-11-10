using System.Collections.Generic;
using UnityEngine;

public class SkillSelectionUIController : MonoBehaviour
{
    [SerializeField] private SkillSelectionManager skillSelectionManager;
    [SerializeField] private PlayerSkillManager playerSkillManager;
    [SerializeField] private SkillChoiceUI[] choiceSlots; //kéo thả 3 ô từ Inspector vào đây
    //mỗi slot là một prefab UI
    //SkillSelectionUIController điền thông tin vào các slot đó bằng Setup()
    public void ShowSkillChoices()
    {
        List<ScriptableObject> selectedSkills = skillSelectionManager.GetRandomSkillChoices();

        for (int i = 0; i < choiceSlots.Length; i++)
        {
            if (i < selectedSkills.Count)
            {
                var skill = selectedSkills[i];
                int currentLevel = playerSkillManager.GetSkillLevel(skill);
                choiceSlots[i].gameObject.SetActive(true);
                choiceSlots[i].Setup(skill, currentLevel);
            }
            else
            {
                choiceSlots[i].gameObject.SetActive(false);
            }
        }
    }
}

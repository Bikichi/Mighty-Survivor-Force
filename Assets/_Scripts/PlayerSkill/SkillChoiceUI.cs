using UnityEngine;
using UnityEngine.UI;

public class SkillChoiceUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text skillNameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text levelText;

    private ScriptableObject currentSkill;
    private PlayerSkillManager playerSkillManager;

    [Header("UI References")]
    [SerializeField] private GameObject skillPanel;

    public void Setup(ScriptableObject skill, int currentLevel, PlayerSkillManager manager)
    {
        currentSkill = skill;
        playerSkillManager = manager;

        // Active Skill
        if (skill is ActiveSkillScriptableObject activeSkill)
        {
            icon.sprite = activeSkill.icon;
            skillNameText.text = activeSkill.skillName;
            if (currentLevel == 0)
            {
                descriptionText.text = activeSkill.description;
            }
            else
            {
                descriptionText.text = activeSkill.upgradeDescription;
            }
        }
        // Passive Skill
        else if (skill is PassiveSkillScriptableObject passiveSkill)
        {
            icon.sprite = passiveSkill.icon;
            skillNameText.text = passiveSkill.skillName;
            descriptionText.text = passiveSkill.description;
        }

        //hiển thị level hiện tại (nếu chưa học thì level = 0)
        levelText.text = (currentLevel + 1) + " "; // gợi ý: +1 vì đang đề xuất nâng cấp tiếp
    }

    public void OnClick()
    {
        Debug.Log("Chọn skill: " + currentSkill.name);

        playerSkillManager.LearnSkill(currentSkill);

        skillPanel.SetActive(false);
    }
}

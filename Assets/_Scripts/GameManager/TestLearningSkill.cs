using UnityEngine;

public class TestLearningSkill : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private PlayerSkillManager skillManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            FindObjectOfType<SkillSelectionManager>().GetRandomSkillChoices();
            FindObjectOfType<LearnedSkillsUIController>().ShowLearnedSkills();
            FindObjectOfType<SkillSelectionUIController>().ShowSkillChoices();
            FindObjectOfType<UIManager>().ShowSkillPanel();
        }
    }
}

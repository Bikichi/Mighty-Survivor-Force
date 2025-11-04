using UnityEngine;

public class ShowLearningSkillUI : MonoBehaviour
{
    private void Awake()
    {
        Invoke("Show", 1.5f);
    }

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

    public void Show()
    {
        FindObjectOfType<SkillSelectionManager>().GetRandomSkillChoices();
        FindObjectOfType<LearnedSkillsUIController>().ShowLearnedSkills();
        FindObjectOfType<SkillSelectionUIController>().ShowSkillChoices();
        FindObjectOfType<UIManager>().ShowSkillPanel();
    }
}

using UnityEngine;

public class ShowLearningSkillUI : MonoBehaviour
{
    [SerializeField] private bool isPlayAtStart;
    private void Awake()
    {
        if (isPlayAtStart)
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

    public void ReRoll()
    {
        FindObjectOfType<SkillSelectionManager>().GetRandomSkillChoices();
        FindObjectOfType<SkillSelectionUIController>().ShowSkillChoices();
    }
}

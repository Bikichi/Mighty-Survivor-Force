using UnityEngine;

public class TestLearningSkill : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private PlayerSkillManager skillManager;

    //private void Start()
    //{
    //    Invoke("Show", 1.5f);
    //}

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

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
            FindObjectOfType<SkillSelectionUIController>().ShowSkillChoices();
            FindAnyObjectByType<UIManager>().ShowSkillPanel(); //SkillPanel sẽ tự show Learned Skills mỗi khi đươc Enable
        }
    }

    public void Show()
    {
        FindObjectOfType<SkillSelectionManager>().GetRandomSkillChoices();
        FindObjectOfType<SkillSelectionUIController>().ShowSkillChoices();
        FindAnyObjectByType<UIManager>().ShowSkillPanel();
    }

    public void ReRoll()
    {
        AudioController.Instance.PlaySound(AudioController.Instance.reroll);
        FindObjectOfType<SkillSelectionManager>().GetRandomSkillChoices();
        FindObjectOfType<SkillSelectionUIController>().ShowSkillChoices();
    }
}

using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject skillPanel;

    public void ShowSkillPanel()
    {
        Time.timeScale = 0f;
        skillPanel.SetActive(true);
    }

    public void HideSkillPanel()
    {
        Time.timeScale = 1f;
        skillPanel.SetActive(false);
    }
}

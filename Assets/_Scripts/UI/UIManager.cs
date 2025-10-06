using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject skillPanel;

    public void ShowSkillPanel()
    {
        skillPanel.SetActive(true);
    }

    public void HideSkillPanel()
    {
        skillPanel.SetActive(false);
    }
}

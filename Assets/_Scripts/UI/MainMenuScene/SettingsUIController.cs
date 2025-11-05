using UnityEngine;

public class SettingsUIController : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void HideSettings()
    {
        settingsPanel.SetActive(false);
    }
}

using UnityEngine;

public class SettingsUIController : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    public void ShowSettings()
    {
        AudioController.Instance.PlaySound(AudioController.Instance.UI_ButtonsClick);
        settingsPanel.SetActive(true);
    }

    public void HideSettings()
    {
        AudioController.Instance.PlaySound(AudioController.Instance.UI_ButtonsClick);
        settingsPanel.SetActive(false);
    }
}

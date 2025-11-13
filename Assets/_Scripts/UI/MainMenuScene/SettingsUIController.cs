using UnityEngine;

public class SettingsUIController : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject buyAdsPanel;

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

    public void ShowBuyAds()
    {
        AudioController.Instance.PlaySound(AudioController.Instance.UI_ButtonsClick);
        buyAdsPanel.SetActive(true);
    }

    public void HideBuyAds()
    {
        AudioController.Instance.PlaySound(AudioController.Instance.UI_ButtonsClick);
        buyAdsPanel.SetActive(false);
    }

}

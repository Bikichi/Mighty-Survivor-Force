using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyNoAdsButton : MonoBehaviour
{
    [SerializeField] private Button buyButton;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private CanvasGroup canvasGroup; // để làm mờ

    private void Start()
    {
        UpdateButtonState();
    }

    public void OnBuyNoAdsButtonClicked()
    {
        AdsInitializer.Instance.BuyNoAds();
        UpdateButtonState();

        Debug.Log("Buy No Ads button clicked.");
    }

    private void UpdateButtonState()
    {
        if (AdsInitializer.Instance.isNoAds)
        {
            buyButton.interactable = false;
            buttonText.text = "PURCHASED";

            canvasGroup.alpha = 0.5f;
        }
        else
        {
            buyButton.interactable = true;
            buttonText.text = "BUY NOW";

            canvasGroup.alpha = 1f;
        }
    }
}

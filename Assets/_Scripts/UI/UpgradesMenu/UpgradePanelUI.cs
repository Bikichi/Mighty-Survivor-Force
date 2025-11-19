using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class UpgradePanelUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject panel;
    public TMP_Text titleText;
    public TMP_Text valueText;
    public TMP_Text currentStatText;
    public Text costText;
    public Image upgradeIcon;

    [Header("Purchase Button UI")]
    public Button purchaseButton;
    public CanvasGroup purchaseButtonCanvasGroup;
    public GameObject purchasedText;

    [Header("Player Models UI")]
    public GameObject[] playerModels;

    [Header("References Button Data")]
    private UpgradeButtonData currentButton;

    public void Show(UpgradeButtonData buttonData, float currentStat)
    {
        currentButton = buttonData;

        titleText.text = buttonData.type.ToString();
        if (buttonData.type == UpgradeButtonData.UpgradeType.Cooldown)
            valueText.text = "-" + buttonData.value;
        else
            valueText.text = "+" + buttonData.value;
        currentStatText.text = $"Current {currentButton.type} Stat: {currentStat}";
        costText.text = buttonData.cost.ToString();
        upgradeIcon.sprite = buttonData.icon;

        ShowPlayerModel(FindObjectOfType<CharacterSelectionManager>().LoadSelectedCharacter());

        UpdatePurchaseButtonUI();

        panel.SetActive(true);
    }


    private void ShowPlayerModel(int characterIndex)
    {
        // Bật model tương ứng, tắt các model còn lại
        for (int i = 0; i < playerModels.Length; i++)
        {
            if (playerModels[i] != null)
                playerModels[i].SetActive(i == characterIndex);
        }
    }
    public void UpdatePurchaseButtonUI()
    {
        bool canPurchase = CoinManager.Instance.totalCoinValue >= currentButton.cost && !currentButton.isPurchased;
        purchaseButton.interactable = canPurchase;
        purchaseButton.gameObject.SetActive(!currentButton.isPurchased);
        purchasedText.gameObject.SetActive(currentButton.isPurchased);

        purchaseButtonCanvasGroup.alpha = canPurchase ? 1f : 0.5f;
        purchaseButtonCanvasGroup.blocksRaycasts = canPurchase;
    }

    public void Close()
    {
        AudioController.Instance.PlaySound(AudioController.Instance.UI_ButtonsClick);
        panel.SetActive(false);
    }

    public UpgradeButtonData GetSelectedUpgradeButon()
    {
        return currentButton;
    }
}

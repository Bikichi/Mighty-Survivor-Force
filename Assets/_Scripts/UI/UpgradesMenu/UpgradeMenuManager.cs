using UnityEngine;

public class UpgradeMenuManager : MonoBehaviour
{
    [Header("All Upgrade Buttons")]
    public UpgradeButtonData[] buttons;

    [Header("Upgrade Panel")]
    public UpgradePanelUI upgradePanel;

    [Header("Character Selection")]
    public CharacterSelectionManager characterSelection;

    private PlayerStats CurrentStats
    {
        get
        {
            int selectedIndex = characterSelection.LoadSelectedCharacter();
            var characterPrefab = characterSelection.characters[selectedIndex].characterPrefab;
            return characterPrefab.GetComponent<PlayerStats>();
        }
    }

    private void Start()
    {
        foreach (var b in buttons)
        {   
            UpgradeButtonData btn = b;

            // Gán listener
            btn.button.onClick.AddListener(() => OnButtonClicked(btn));

            // Cập nhật trạng thái purchased
            if (btn.purchasedBG != null)
                btn.purchasedBG.SetActive(btn.isPurchased);

            btn.button.interactable = true;
        }
    }

    private void OnButtonClicked(UpgradeButtonData button)
    {
        float currentStat = GetCurrentStat(button.type);
        upgradePanel.Show(button, currentStat);
    }

    public void PurchaseSelectedUpgrade()
    {
        var b = upgradePanel.GetSelectedUpgradeButon();
            
        // Trừ coin
        CoinManager.Instance.totalCoinValue -= b.cost;
        CoinManager.Instance.SaveCoinValue();

        ApplyUpgrade(b);

        b.isPurchased = true;
        //b.button.interactable = false;
        if (b.purchasedBG != null)
            b.purchasedBG.SetActive(true);

        upgradePanel.Close();
    }

    private float GetCurrentStat(UpgradeButtonData.UpgradeType type)
    {
        var stats = CurrentStats;
        return type switch
        {
            UpgradeButtonData.UpgradeType.Health => stats.maxHP,
            UpgradeButtonData.UpgradeType.Damage => stats.baseDamage,
            UpgradeButtonData.UpgradeType.Speed => stats.baseMoveSpeed,
            UpgradeButtonData.UpgradeType.Cooldown => stats.baseShootCooldown,
            _ => 0
        };
    }

    private void ApplyUpgrade(UpgradeButtonData b)
    {
        var stats = CurrentStats;

        switch (b.type)
        {
            case UpgradeButtonData.UpgradeType.Health:
                stats.maxHP += b.value;
                stats.NotifyMaxHealthChanged();
                break;

            case UpgradeButtonData.UpgradeType.Damage:
                stats.baseDamage += b.value;
                stats.NotifyDamageChanged();
                break;

            case UpgradeButtonData.UpgradeType.Speed:
                stats.baseMoveSpeed += b.value;
                stats.NotifyMoveSpeedChanged();
                break;

            case UpgradeButtonData.UpgradeType.Cooldown:
                stats.baseShootCooldown = Mathf.Max(0.1f, stats.baseShootCooldown - b.value);
                stats.NotifyShootCooldownChanged();
                break;
        }
    }

}

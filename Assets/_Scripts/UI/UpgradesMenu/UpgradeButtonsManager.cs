using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonsManager : MonoBehaviour
{
    [Header("All Upgrade Buttons")]
    public UpgradeButtonData[] buttons;

    [Header("Upgrade Panel")]
    public UpgradePanelUI upgradePanel;

    [Header("Character Selection")]
    public CharacterSelectionManager characterSelection;

    [SerializeField] private PlayerStats currentStats;

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            var btn = buttons[i];
            
            btn.LoadButtonPurchaseState(i); //gán index và load trạng thái

            //thêm listener khi click
            btn.button.onClick.AddListener(() =>
            {
                LoadStatsForSelectedCharacter();
                OnButtonClicked(btn);
            });

            if (btn.purchasedBG != null)
                btn.purchasedBG.SetActive(btn.isPurchased);

            btn.button.interactable = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayerPrefs();
            Debug.Log("PlayerPrefs đã được reset!");
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

        CoinManager.Instance.totalCoinValue -= b.cost;
        CoinManager.Instance.SaveCoinValue();

        ApplyUpgrade(b);

        b.isPurchased = true;
        b.SavePurchaseState();

        if (b.purchasedBG != null)
            b.purchasedBG.SetActive(true);

        upgradePanel.Close();
    }

    private float GetCurrentStat(UpgradeButtonData.UpgradeType type)
    {
        return type switch
        {
            UpgradeButtonData.UpgradeType.Health => currentStats.maxHP,
            UpgradeButtonData.UpgradeType.Damage => currentStats.baseDamage,
            UpgradeButtonData.UpgradeType.Speed => currentStats.baseMoveSpeed,
            UpgradeButtonData.UpgradeType.Cooldown => currentStats.baseShootCooldown,
            _ => 0
        };
    }

    //áp dụng upgrade cho tất cả nhân vật
    private void ApplyUpgrade(UpgradeButtonData b)
    {
        for (int i = 0; i < characterSelection.characters.Length; i++)
        {
            var characterPrefab = characterSelection.characters[i].characterPrefab;
            var stats = characterPrefab.GetComponent<PlayerStats>();

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

            SaveStatsForCharacter(i, stats);
        }
    }


    //load stats chỉ cho nhân vật hiện tại
    private void LoadStatsForSelectedCharacter()
    {
        int selectedIndex = characterSelection.LoadSelectedCharacter();
        var characterPrefab = characterSelection.characters[selectedIndex].characterPrefab;
        currentStats = characterPrefab.GetComponent<PlayerStats>();

        currentStats.maxHP = PlayerPrefs.GetFloat($"Character_{selectedIndex}_HP", currentStats.maxHP);
        currentStats.baseDamage = PlayerPrefs.GetFloat($"Character_{selectedIndex}_Damage", currentStats.baseDamage);
        currentStats.baseMoveSpeed = PlayerPrefs.GetFloat($"Character_{selectedIndex}_Speed", currentStats.baseMoveSpeed);
        currentStats.baseShootCooldown = PlayerPrefs.GetFloat($"Character_{selectedIndex}_Cooldown", currentStats.baseShootCooldown);
    }


    //gàm lưu dữ liệu cho từng nhân vật
    private void SaveStatsForCharacter(int index, PlayerStats stats)
    {
        PlayerPrefs.SetFloat($"Character_{index}_HP", stats.maxHP);
        PlayerPrefs.SetFloat($"Character_{index}_Damage", stats.baseDamage);
        PlayerPrefs.SetFloat($"Character_{index}_Speed", stats.baseMoveSpeed);
        PlayerPrefs.SetFloat($"Character_{index}_Cooldown", stats.baseShootCooldown);
        PlayerPrefs.Save();
    }

    //lưu toàn bộ nhân vật
    private void SaveCurrentStats()
    {
        for (int i = 0; i < characterSelection.characters.Length; i++)
        {
            var characterPrefab = characterSelection.characters[i].characterPrefab;
            var stats = characterPrefab.GetComponent<PlayerStats>();
            SaveStatsForCharacter(i, stats);
        }
    }

    private void ResetPlayerPrefs()
    {
        for (int i = 0; i < characterSelection.characters.Length; i++)
        {
            PlayerPrefs.DeleteKey($"Character_{i}_HP");
            PlayerPrefs.DeleteKey($"Character_{i}_Damage");
            PlayerPrefs.DeleteKey($"Character_{i}_Speed");
            PlayerPrefs.DeleteKey($"Character_{i}_Cooldown");
        }

        PlayerPrefs.Save();

        foreach (var b in buttons)
        {
            b.isPurchased = false;
            if (b.purchasedBG != null)
                b.purchasedBG.SetActive(false);

            b.button.interactable = true;
        }
    }
}

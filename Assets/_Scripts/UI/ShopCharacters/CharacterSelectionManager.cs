using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionManager : MonoBehaviour
{
    [System.Serializable]
    public class CharacterData
    {
        public string characterName;

        // Model hiển thị trong UI (không phải prefab gameplay)
        public GameObject characterModel;

        // Prefab dùng để spawn trong game scene
        public GameObject characterPrefab;

        public bool isUnlocked;
        public int unlockCost;
    }

    [Header("Character Setup")]
    public CharacterData[] characters;

    [Header("UI References")]
    public TMP_Text txtName;
    public TMP_Text txtAttack;
    public TMP_Text txtAttackRate;
    public TMP_Text txtHealth;
    public TMP_Text txtSpeed;

    [Header("Unlock UI")]
    public GameObject btnUnlock;      // nút Unlock
    public Text txtUnlockCost;        // text giá trên nút
    public Button unlockButton;       // component Button để chỉnh interactable

    private int currentIndex = 0;

    #region Select & Display


    private void Start()
    {
        LoadAllStatsCharacters();
    }
    private void Update()
    {
        // Nhấn R → reset tất cả nhân vật unlock
        if (Input.GetKeyDown(KeyCode.S))
        {
            ResetAllUnlocks();
            ResetAllStatsCharacters();
        }
    }

    public void SelectCharacter(int index)
    {
        currentIndex = index;
        CharacterData c = characters[index];

        // bật model UI
        for (int i = 0; i < characters.Length; i++)
            characters[i].characterModel.SetActive(i == index);

        // lấy stats từ prefab gameplay
        PlayerStats ps = c.characterPrefab.GetComponent<PlayerStats>();

        // cập nhật UI chỉ số từ player stats của prefab
        txtName.text = c.characterName;
        txtAttack.text = ps.baseDamage.ToString();
        txtAttackRate.text = ps.baseShootCooldown.ToString("0.0");
        txtHealth.text = ps.maxHP.ToString("0");
        txtSpeed.text = ps.baseMoveSpeed.ToString();

        // cập nhật nút unlock
        UpdateUnlockUI();

        // chỉ lưu nếu nhân vật đã unlock
        if (c.isUnlocked)
        {
            // ghi lại prefab này để game scene spawn
            ActivePlayerManager.Instance.SetCurrent(c.characterPrefab);
            SaveSelectedCharacter();
        }

    }

    #endregion

    #region Unlock UI

    public void UpdateUnlockUI()
    {
        CharacterData c = characters[currentIndex];

        if (c.isUnlocked)
        {
            btnUnlock.SetActive(false);
            return;
        }

        btnUnlock.SetActive(true);

        txtUnlockCost.text = c.unlockCost.ToString();

        bool canUnlock = CoinManager.Instance.totalCoinValue >= c.unlockCost;
        unlockButton.interactable = canUnlock;

        // làm nút mờ nếu không đủ coin
        CanvasGroup canvasGroup = btnUnlock.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = canUnlock ? 1f : 0.5f;
        }
    }

    public void UnlockCurrentCharacter()
    {
        CharacterData c = characters[currentIndex];

        if (CoinManager.Instance.totalCoinValue >= c.unlockCost)    
        {
            CoinManager.Instance.totalCoinValue -= c.unlockCost;  
            CoinManager.Instance.SaveCoinValue();

            CoinUIManager coinUI = FindObjectOfType<CoinUIManager>();
            coinUI.UpdateCoinUI();

            c.isUnlocked = true;
            ActivePlayerManager.Instance.SetCurrent(c.characterPrefab);

            SaveUnlockStates();
            SaveSelectedCharacter();

            UpdateUnlockUI();

            // cập nhật UI nút CharacterButtonImageManager
            GetComponent<CharacterButtonImageManager>().SetActiveButton(currentIndex);

            AudioController.Instance.PlaySound(AudioController.Instance.unlock);
        }
    }

    #endregion

    #region Save / Load

    private void SaveUnlockStates()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            PlayerPrefs.SetInt("CharacterUnlocked_" + i, characters[i].isUnlocked ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public void LoadUnlockStates()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            int defaultValue = (i == 0) ? 1 : 0; // nhân vật đầu tiên mặc định unlock
            characters[i].isUnlocked = PlayerPrefs.GetInt("CharacterUnlocked_" + i, defaultValue) == 1;
        }
    }

    private void SaveSelectedCharacter()
    {
        PlayerPrefs.SetInt("SelectedCharacter", currentIndex);
        PlayerPrefs.Save();
    }

    public int LoadSelectedCharacter()
    {
        return PlayerPrefs.GetInt("SelectedCharacter", 0); // mặc định nhân vật 0
    }

    private void ResetAllUnlocks()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].isUnlocked = (i == 0); // nhân vật đầu tiên vẫn unlock mặc định
            PlayerPrefs.SetInt("CharacterUnlocked_" + i, characters[i].isUnlocked ? 1 : 0);
        }

        PlayerPrefs.DeleteKey("SelectedCharacter"); // xóa lựa chọn cũ
        PlayerPrefs.Save();

        // cập nhật UI sau khi reset
        SelectCharacter(0); // chọn lại nhân vật đầu tiên
        UpdateUnlockUI();

        // cập nhật nút CharacterButtonImageManager nếu có
        CharacterButtonImageManager btnImg = GetComponent<CharacterButtonImageManager>();
        btnImg?.SetActiveButton(0);

        Debug.Log("Reset all character unlocks!");
    }

    public void LoadAllStatsCharacters()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            var characterPrefab = characters[i].characterPrefab;
            var stats = characterPrefab.GetComponent<PlayerStats>();

            // Kiểm tra xem key đã tồn tại hay chưa, nếu chưa thì dùng default
            stats.maxHP = PlayerPrefs.HasKey($"Character_{i}_HP")
                ? PlayerPrefs.GetFloat($"Character_{i}_HP")
                : stats.defaultMaxHP;

            stats.baseDamage = PlayerPrefs.HasKey($"Character_{i}_Damage")
                ? PlayerPrefs.GetFloat($"Character_{i}_Damage")
                : stats.defaultDamage;

            stats.baseMoveSpeed = PlayerPrefs.HasKey($"Character_{i}_Speed")
                ? PlayerPrefs.GetFloat($"Character_{i}_Speed")
                : stats.defaultMoveSpeed;

            stats.baseShootCooldown = PlayerPrefs.HasKey($"Character_{i}_Cooldown")
                ? PlayerPrefs.GetFloat($"Character_{i}_Cooldown")
                : stats.defaultShootCooldown;

            // Cập nhật lại các notify để gameplay nhận giá trị đúng
            stats.NotifyMaxHealthChanged();
            stats.NotifyDamageChanged();
            stats.NotifyMoveSpeedChanged();
            stats.NotifyShootCooldownChanged();
        }

        //Debug.Log("All character stats have been loaded from PlayerPrefs (or defaults if none exist).");
    }


    // Reset toàn bộ stats của tất cả nhân vật về giá trị gốc (mặc định trong prefab)
    public void ResetAllStatsCharacters()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            // Xóa PlayerPrefs
            PlayerPrefs.DeleteKey($"Character_{i}_HP");
            PlayerPrefs.DeleteKey($"Character_{i}_Damage");
            PlayerPrefs.DeleteKey($"Character_{i}_Speed");
            PlayerPrefs.DeleteKey($"Character_{i}_Cooldown");

            // Lấy prefab nhân vật và component stats
            var characterPrefab = characters[i].characterPrefab;
            var stats = characterPrefab.GetComponent<PlayerStats>();

            // Khôi phục về giá trị gốc trong prefab (nếu bạn có biến default)
            stats.maxHP = stats.defaultMaxHP;
            stats.baseDamage = stats.defaultDamage;
            stats.baseMoveSpeed = stats.defaultMoveSpeed;
            stats.baseShootCooldown = stats.defaultShootCooldown;

            // Gọi Notify để gameplay nhận giá trị đúng
            stats.NotifyMaxHealthChanged();
            stats.NotifyDamageChanged();
            stats.NotifyMoveSpeedChanged();
            stats.NotifyShootCooldownChanged();
        }

        PlayerPrefs.Save();

        Debug.Log("All character stats have been RESET to default values.");
    }

    #endregion
}

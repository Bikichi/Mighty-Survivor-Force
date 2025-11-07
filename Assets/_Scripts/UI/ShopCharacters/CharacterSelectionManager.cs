using UnityEngine;
using TMPro;

public class CharacterSelectionManager : MonoBehaviour
{
    [System.Serializable]
    public class CharacterData
    {
        public string characterName;

        //Model hiển thị trong UI (không phải prefab gameplay)
        public GameObject characterModel;

        //Prefab dùng để spawn trong game scene
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

    private int currentIndex = 0;

    public void SelectCharacter(int index)
    {
        currentIndex = index;
        CharacterData c = characters[index];

        // Bật model UI
        for (int i = 0; i < characters.Length; i++)
            characters[i].characterModel.SetActive(i == index);

        // Lấy stats từ prefab gameplay
        PlayerStats ps = c.characterPrefab.GetComponent<PlayerStats>();

        if (ps == null)
        {
            Debug.LogError("PlayerStats missing on prefab: " + c.characterPrefab.name);
            return;
        }

        // Ghi lại prefab này để gameplay spawn
        ActivePlayerManager.SetCurrent(c.characterPrefab);

        // Cập nhật UI
        txtName.text = c.characterName;
        txtAttack.text = ps.baseDamage.ToString();
        txtAttackRate.text = ps.baseShootCooldown.ToString("0.0");
        txtHealth.text = ps.maxHP.ToString("0");
        txtSpeed.text = ps.baseMoveSpeed.ToString();
    }

    public void UnlockCurrentCharacter()
    {
        CharacterData c = characters[currentIndex];

        if (CoinManager.Instance.totalCoinValue >= c.unlockCost)
        {
            CoinManager.Instance.totalCoinValue -= c.unlockCost;
            CoinManager.Instance.SaveCoinValue();
            c.isUnlocked = true;
        }
    }

    public bool IsCurrentUnlocked()
    {
        return characters[currentIndex].isUnlocked;
    }

    public int GetCurrentUnlockCost()
    {
        return characters[currentIndex].unlockCost;
    }
}

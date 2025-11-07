using UnityEngine;
using TMPro;

public class CharacterSelectionManager : MonoBehaviour
{
    [System.Serializable]
    public class CharacterData
    {
        public string characterName;
        public GameObject characterModel;
        public bool isUnlocked; //trạng thái khoa của nhân vật 
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

    //Bỏ unlockPanel khỏi đây
    // public GameObject unlockPanel;
    //public Text txtUnlockCost;
    //public Button btnUnlock;

    public void SelectCharacter(int index)
    {
        currentIndex = index;
        CharacterData c = characters[index];

        for (int i = 0; i < characters.Length; i++)
            characters[i].characterModel.SetActive(i == index);

        PlayerStats ps = c.characterModel.GetComponent<PlayerStats>();
        if (ps == null)
        {
            Debug.LogError("PlayerStats missing on prefab: " + c.characterModel.name);
            return;
        }

        ActivePlayerManager.SetCurrent(c.characterModel);

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

using UnityEngine;

public class CharacterStatsLoader : MonoBehaviour
{
    [Header("Character Selection Manager")]
    public CharacterSelectionManager characterSelection;

    private void Awake()
    {
        LoadAllStats();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadAllStats();
        }
    }

    public void LoadAllStats()
    {
        //load trực tiếp dữ liệu từ prefab gốc
        for (int i = 0; i < characterSelection.characters.Length; i++)
        {
            var prefab = characterSelection.characters[i].characterPrefab;
            var stats = prefab.GetComponent<PlayerStats>();

            stats.maxHP = PlayerPrefs.GetFloat($"Character_{i}_HP", stats.maxHP);
            stats.baseDamage = PlayerPrefs.GetFloat($"Character_{i}_Damage", stats.baseDamage);
            stats.baseMoveSpeed = PlayerPrefs.GetFloat($"Character_{i}_Speed", stats.baseMoveSpeed);
            stats.baseShootCooldown = PlayerPrefs.GetFloat($"Character_{i}_Cooldown", stats.baseShootCooldown);
        }

    }
}

using UnityEngine;

public class CritManager : Singleton<CritManager>
{
    [Header("Crit Settings")]
    [Range(0f, 100f)]
    public float critChance;
    public float critMultiplier;

    public (float damage, bool isCrit) CalculateCritDamage(float baseDamage)
    {
        PlayerStats playerStats = ActivePlayerManager.Instance.CurrentPlayerInstance.GetComponent<PlayerStats>();
        critChance = playerStats.baseCritChance;
        critMultiplier = playerStats.baseCritMultiplier;
        // Chia 100 để dùng với Random.value (0–1)
        bool isCrit = Random.value < critChance / 100f;
        float finalDamage = isCrit ? baseDamage * critMultiplier : baseDamage;
        return (finalDamage, isCrit);
    }
}

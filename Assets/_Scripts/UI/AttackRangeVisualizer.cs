using UnityEngine;

public class AttackRangeVisualizer : MonoBehaviour
{
    private PlayerStats playerStats;

    private void Start()
    {
        if (ActivePlayerManager.CurrentPlayerInstance == null)
        {
            Debug.LogError("AttackRangeVisualizer: No player instance found!");
            return;
        }

        playerStats = ActivePlayerManager.CurrentPlayerInstance.GetComponent<PlayerStats>();
        if (playerStats == null)
        {
            Debug.LogError("AttackRangeVisualizer: PlayerStats not found on player instance!");
            return;
        }

        UpdateScale();

        playerStats.onAttackRangeChanged += UpdateScale;
    }

    public void UpdateScale()
    {
        if (playerStats == null) return;

        float attackRange = playerStats.baseAttackRange;

        // Tỷ lệ 8:20
        float scaleFactor = 8f / 20f;
        float newScale = attackRange * scaleFactor;

        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    private void OnDestroy()
    {
        if (playerStats != null)
            playerStats.onAttackRangeChanged -= UpdateScale;
    }
}

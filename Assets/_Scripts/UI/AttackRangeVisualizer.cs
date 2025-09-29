using UnityEngine;

public class AttackRangeVisualizer : MonoBehaviour
{
    private void Start()
    {
        UpdateScale();

        PlayerStats.Instance.onAttackRangeChanged += UpdateScale;
    }

    public void UpdateScale()
    {
        float attackRange = PlayerStats.Instance.baseAttackRange;

        // Tỷ lệ 8:20
        float scaleFactor = 8f / 20f;

        float newScale = attackRange * scaleFactor;

        // Dùng transform của chính GameObject này
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    private void OnDestroy()
    {
        if (PlayerStats.Instance != null)
            PlayerStats.Instance.onAttackRangeChanged -= UpdateScale;
    }
}

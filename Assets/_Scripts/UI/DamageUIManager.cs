using UnityEngine;

public class DamageUIManager : Singleton<DamageUIManager>
{
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private Transform uiParnet;
    public void ShowDamageUI(float damage, Collider enemyCollider, bool isCrit = false)
    {
        float defenseValue = 0f;

        //lấy EnemyHealth từ collider truy cập vào defense
        var enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
        defenseValue = enemyHealth.defense;

        float finalDamage = Mathf.Max(damage - defenseValue, 1);

        Vector3 spawnPosition = enemyCollider.bounds.center + new Vector3(0, enemyCollider.bounds.size.y * 0.6f, 0);

        GameObject go = Instantiate(floatingTextPrefab, uiParnet);
        go.transform.position = spawnPosition;

        var floatingText = go.GetComponent<FloatingText>();
        if (floatingText != null)
        {
            Color textColor = isCrit ? Color.red : Color.white;

            floatingText.Setup(Mathf.RoundToInt(finalDamage).ToString(), textColor);
        }
    }
}

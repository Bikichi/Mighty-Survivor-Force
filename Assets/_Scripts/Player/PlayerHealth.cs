using UnityEngine;

public class PlayerHealth : LivingEntity
{
    [Header("Dodge Settings")]
    [SerializeField, Range(0f, 1f)] private float dodgeChance;
    [SerializeField] private GameObject missTextPrefab; // Prefab UI MISS
    [SerializeField] private Transform uiParent; // Canvas hoặc vị trí spawn UI

    public override void TakeDamage(float damage)
    {
        if (Random.value < dodgeChance)
        {
            ShowMissText();
            return;
        }

        base.TakeDamage(damage);
    }

    private void ShowMissText()
    {
        GameObject missObj = Instantiate(missTextPrefab, uiParent);
        Collider colPlayer = GetComponent<Collider>();
        missObj.transform.position = colPlayer.bounds.center + new Vector3(0, colPlayer.bounds.size.y * 0.6f, 0);
        missObj.GetComponent<FloatingText>().Setup("Dodge", Color.yellow);

    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("Player Die!!!");
    }
}

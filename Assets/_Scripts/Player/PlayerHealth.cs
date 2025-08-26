using UnityEngine;

public class PlayerHealth : LivingEntity
{
    [Header("Dodge Settings")]
    [SerializeField, Range(0f, 1f)] private float dodgeChance;
    //[SerializeField] private GameObject missTextPrefab; // Prefab UI MISS
    //[SerializeField] private Transform uiParent; // Canvas hoặc vị trí spawn UI

    public override void TakeDamage(float damage)
    {
        if (Random.value < dodgeChance)
        {
            //ShowMissText();
            Debug.Log("Player dodged the attack!");
            return;
        }

        base.TakeDamage(damage);
    }

    //private void ShowMissText()
    //{
    //    if (missTextPrefab != null && uiParent != null)
    //    {
    //        GameObject missObj = Instantiate(missTextPrefab, uiParent);
    //        missObj.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2f);
    //        missObj.GetComponent<FloatingText>().Setup("MISS");
    //    }
    //}

    protected override void Die()
    {
        base.Die();
        Debug.Log("Player Die!!!");
    }
}

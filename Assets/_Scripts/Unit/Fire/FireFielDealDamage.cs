using UnityEngine;

public class FireFielDealDamage : FieldDealDamageBase
{
    [SerializeField] private float damageIncreasePerTick = 100f; //sát thương cộng thêm mỗi tick nếu còn burn
    [SerializeField] private float burnDuration = 3f;
    [SerializeField] private GameObject burningVFXPrefab;

    protected override void DealDamageAOE()
    {
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, radius);

        foreach (var col in affectedObjects)
        {
            if (col.CompareTag("Enemy"))
            {
                BurningDamageHandler burn = col.GetComponent<BurningDamageHandler>();
                if (burn != null)
                {
                    burn.ApplyBurn(fieldDamage, damageIncreasePerTick, burnDuration, burningVFXPrefab);
                }
            }
        }
    }
}

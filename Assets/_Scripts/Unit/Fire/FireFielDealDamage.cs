using UnityEngine;

public class FireFielDealDamage : FieldDealDamageBase
{
    [SerializeField] private float damageIncreasePerTick;
    [SerializeField] private float burnDuration = 4.5f; //thời gian burn mỗi lần enemy dính lửa
    [SerializeField] private GameObject burningVFXPrefab;

    protected override void OnEnable()
    {
        //cập nhật damage trước khi chạy Coroutine và Destroy
        fieldDamage = PlayerStats.Instance.baseDamage;
        damageIncreasePerTick = 0.5f * fieldDamage;
        base.OnEnable();
    }

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

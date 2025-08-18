using UnityEngine;

public class IceFieldDealDamage : FieldDealDamageBase
{
    [SerializeField] private float baseSlowPercent;           //lần đầu dính băng
    [SerializeField] private float slowIncreasePerTick;       //mỗi lần dính tiếp cộng dồn
    [SerializeField] private float slowDuration;               //thời gian slow tồn tại
    [SerializeField] private GameObject iceVFXPrefab;               //VFX hiệu ứng băng

    protected override void DealDamageAOE()
    {
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, radius);

        foreach (var col in affectedObjects)
        {
            if (col.CompareTag("Enemy"))
            {
                IceSlowHandler slowHandler = col.GetComponent<IceSlowHandler>();
                if (slowHandler != null)
                {
                    slowHandler.ApplySlow(baseSlowPercent, slowIncreasePerTick, slowDuration, iceVFXPrefab);
                }
            }
        }
    }
}

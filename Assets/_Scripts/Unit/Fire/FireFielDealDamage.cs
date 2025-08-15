using UnityEngine;

public class FireFielDealDamage : FieldDealDamageBase
{
    [SerializeField] private float damageIncreasePerTick = 100f;
    [SerializeField] private float currentDamage;

    protected override void OnEnable()
    {
        currentDamage = fieldDamage;
        base.OnEnable();
    }

    protected override void DealDamageAOE()
    {
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, radius);

        foreach (var col in affectedObjects)
        {
            if (col.CompareTag("Enemy"))
            {
                EnemyHealth enemy = col.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(currentDamage);
                }
            }
        }

        // tăng damage sau mỗi lần tick
        currentDamage += damageIncreasePerTick;
    }
}

using System.Collections;
using UnityEngine;

public class FieldDealDamageBase : MonoBehaviour
{
    [SerializeField] protected float radius = 4f;
    [SerializeField] protected float fieldDamage = 100f;     
    [SerializeField] protected float damageInterval = 1f;

    protected Coroutine damageRoutine;

    protected virtual void OnEnable()
    {
        damageRoutine = StartCoroutine(DealDamagePerTime());
    }

    protected virtual void OnDisable()
    {
        if (damageRoutine != null)
            StopCoroutine(damageRoutine);
    }

    protected virtual IEnumerator DealDamagePerTime()
    {
        while (true)
        {
            DealDamageAOE();
            yield return new WaitForSeconds(damageInterval);
        }
    }

    protected virtual void DealDamageAOE()
    {
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, radius);

        foreach (var col in affectedObjects)
        {
            if (col.CompareTag("Enemy"))
            {
                EnemyHealth enemy = col.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(fieldDamage);
                }
            }
        }
    }

    //vẽ bán kính vùng sát thương trong Scene view
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Màu vòng tròn
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

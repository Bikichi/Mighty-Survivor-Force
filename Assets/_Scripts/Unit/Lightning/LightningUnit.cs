using UnityEngine;
using System.Collections.Generic;

public class LightningUnit : MonoBehaviour
{
    public float damagePerHit;            //damage mỗi lần đánh
    public float attackInterval;           //thời gian giữa các lần đánh
    public int targetCount;                 //số lượng enemy tối đa bị đánh
    public GameObject lightningEffectPrefab;  
    public GameObject hitEffectPrefab;          

    public float nextAttackTime;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            AttackEnemies();
            nextAttackTime = Time.time + attackInterval;
        }
    }

    void AttackEnemies()
    {
        EnemyMovement[] closestEnemiesArray = CheckDistance.Instance.GetClosestEnemiesByCount(targetCount);
        foreach (EnemyMovement enemy in closestEnemiesArray)
        {
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();

            //var result = CritManager.Instance.CalculateCritDamage(damagePerHit);
            //health.TakeDamage(result.damage);
            health.TakeDamage(damagePerHit);

            Collider col = enemy.GetComponent<Collider>();
            DamageUIManager.Instance.ShowDamageUI(damagePerHit, col);

            float height = col.bounds.size.y;

            Vector3 lightningPos = enemy.transform.position + Vector3.up * height;  //sinh hiệu ứng ở trên đầu enemy
            Quaternion lightningRotation = Quaternion.Euler(-90f, 0f, 0f);
            GameObject lightning = Instantiate(lightningEffectPrefab, lightningPos, lightningRotation);
            lightning.transform.SetParent(enemy.transform);
            
            Destroy(lightning, 0.5f);

            
            //hiệu ứng hit tại enemy
            Vector3 hitEffectPos = enemy.transform.position + Vector3.up * height * 0.5f;
            GameObject hitEffect = Instantiate(hitEffectPrefab, hitEffectPos, Quaternion.identity);
            hitEffect.transform.SetParent(enemy.transform);
            
            Destroy(hitEffect, 0.3f);
        }
    }

}

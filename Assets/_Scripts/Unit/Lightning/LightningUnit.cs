using UnityEngine;
using System.Collections.Generic;

public class LightningUnit : MonoBehaviour
{
    public float damagePerHit = 20f;            //damage mỗi lần đánh
    public float attackInterval = 1f;           //thời gian giữa các lần đánh
    public int targetCount = 3;                 //số lượng enemy tối đa bị đánh
    public GameObject lightningEffectPrefab;  
    public GameObject hitEffectPrefab;          

    private float nextAttackTime = 0f;

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

            health.TakeDamage(damagePerHit);

            //sinh hiệu ứng ở trên đầu enemy
            Vector3 lightningPos = enemy.transform.position + Vector3.up * 3.1f;
            Quaternion lightningRotation = Quaternion.Euler(-90f, 0f, 0f);
            GameObject lightning = Instantiate(lightningEffectPrefab, lightningPos, lightningRotation);

            Destroy(lightning, 0.5f);

            //hiệu ứng hit tại enemy
            GameObject hitEffect = Instantiate(hitEffectPrefab, enemy.transform.position, Quaternion.identity);
            Destroy(hitEffect, 0.3f);
        }
    }

}

using UnityEngine;
using System.Collections.Generic;

public class LightningUnit : MonoBehaviour
{
    public float damageMultiplier;         // hệ số nhân damage dựa trên PlayerStats
    public float damagePerHit;             // damage mỗi lần đánh
    public float attackInterval;           // thời gian giữa các lần đánh
    public int targetCount;                // số lượng enemy tối đa bị đánh
    public GameObject lightningEffectPrefab;
    public GameObject hitEffectPrefab;

    public float nextAttackTime;

    private PlayerStats playerStats;

    private void OnEnable()
    {
        // Lấy PlayerStats từ CurrentPlayerInstance
        playerStats = ActivePlayerManager.CurrentPlayerInstance.GetComponent<PlayerStats>();

        CalculateDamage(); // gán lần đầu khi bật

        if (playerStats != null)
            playerStats.onDamageChanged += CalculateDamage;

        nextAttackTime = Time.time + attackInterval / 2f;
    }

    private void OnDisable()
    {
        if (playerStats != null)
            playerStats.onDamageChanged -= CalculateDamage;
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            EnemyMovement[] closestEnemiesArray = CheckDistance.Instance.GetClosestEnemiesByCount(targetCount);

            if (closestEnemiesArray != null && closestEnemiesArray.Length > 0)
            {
                AttackEnemies(closestEnemiesArray);
                nextAttackTime = Time.time + attackInterval;
            }
        }
    }

    private void CalculateDamage()
    {
        if (playerStats == null) return;
        damagePerHit = playerStats.baseDamage * damageMultiplier;
    }

    void AttackEnemies(EnemyMovement[] enemies)
    {
        if (enemies == null || enemies.Length == 0) return;

        foreach (EnemyMovement enemy in enemies)
        {
            if (enemy == null) continue;

            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            if (health == null) continue;

            health.TakeDamage(damagePerHit);

            Collider col = enemy.GetComponent<Collider>();
            if (col != null)
            {
                float height = col.bounds.size.y;

                // hiệu ứng tia sét
                Vector3 lightningPos = enemy.transform.position + Vector3.up * height;
                Quaternion lightningRotation = Quaternion.Euler(-90f, 0f, 0f);
                GameObject lightning = Instantiate(lightningEffectPrefab, lightningPos, lightningRotation, enemy.transform);
                Destroy(lightning, 0.5f);

                // hiệu ứng hit
                Vector3 hitEffectPos = enemy.transform.position + Vector3.up * height * 0.5f;
                GameObject hitEffect = Instantiate(hitEffectPrefab, hitEffectPos, Quaternion.identity, enemy.transform);
                Destroy(hitEffect, 0.3f);
            }
        }
    }
}

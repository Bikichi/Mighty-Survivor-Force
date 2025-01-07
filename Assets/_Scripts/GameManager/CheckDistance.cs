using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : Singleton<CheckDistance>
{
    [SerializeField] public float lookAtDistance;  // Khoảng cách tối thiểu để quay về phía kẻ địch

    public float CalculateDistanceFromPlayerToEnemy(Transform playerTransform, Transform enemyTransform)
    {
        float distanceToEnemy = Vector3.Distance(playerTransform.position, enemyTransform.position);
        return distanceToEnemy;
    }

    public bool CheckPlayerEnemyDistance(Transform enemyTransform)
    {
        float distancePlayerToEnemy = CalculateDistanceFromPlayerToEnemy(transform, enemyTransform);
        bool isDetecionDistance = distancePlayerToEnemy <= lookAtDistance;
        return isDetecionDistance;
    }

    public Transform FindTargetEnemy()
    {
        float distanceToClosetEnemy = Mathf.Infinity; // khởi tạo khoảng cách này là vô cực
        float lowestHealth = Mathf.Infinity; // Khởi tạo lượng máu thấp nhất là vô cực
        EnemyMovement targetEnemy = null;
        EnemyMovement[] allEnemies = GameObject.FindObjectsOfType<EnemyMovement>();
        
        if (allEnemies.Length == 0)
        {
            return null;
        }
        
        foreach (EnemyMovement currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - transform.position).magnitude;
            EnemyHealth enemyHealth = currentEnemy.GetComponent<EnemyHealth>(); // lấy ra Component EnemyHealth của GameObject mà Component EnemyMovement đang đại diện
            if (distanceToEnemy < distanceToClosetEnemy)
            {
                distanceToClosetEnemy = distanceToEnemy;
                targetEnemy = currentEnemy;
            }

            else if (distanceToEnemy == distanceToClosetEnemy && enemyHealth.currentHealth < lowestHealth)
            {
                lowestHealth = enemyHealth.currentHealth;
                targetEnemy = currentEnemy;
            }
        }

        return targetEnemy?.transform; // Trả về transform nếu targetEnemies không null
    }
}

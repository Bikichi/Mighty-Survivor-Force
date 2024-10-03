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

    public Transform FindClosestEnemy()
    {
        float distanceToCloseEnemy = Mathf.Infinity; // khởi tạo khoảng cách này là vô cực
        EnemyMovement closestEnemies = null;
        EnemyMovement[] allEnemies = GameObject.FindObjectsOfType<EnemyMovement>();
        
        if (allEnemies.Length == 0)
        {
            Debug.Log("No enemies found.");
            return null;
        }
        
        foreach (EnemyMovement currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - transform.position).magnitude;
            if (distanceToEnemy < distanceToCloseEnemy)
            {
                distanceToCloseEnemy = distanceToEnemy;
                closestEnemies = currentEnemy;
            }
        }

        return closestEnemies?.transform; // Trả về transform nếu closestEnemies không null
    }
}

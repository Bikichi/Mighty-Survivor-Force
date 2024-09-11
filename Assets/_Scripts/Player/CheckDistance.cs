using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : Singleton<CheckDistance>
{
    [SerializeField] public Transform playerTransform;
    [SerializeField] public Transform enemyTransform;
    [SerializeField] public float lookAtDistance;  // Khoảng cách tối thiểu để quay về phía kẻ địch

    public float CalculateDistanceToEnemy()
    {
        float distanceToEnemy = Vector3.Distance(transform.position, enemyTransform.position);
        return distanceToEnemy;
    }

    public bool CheckPlayerEnemyDistance()
    {
        float distanceToEnemy = Vector3.Distance(transform.position, enemyTransform.position);
        bool isWithinRotationDistance = distanceToEnemy <= lookAtDistance;
        return isWithinRotationDistance;
    }

    public void FindClosestEnemy()
    {
        float distanceToCloseEnemy = Mathf.Infinity; // khởi tạo khoảng cách này là vô cực
        EnemyMovement closestEnemies = null;
        EnemyMovement[] allEnenies = GameObject.FindObjectsOfType<EnemyMovement>();
        foreach (EnemyMovement currentEnemy in allEnenies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - playerTransform.position).magnitude;
            if (distanceToEnemy < distanceToCloseEnemy)
            {
                distanceToCloseEnemy = distanceToEnemy;
                closestEnemies = currentEnemy;
            }
        }
    }
}

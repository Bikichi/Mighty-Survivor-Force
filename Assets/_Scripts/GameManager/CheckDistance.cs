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
        float distanceToClosetEnemy = Mathf.Infinity; // Khởi tạo khoảng cách là vô cực
        float targetEnemyHealth = Mathf.Infinity; // Khởi tạo máu mục tiêu là vô cực
        EnemyMovement targetEnemy = null;
        //mục đích khởi tạo 3 biến trên là để con quái vật đầu tiên xuất hiện sẽ luôn là targetEnemy

        EnemyMovement[] allEnemies = GameObject.FindObjectsOfType<EnemyMovement>();

        if (allEnemies.Length == 0)
        {
            return null;
        }

        foreach (EnemyMovement currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - transform.position).magnitude;
            EnemyHealth enemyHealth = currentEnemy.GetComponent<EnemyHealth>(); // Lấy Component EnemyHealth của GameObject mà Component EnemyMovement đang đại diện

            if (enemyHealth == null) continue; // Bỏ qua nếu không có EnemyHealth

            if (distanceToEnemy < distanceToClosetEnemy)
            {
                distanceToClosetEnemy = distanceToEnemy;
                targetEnemy = currentEnemy;
                targetEnemyHealth = enemyHealth.currentHealth; // Cập nhật máu của mục tiêu mới
            }
            else if (distanceToEnemy == distanceToClosetEnemy)
            {
                if (enemyHealth.currentHealth < targetEnemyHealth) // So sánh máu
                {
                    targetEnemy = currentEnemy;
                    targetEnemyHealth = enemyHealth.currentHealth; // Cập nhật máu của mục tiêu mới
                }
            }
        }

        return targetEnemy?.transform; // Trả về transform nếu targetEnemy không null
    }
}

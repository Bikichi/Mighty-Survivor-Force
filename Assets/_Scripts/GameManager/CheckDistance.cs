using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : Singleton<CheckDistance>
{
    public float CalculateDistanceToEnemy(Transform playerTransform, Transform enemyTransform)
    {
        if (playerTransform == null || enemyTransform == null) return 0;
        float distanceToEnemy = Vector3.Distance(playerTransform.position, enemyTransform.position);
        return distanceToEnemy;
    }

    public Transform FindTargetEnemy()
    {
        float distanceToClosetEnemy = Mathf.Infinity; //khởi tạo khoảng cách là vô cực
        float targetEnemyHealth = Mathf.Infinity; //khởi tạo máu mục tiêu là vô cực
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
            EnemyHealth enemyHealth = currentEnemy.GetComponent<EnemyHealth>(); //lấy Component EnemyHealth của GameObject mà Component EnemyMovement đang gắn vào

            if (enemyHealth == null || enemyHealth.IsDead) continue; //bỏ qua nếu không có EnemyHealth hoặc đã chết

            if (distanceToEnemy < distanceToClosetEnemy)
            {
                distanceToClosetEnemy = distanceToEnemy;
                targetEnemy = currentEnemy;
                targetEnemyHealth = enemyHealth.currentHealth;
            }
            else if (distanceToEnemy == distanceToClosetEnemy)
            {
                if (enemyHealth.currentHealth < targetEnemyHealth)
                {
                    targetEnemy = currentEnemy;
                    targetEnemyHealth = enemyHealth.currentHealth; // Cập nhật máu của mục tiêu mới
                }
            }
        }

        return targetEnemy?.transform; //trả về transform nếu targetEnemy không null
    }

    public EnemyMovement[] GetClosestEnemiesByCount(int count)
    {
        //khởi tạo mạng tất cả các enemy trong scene
        EnemyMovement[] allEnemies = GameObject.FindObjectsOfType<EnemyMovement>();

        if (allEnemies.Length == 0)
        {
            return new EnemyMovement[0];
        }

        //lọc enemy còn sống lưu vào danh sách phụ
        List<EnemyMovement> aliveEnemies = new List<EnemyMovement>();
        foreach (EnemyMovement enemy in allEnemies)
        {
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            if (health != null && !health.IsDead)
            {
                aliveEnemies.Add(enemy);
            }
        }

        if (aliveEnemies.Count == 0)
        {
            return new EnemyMovement[0];
        }

        //sắp xếp aliveEnemies theo khoảng cách từ đối tượng hiện tại 
        //thuật toán sủi bọt
        for (int i = 0; i < aliveEnemies.Count - 1; i++)
        {
            for (int j = i + 1; j < aliveEnemies.Count; j++)
            {
                float distI = (aliveEnemies[i].transform.position - transform.position).sqrMagnitude;
                float distJ = (aliveEnemies[j].transform.position - transform.position).sqrMagnitude;

                if (distJ < distI)
                {
                    EnemyMovement temp = aliveEnemies[i];
                    aliveEnemies[i] = aliveEnemies[j];
                    aliveEnemies[j] = temp;
                }
            }
        }

        //lấy số lượng enemy theo yêu cầu (hoặc ít hơn nếu không đủ)
        int takeCount = Mathf.Min(count, aliveEnemies.Count); //trả về số nhỏ nhất giữa 2 số
        //khởi tạo mang lưu lại số lương quái gần nhất theo yêu cầu
        EnemyMovement[] closestEnemiesArray = new EnemyMovement[takeCount];
        for (int i = 0; i < takeCount; i++)
        {
            closestEnemiesArray[i] = aliveEnemies[i];
        }
        return closestEnemiesArray;
    }
}

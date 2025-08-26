using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : Singleton<CheckDistance>
{
    public Transform playerTransform;
    private void Awake()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
    }
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
            EnemyHealth enemyHealth = currentEnemy.GetComponent<EnemyHealth>(); //lấy Component EnemyHealth của GameObject mà Component EnemyMovement đang gắn vào
            if (enemyHealth == null || enemyHealth.IsDead) continue; //bỏ qua nếu không có EnemyHealth hoặc đã chết
            float distanceToEnemy = (currentEnemy.transform.position - playerTransform.position).magnitude;

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
                    targetEnemyHealth = enemyHealth.currentHealth; //cập nhật máu của mục tiêu mới
                }
            }
        }
        return targetEnemy?.transform; //trả về transform nếu targetEnemy không null
    }

    public EnemyMovement[] GetClosestEnemiesByCount(int count)
    {
        if (playerTransform == null) return new EnemyMovement[0];

        EnemyMovement[] allEnemies = GameObject.FindObjectsOfType<EnemyMovement>();
        if (allEnemies.Length == 0) return new EnemyMovement[0];

        //lọc quái còn sống
        List<EnemyMovement> aliveEnemies = new List<EnemyMovement>();
        foreach (EnemyMovement enemy in allEnemies)
        {
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            if (health != null && !health.IsDead)
            {
                aliveEnemies.Add(enemy);
            }
        }

        if (aliveEnemies.Count == 0) return new EnemyMovement[0];

        //sắp xếp theo khoảng cách đến player
        aliveEnemies.Sort((a, b) =>
        {
            float distA = (a.transform.position - playerTransform.position).sqrMagnitude;
            float distB = (b.transform.position - playerTransform.position).sqrMagnitude;
            return distA.CompareTo(distB); //phẩn tử có khoảng cách gần sẽ đẩy lên đầu list
        });

        //lấy số lượng yêu cầu
        int takeCount = Mathf.Min(count, aliveEnemies.Count); //trả về số nhỏ nhất giữa 2 số
        return aliveEnemies.GetRange(0, takeCount).ToArray();
    }
}

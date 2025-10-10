using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckDistance : Singleton<CheckDistance>
{
    [SerializeField] private Transform playerTransform;
    private void Awake()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
    }
    public float CalculateDistanceToPlayer(Transform playerTransform, Transform enemyTransform)
    {
        if (playerTransform == null || enemyTransform == null) return 0;
        float distanceToEnemy = Vector3.Distance(playerTransform.position, enemyTransform.position);
        return distanceToEnemy;
    }

    public Transform FindClosestEnemy()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return null;

        GameObject closest = enemies
            .Where(e => Vector3.Distance(playerTransform.position, e.transform.position) <= PlayerStats.Instance.baseAttackRange) //lọc
            .OrderBy(e => Vector3.Distance(playerTransform.position, e.transform.position)) //sắp xếp tăng dần
            .FirstOrDefault();

        return closest?.transform;
    }

    public Transform FindFarthestEnemy()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return null;

        GameObject farthest = enemies
            .Where(e => Vector3.Distance(playerTransform.position, e.transform.position) <= PlayerStats.Instance.baseAttackRange)
            .OrderByDescending(e => Vector3.Distance(playerTransform.position, e.transform.position)) //sắp xếp giảm dần
            .FirstOrDefault(); // trả về null nếu không có enemy nào trong tầm
        //toán tử lambda, dùng để phân tách tham số (bên trái) và biểu thức/giá trị trả về (bên phải)
        //biểu thức/giá trị trả về (bên phải) là điều kiện/giá trị mà bạn muốn lấy làm khóa để sắp xếp.
        return farthest?.transform;
    }

    public Transform FindLowestHealthEnemy()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return null;

        GameObject lowestHealthEnemy = enemies
            .Select(e => e.GetComponent<EnemyHealth>()) //chuyển đổi từng phần tử trong danh sách, từ GameObject e sang EnemyHealth h
            .Where(h => h != null && Vector3.Distance(playerTransform.position, h.transform.position) <= PlayerStats.Instance.baseAttackRange) //lọc tập hợp, giữ lại chỉ những EnemyHealth không null và trong tầm đánh
            .OrderBy(h => h.currentHealth) //sắp xếp tăng dần
            .FirstOrDefault()?.gameObject; //dùng FirstOrDefault thay vì dùng First vì ở đây có khả năng trả về null vì đã chuyển đổi các phần tử ở Select
        //First dùng ở 2 hàm tìm gàn và xa vì đã có điều kiện  if (enemies.Length == 0) return null; nên chắc chắn không null

        return lowestHealthEnemy != null ? lowestHealthEnemy.transform : null;
        //nếu ở đây trả về null trong Unity không gây lỗi. Unity cho phép một Transform (hoặc GameObject) là null. Chỉ cần đảm bảo rằng trước khi dùng biến này, kiểm tra nó khác null để tránh lỗi NullReferenceException.
    }

    public EnemyMovement[] GetClosestEnemiesByCount(int count)
    {
        if (playerTransform == null) return new EnemyMovement[0];

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return new EnemyMovement[0];

        // Lọc quái còn sống và trong tầm đánh
        var closestEnemies = enemies
            .Select(e => e.GetComponent<EnemyMovement>())
            .Where(e => e != null
                     && !e.GetComponent<EnemyHealth>().IsDead
                     && Vector3.Distance(playerTransform.position, e.transform.position) <= PlayerStats.Instance.baseAttackRange)
            .OrderBy(e => Vector3.Distance(playerTransform.position, e.transform.position))
            .Take(count)
            .ToArray();

        return closestEnemies;
    }

}

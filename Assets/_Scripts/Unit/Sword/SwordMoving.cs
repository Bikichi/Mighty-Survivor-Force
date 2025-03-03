using UnityEngine;

public class SwordMoving : MonoBehaviour
{
    public Transform player;
    public float speed = 10f;            // Tốc độ ban đầu của kiếm
    public float minSpeed = 2f;          // Tốc độ chậm nhất khi gần địch
    public float returnSpeed = 15f;      // Tốc độ quay về Player
    public float stopDistance = 0.1f;    // Khoảng cách dừng trước khi quay lại
    public float penetrationDistance = 1f; // Kiếm sẽ xuyên qua địch 1 đoạn
    [SerializeField] private Transform targetEnemy;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private bool isReturning = false; // Trạng thái kiếm (đang tấn công hay quay về)

    void Update()
    {
        if (!isReturning)
        {
            AttackEnemy();
        }
        else
        {
            ReturnToPlayer();
        }
    }

    void AttackEnemy()
    {
        targetEnemy = CheckDistance.Instance.FindTargetEnemy();

        if (targetEnemy != null)
        {
            // Xác định vị trí kiếm cần lao tới (vượt qua kẻ địch một đoạn)
            targetPosition = targetEnemy.position + (targetEnemy.forward * penetrationDistance);

            // Tính khoảng cách đến mục tiêu
            float distance = Vector3.Distance(transform.position, targetPosition);

            // Giảm tốc khi gần kẻ địch
            float currentSpeed = Mathf.Lerp(minSpeed, speed, distance / penetrationDistance);

            // Di chuyển kiếm về mục tiêu
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

            // Nếu kiếm gần đến mục tiêu, đổi trạng thái quay về
            if (distance <= stopDistance)
            {
                isReturning = true;
            }
        }
    }

    void ReturnToPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Kiếm quay về Player với tốc độ ổn định
        transform.position = Vector3.MoveTowards(transform.position, player.position, returnSpeed * Time.deltaTime);

        // Nếu kiếm về đến Player, reset trạng thái
        if (distance <= stopDistance)
        {
            isReturning = false;
        }
    }
}

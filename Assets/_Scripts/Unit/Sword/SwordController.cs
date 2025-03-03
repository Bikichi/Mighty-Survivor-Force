using UnityEngine;

public class SwordController : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 5f;
    public float rotationSpeed = 5f;
    public Vector3 baseOffset = new Vector3(0, 0, 0);
    public Vector3 currentOffset;

    // Biến cho cơ chế tấn công
    public float attackSpeed = 10f;
    public float minAttackSpeed = 2f;
    public float returnSpeed = 15f;
    public float stopDistance = 0.5f;
    public float penetrationDistance = 1f;
    public Transform targetEnemy;
    private Vector3 targetPosition;
    public bool isReturning = false;
    public bool isAttacking = false;

    // 🔥 Thêm biến delay giữa các lần tấn công
    public float attackCooldown = 1.5f; // Delay giữa mỗi lần tấn công (giây)
    private float lastAttackTime = -Mathf.Infinity; // Lần tấn công gần nhất

    // 🔥 Thêm biến tầm đánh
    public float attackRange = 8f; // Kiếm chỉ tấn công nếu kẻ địch trong phạm vi này

    void Start()
    {
        currentOffset = baseOffset;
    }

    void Update()
    {
        RotateSword();

        if (!isAttacking && !isReturning)
        {
            Transform targetEnemy = CheckDistance.Instance.FindTargetEnemy();
            if (targetEnemy != null && Time.time >= lastAttackTime + attackCooldown) // 🔥 Kiểm tra delay
            {
                float distanceToEnemy = Vector3.Distance(transform.position, targetEnemy.position);
                if (distanceToEnemy <= attackRange) // 🔥 Kiểm tra tầm đánh
                {
                    StartAttack();
                }
            }
        }

        if (isAttacking && !isReturning)
        {
            AttackEnemy();
        }
        else if (isReturning && !isAttacking)
        {
            ReturnToPlayer();
        }
        else
        {
            UpdateOffset();
            FollowPlayer();
        }
    }

    void UpdateOffset()
    {
        Vector3 newOffset = Quaternion.Euler(0, player.rotation.eulerAngles.y, 0) * baseOffset;
        currentOffset = Vector3.Lerp(currentOffset, newOffset, Time.deltaTime * followSpeed);
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            transform.position = player.position + currentOffset;
        }
    }

    void RotateSword()
    {
        var targetEnemy = CheckDistance.Instance.FindTargetEnemy();
        if (targetEnemy != null)
        {
            Vector3 directionToEnemy = targetEnemy.position - transform.position;
            directionToEnemy.y = 0;

            if (directionToEnemy != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
                Quaternion newRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y - 90, 264);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            Quaternion newRotation = Quaternion.Euler(0, player.rotation.eulerAngles.y - 90, 264);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void StartAttack()
    {
        targetEnemy = CheckDistance.Instance.FindTargetEnemy();
        if (targetEnemy != null)
        {
            isAttacking = true;
            isReturning = false;
            lastAttackTime = Time.time; // 🔥 Cập nhật thời gian tấn công gần nhất
            targetPosition = targetEnemy.position + (targetEnemy.forward * penetrationDistance);
        }
    }

    void AttackEnemy()
    {
        if (targetEnemy == null)
        {
            isAttacking = false;
            isReturning = true;
            return;
        }

        float distance = Vector3.Distance(transform.position, targetPosition);
        float currentSpeed = Mathf.Lerp(minAttackSpeed, attackSpeed, distance / penetrationDistance);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

        if (distance <= stopDistance)
        {
            isAttacking = false;
            isReturning = true;
        }
    }

    void ReturnToPlayer()
    {
        Vector3 returnPosition = player.position + currentOffset;
        float distance = Vector3.Distance(transform.position, returnPosition);
        transform.position = Vector3.MoveTowards(transform.position, returnPosition, returnSpeed * Time.deltaTime);

        if (distance <= stopDistance)
        {
            isReturning = false;
        }
    }
}

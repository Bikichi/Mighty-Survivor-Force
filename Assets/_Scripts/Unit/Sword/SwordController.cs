using UnityEngine;

public class SwordController : MonoBehaviour
{
    [Header("Player & Movement")]
    public Transform player;
    public float followSpeed = 5f;
    public Vector3 baseOffset = new Vector3(0, 0, 0);
    public Vector3 currentOffset;

    [Header("Rotation")]
    public float rotationSpeed = 5f;
    public float rotationReturnSpeed = 10f;

    [Header("Attack Settings")]
    public float attackSpeed = 10f;
    public float attackDamage;
    public float minAttackSpeed = 2f;
    public float attackRange = 8f;
    public float distanceToEnemy;
    public float penetrationDistance = 1f; //kiếm sẽ lao xuyên qua quái vật 1 khoảng bao nhiêu
    public float stopDistance = 0.5f;

    [Header("Cooldown & Delay")]
    public float attackCooldown = 1.5f;
    private float lastAttackTime = -Mathf.Infinity;

    [Header("Return Settings")]
    public float returnSpeed = 15f;
    public bool isReturning = false;
    public bool isAttacking = false;

    [Header("Target Tracking")]
    public Transform targetEnemy;
    private Vector3 targetPosition;

    void Start()
    {
        currentOffset = baseOffset;
    }

    void Update()
    {
        targetEnemy = CheckDistance.Instance.FindTargetEnemy();
        distanceToEnemy = CheckDistance.Instance.CalculateDistanceToEnemy(transform, targetEnemy);

        UpdateOffset();
        RotateSword();

        FollowPlayer();

        StartAttack();
        AttackEnemy();
        ReturnToPlayer();
    }

    void UpdateOffset()
    {
        Vector3 newOffset = Quaternion.Euler(0, player.rotation.eulerAngles.y, 0) * baseOffset;
        currentOffset = Vector3.Lerp(currentOffset, newOffset, Time.deltaTime * followSpeed);
    }

    void FollowPlayer()
    {
        if (player != null && !isAttacking && !isReturning)
        {
            transform.position = player.position + currentOffset;
        }
    }

    void RotateSword()
    {
        if (isReturning) return;

        if (targetEnemy != null && distanceToEnemy <= attackRange)
        {
            Vector3 directionToEnemy = targetEnemy.position - transform.position;
            directionToEnemy.y = 0;

            if (directionToEnemy != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
                Quaternion newRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y - 90, 270); //do model thanh kiếm ban đầu là hướng thẳng đứng nên:
                                                                                                       //+ trừ đi 90 độ ở trục Y để mũi kiếm hướng theo Player
                                                                                                      //+ giữ trục Z là 270 để lưỡi kiếm hướng xuống
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            Quaternion newRotation = Quaternion.Euler(0, player.rotation.eulerAngles.y - 90, 270);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void StartAttack()
    {
        if (!isAttacking && !isReturning)
        {
            if (targetEnemy != null)
            {
                bool readyToAttack = Time.time >= lastAttackTime + attackCooldown;
                if (distanceToEnemy <= attackRange && readyToAttack)
                {
                    isAttacking = true;
                    isReturning = false;
                    lastAttackTime = Time.time;

                    float enemyHeight = targetEnemy.GetComponent<Collider>().bounds.size.y;
                    Vector3 enemyHeadPosition = targetEnemy.position + new Vector3(0, enemyHeight / 2, 0);

                    Vector3 directionToEnemy = (targetEnemy.position - transform.position).normalized;
                    targetPosition = enemyHeadPosition + (directionToEnemy * penetrationDistance);
                }
            }
        }
    }

    void AttackEnemy()
    {
        if (isAttacking && !isReturning)
        {
            float distance = Vector3.Distance(transform.position, targetPosition);
            float currentSpeed = Mathf.Lerp(minAttackSpeed, attackSpeed, distance / penetrationDistance);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

            if (distance <= stopDistance)
            {
                isAttacking = false;
                isReturning = true;
            }
        }
    }

    void ReturnToPlayer()
    {
        if (!isAttacking && isReturning)
        {
            Vector3 returnPosition = player.position + currentOffset;

            float distance = Vector3.Distance(transform.position, returnPosition);
            transform.position = Vector3.MoveTowards(transform.position, returnPosition, returnSpeed * Time.deltaTime);

            Vector3 directionToReturn = returnPosition - transform.position;
            directionToReturn.y = 0; //đảm bảo chỉ xoay trên mặt phẳng ngang

            if (directionToReturn != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToReturn);
                Quaternion newRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y - 90, 270);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationReturnSpeed * Time.deltaTime);
            }

            if (distance <= stopDistance)
            {
                isReturning = false;
            }
        }
    }
}

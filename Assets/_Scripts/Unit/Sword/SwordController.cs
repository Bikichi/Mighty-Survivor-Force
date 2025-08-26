using UnityEngine;

public class SwordController : UnitFollowerBase
{
    [Header("Sword Rotation")]
    public float rotationReturnSpeed = 10f;

    [Header("Sword Attack Settings")]
    public float penetrationDistance = 1f;
    public float stopDistance = 0.5f;
    public float attackSpeed = 50f;
    public float minAttackSpeed = 20f;

    [Header("Cooldown & Delay")]
    public float attackCooldown;
    private float lastAttackTime;

    [Header("Return Settings")]
    public float returnSpeed = 15f;
    public bool isReturning = false;
    public bool isAttacking = false;

    private Vector3 targetPosition;

    protected override void Start()
    {
        base.Start();
        lastAttackTime = -attackCooldown / 2;
    }
    protected override void Update()
    {
        base.Update();

        StartAttack();
        AttackEnemy();
        ReturnToPlayer();
    }

    protected override void FollowPlayer()
    {
        if (player != null && !isAttacking && !isReturning)
        {
            transform.position = player.position + currentOffset;
        }
    }

    protected override void RotateUnit()
    {
        if (isReturning) return;

        if (targetEnemy != null && distanceToEnemy <= attackRange)
        {
            Vector3 directionToEnemy = targetEnemy.position - transform.position;
            directionToEnemy.y = 0;

            if (directionToEnemy != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
                Quaternion newRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y - 90, 270);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else if (player != null)
        {
            Quaternion newRotation = Quaternion.Euler(0, player.rotation.eulerAngles.y - 90, 270);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void StartAttack()
    {
        if (!isAttacking && !isReturning && targetEnemy != null)
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

    private void AttackEnemy()
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

    private void ReturnToPlayer()
    {
        if (!isAttacking && isReturning)
        {
            Vector3 returnPosition = player.position + currentOffset;
            float distance = Vector3.Distance(transform.position, returnPosition);
            transform.position = Vector3.MoveTowards(transform.position, returnPosition, returnSpeed * Time.deltaTime);

            Vector3 directionToReturn = returnPosition - transform.position;
            directionToReturn.y = 0;

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
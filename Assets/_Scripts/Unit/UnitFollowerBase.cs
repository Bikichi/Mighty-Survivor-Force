using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFollowerBase : MonoBehaviour
{
    [Header("Player & Movement")]
    public Transform player;
    public float followSpeed = 5f;
    public Vector3 baseOffset = Vector3.zero;
    protected Vector3 currentOffset;

    [Header("Rotation")]
    public float rotationSpeed = 5f;
    public float attackRange;
    protected float distanceToEnemy;

    [Header("Target Tracking")]
    public Transform targetEnemy;

    protected virtual void Start()
    {
        GetTransformPlayer();

        UpdateAttackRange(); //gán ban đầu

        PlayerStats.Instance.onAttackRangeChanged += UpdateAttackRange;
    }

    protected virtual void Update()
    {
        UpdateTargetEnemy();
        UpdateDistanceToEnemy();
        UpdateOffset();
        FollowPlayer();
        RotateUnit();
    }

    private void OnDestroy()
    {
        if (PlayerStats.Instance != null)
            PlayerStats.Instance.onAttackRangeChanged += UpdateAttackRange;
    }

    protected virtual void GetTransformPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void UpdateTargetEnemy()
    {
        targetEnemy = CheckDistance.Instance.FindTargetEnemy();
    }

    protected virtual void UpdateDistanceToEnemy()
    {
        distanceToEnemy = CheckDistance.Instance.CalculateDistanceToEnemy(transform, targetEnemy);
    }

    protected virtual void UpdateOffset()
    {
        Vector3 newOffset = Quaternion.Euler(0, player.rotation.eulerAngles.y, 0) * baseOffset;
        currentOffset = Vector3.Lerp(currentOffset, newOffset, Time.deltaTime * followSpeed);
    }
    private void UpdateAttackRange()
    {
        attackRange = PlayerStats.Instance.baseAttackRange;
    }

    protected virtual void FollowPlayer()
    {
        if (player != null)
        {
            transform.position = player.position + currentOffset;
        }
    }

    protected virtual void RotateUnit()
    {
        if (targetEnemy != null && distanceToEnemy <= attackRange)
        {
            Vector3 directionToEnemy = targetEnemy.position - transform.position;
            directionToEnemy.y = 0;

            if (directionToEnemy != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else if (player != null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, rotationSpeed * Time.deltaTime);
        }
    }
}

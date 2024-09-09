using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public CharacterController characterController;
    public Transform playerTransform;
    public Transform enemyTransform;
    public Transform movePointerTransform;
    [SerializeField] private float rotationSpeed; 
    [SerializeField] private float rotateDistance; // Khoảng cách tối thiểu để quay về phía kẻ địch

    void Update()
    {
        RotatePlayerTowardsEnemy();
    }

    public void RotateInDirection(Vector3 direction, Transform transform)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void RotatePlayerTowardsEnemy()
    {
        float distanceToEnemy = Vector3.Distance(transform.position, enemyTransform.position); //Khoảng cách từ Player đến Enemy
        if (distanceToEnemy <= rotateDistance)
        {
            Vector3 directionToEnemy = enemyTransform.position - transform.position; //Hướng từ Player đến Enemy
            RotateInDirection(directionToEnemy, playerTransform);
        } else if (characterController.velocity != Vector3.zero && distanceToEnemy > rotateDistance)
        {
            RotarePlayerInDirection();
        }
    }

    public void RotarePlayerInDirection()
    {
        RotateInDirection(characterController.velocity, playerTransform);
    }

    public void RotareMovePointerInDirection()
    {
        RotateInDirection(characterController.velocity, movePointerTransform);
    }
}

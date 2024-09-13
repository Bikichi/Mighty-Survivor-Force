using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public CharacterController characterController;

    public Transform enemyTransform;
    public Transform movePointerTransform;
    
    [SerializeField] private float rotationSpeed;

    void Update()
    {
        RotateMovePointerInDirection();
        RotatePlayerInDirection();
    }

    public void RotateInDirection(Vector3 direction, Transform transform)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        // Quaternion.LookRotation tạo ra một góc quay theo hướng mong muốn với trục y được giữ theo hướng Vector3.up
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //transform.rotation = targetRotation;    
    }

    public void RotateInMoveDirection(Transform transform)
    {
        Vector3 moveDirection = characterController.velocity;
        moveDirection.y = 0; // Chỉ xoay trục y mà không muốn xoay các trục xz bằng cách đặt y = 0
        // Kiểm tra xem velocity có phải Vector3.zero hay không
        // Khi gọi Quaternion.LookRotation với một vector có độ dài bằng 0, Unity không thể xác định hướng, dẫn tới lỗi.
        if (moveDirection != Vector3.zero)
        {
            // Nếu nhân vật đang di chuyển, xoay movePointer theo hướng di chuyển
            RotateInDirection(moveDirection, transform);
        }
    }

    public void RotateMovePointerInDirection()
    {
        RotateInMoveDirection(movePointerTransform);

    }
    public void RotatePlayerInDirection()
    {

        if (CheckDistance.Instance.CheckPlayerEnemyDistance(CheckDistance.Instance.FindClosestEnemy()))
        {
            RotatePlayerTowardsEnemy();
        }
        else
        {
            RotarePlayerInMoveDirection();
        }
    }

    public void RotatePlayerTowardsEnemy()
    {
        Vector3 directionToEnemy = enemyTransform.position - transform.position; //Hướng từ Player đến Enemy
        RotateInDirection(directionToEnemy, transform);
    }

    public void RotarePlayerInMoveDirection()
    {
        RotateInMoveDirection(transform);
    }
}

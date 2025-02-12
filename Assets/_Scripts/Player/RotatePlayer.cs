using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public CharacterController characterController;

    public Transform movePointerTransform;
    
    //[SerializeField] private float rotationSpeed;

    public float rotateTimer;
    public float rotateInterval;

    void Update()
    {
        RotateMovePointerInDirection();
        RotatePlayerInDirection();
    }

    public void RotateInDirection(Vector3 direction, Transform transform)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        // Quaternion.LookRotation tạo ra một góc quay theo hướng mong muốn với trục y được giữ theo hướng Vector3.up
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = targetRotation;
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

    public void RotatePlayerToTargetEnemy()
    {
        Vector3 directionToEnemy = CheckDistance.Instance.FindTargetEnemy().position - transform.position; //Hướng từ Player đến Enemy
        RotateInDirection(directionToEnemy, transform);
    }

    public void RotarePlayerInMoveDirection()
    {
        RotateInMoveDirection(transform);
    }

    public void RotatePlayerInDirection()
    {
        var targetEnemy = CheckDistance.Instance.FindTargetEnemy();
        if (targetEnemy != null)
        {
            rotateTimer += Time.deltaTime;
            if (rotateTimer >= rotateInterval)
            {
                RotatePlayerToTargetEnemy(); //targetEnemy không đổi thì hàm này thực chất vẫn chạy chỉ cần thoả mãn điều kiện if
                //targetEnemy không đổi tức là lúc này góc quay là 0 độ nên thực tế trên scene không nhìn ra được là nó đang chạy @@
                rotateTimer = 0f;
            }
        }
        else
        {
            RotarePlayerInMoveDirection();
        }
    }
}

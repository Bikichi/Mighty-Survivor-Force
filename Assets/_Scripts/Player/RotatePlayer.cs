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
    [SerializeField] public float lookAtDistance;  // Khoảng cách tối thiểu để quay về phía kẻ địch

    void Update()
    {
        RotateMovePointerInDirection();
        RotatePlayerInDirection();
    }

    public void RotateInDirection(Vector3 direction, Transform transform)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        // Quaternion.LookRotation tạo ra một góc quay theo hướng mong muốn với trục y được giữ theo hướng Vector3.up
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20f * Time.deltaTime);
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
        rotateTimer += Time.deltaTime;
        bool isReadyToRotate = rotateTimer >= rotateInterval;
        var targetEnemy = CheckDistance.Instance.FindTargetEnemy();
        bool canSeeEnemy = CheckDistance.Instance.CalculateDistanceToEnemy(transform, targetEnemy) <= lookAtDistance;
        if (targetEnemy != null && canSeeEnemy)
        {
            if (isReadyToRotate)
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

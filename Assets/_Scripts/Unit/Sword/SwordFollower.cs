using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFollower : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed;
    public Vector3 baseOffset = new Vector3(0, 0, 0);
    private Vector3 currentOffset;

    void Start()
    {
        currentOffset = baseOffset;
    }

    void Update()
    {
        UpdateOffset(); 
        FollowPlayer(); 
        RotateSword();
    }

    void UpdateOffset()
    {
        //tính toán offset dựa trên hướng của Player
        Vector3 newOffset = Quaternion.Euler(0, player.rotation.eulerAngles.y, 0) * baseOffset;

        //chỉ thay đổi offset dần dần giúp chuyển động mượt hơn
        currentOffset = Vector3.Lerp(currentOffset, newOffset, Time.deltaTime * rotationSpeed);
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
            directionToEnemy.y = 0; //loại bỏ position.y khi tính hướng để tránh nghiêng lên/xuống khi quay
            //giữ lại trục x,z để chỉ tính hướng trên mặt phẳng

            if (directionToEnemy != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
                //do model thanh kiếm ban đầu là hướng thẳng đứng nên:
                //+ trừ đi 90 độ ở trục Y để mũi kiếm hướng theo Player
                //+ giữ trục Z là 264 để lưỡi kiếm hướng xuống
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
}
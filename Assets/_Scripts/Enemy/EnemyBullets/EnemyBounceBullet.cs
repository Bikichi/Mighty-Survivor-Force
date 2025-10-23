using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBounceBullet : BaseBullet
{
    private Rigidbody rb;
    private Vector3 moveDir;
    private int bounceCount = 0;          //số lần nảy
    [SerializeField] private int maxBounce = 4; //nảy tối đa 4 lần

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();

        moveDir = transform.forward.normalized;
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.velocity = moveDir * _speedBullet;
    }

    protected override void Update()
    {
        SmoothRotateTowardsMoveDir();
    }

    protected override void MoveBullet()
    {
        //không cần di chuyển thủ công, Rigidbody tự di chuyển bằng velocity
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag(Const.WALL_TAG))
        {
            Bounce(col.contacts[0].normal);
            //col.contacts là mảng các điểm tiếp xúc (ContactPoint[]) tại vị trí va chạm
            //ContactPoint có các thuộc tính: 
            //point: vị trí va chạm trong thế giới (world position)
            //normal: vector pháp tuyến tại điểm va chạm (hướng vuông góc bề mặt tại điểm va chạm)
        }
    }
    protected override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        if (col.CompareTag(Const.PLAYER_TAG))
        {
            PlayerHealth playerHealth = col.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.TakeDamage(damageBullet);
            Destroy(gameObject);
        }
    }
    private void Bounce(Vector3 normal)
    {
        bounceCount++;

        if (bounceCount >= maxBounce)
        {
            Destroy(gameObject);
            return;
        }

        moveDir = Vector3.Reflect(moveDir, normal).normalized;
        //Reflect là hàm tính hướng phản xạ của một vector khi đập vào bề mặt có pháp tuyến normal
        //Reflected = direction - 2 * (direction ⋅ normal) * normal

        rb.velocity = moveDir * _speedBullet;
    }

    private void SmoothRotateTowardsMoveDir()
    {
        if (moveDir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 30f);
        }
    }

}

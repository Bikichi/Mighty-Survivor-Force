using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBullet : BaseBullet
{
    private Vector3 moveDir;

    protected override void Start()
    {
        base.Start();
        moveDir = transform.forward.normalized;
    }

    protected override void MoveBullet()
    {
        transform.position += moveDir * _speedBullet * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag(Const.PLAYER_TAG))
        {
            PlayerHealth playerHealth = col.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageBullet);
            }

            Destroy(gameObject);
        }
        // Va vào tường → nảy ngược hướng
        else if (col.collider.CompareTag(Const.WALL_TAG))
        {
            BounceBack(col);
        }
    }

    private void BounceBack(Collision col)
    {
        // Lấy normal (hướng pháp tuyến) của mặt va chạm
        Vector3 normal = col.contacts[0].normal;

        // Phản xạ vector chuyển động theo hướng ngược lại
        moveDir = Vector3.Reflect(moveDir, normal).normalized;

        // Cập nhật hướng nhìn để khớp hướng mới
        transform.rotation = Quaternion.LookRotation(moveDir);
    }
}

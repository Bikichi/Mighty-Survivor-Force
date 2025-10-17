using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBullet : BaseBullet
{
    private Vector3 moveDir;                        // hướng hiện tại

    protected override void Start()
    {
        base.Start();
        moveDir = transform.forward; 
    }

    protected override void MoveBullet()
    {
        transform.position += moveDir * _speedBullet * Time.deltaTime;
    }

    protected override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);

        // Va chạm với Player
        if (col.CompareTag(Const.PLAYER_TAG))
        {
            PlayerHealth playerHealth = col.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageBullet);
            }

            Destroy(gameObject); 
        }
    }
}

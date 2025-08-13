using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedShooterBase : MonoBehaviour
{
    protected const string shootParaname = "Shoot";

    [Header("Shooting Settings")]
    [SerializeField] protected GameObject bulletPrefabs;
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected float shootingRange;
    [SerializeField] protected float shootCooldown;

    [Header("Optional Animator")]
    [SerializeField] protected Animator anim;

    [SerializeField] protected float lastShootTime;

    protected virtual void Start()
    {
        if (anim == null)
            anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        ShootBullet();
    }

    protected virtual void ShootBullet()
    {
        Transform targetEnemy = CheckDistance.Instance.FindTargetEnemy();
        if (targetEnemy == null || targetEnemy.GetComponent<EnemyHealth>().IsDead)
            return;

        float distance = CheckDistance.Instance.CalculateDistanceToEnemy(transform, targetEnemy);
        bool isReadyToShoot = Time.time - lastShootTime > shootCooldown;

        if (isReadyToShoot && distance <= shootingRange)
        {
            if (anim != null)
                anim.SetTrigger(shootParaname);

            Instantiate(bulletPrefabs, shootPoint.position, shootPoint.rotation);
            lastShootTime = Time.time;
        }
    }
}
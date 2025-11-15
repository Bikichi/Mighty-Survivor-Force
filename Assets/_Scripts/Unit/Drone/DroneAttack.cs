using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttack : RangedShooterBase
{
    [SerializeField] private float multiShootDelay = 0.2f;
    [SerializeField] private int bulletCount = 2;
    [SerializeField] public Transform targetEnemy;
    protected override void ShootBullet()
    {
        targetEnemy = CheckDistance.Instance.FindLowestHealthEnemy();
        if (targetEnemy == null || targetEnemy.GetComponent<EnemyHealth>().IsDead)
            return;

        bool isReadyToShoot = Time.time - lastShootTime > shootCooldown;

        if (isReadyToShoot)
        {
            if (anim != null)
                anim.SetTrigger(shootParaname);

            StartCoroutine(MultiShoot(bulletCount));
            lastShootTime = Time.time;
        }
    }

    protected virtual IEnumerator MultiShoot(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(bulletPrefabs, shootPoint.position, shootPoint.rotation);
            AudioController.Instance.PlaySound(AudioController.Instance.droneShot);
            if (i < count - 1) //khi viên cuối bắn ra thì không cần chờ nữa
                yield return new WaitForSeconds(multiShootDelay);
        }
    }
}

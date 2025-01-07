using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefabs;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float shootingInterval;
    [SerializeField] private float _delayShoot = 1.5f;

    private void Update()
    {
        ShootBullet();
    }
    public void ShootBullet()
    {
        bool isReadyToShoot = Time.time - shootingInterval > _delayShoot;
        var targetEnemy = CheckDistance.Instance.FindTargetEnemy();
        
        if (targetEnemy == null)
        {
            return;
        }
        if (CheckDistance.Instance.CheckPlayerEnemyDistance(targetEnemy) && isReadyToShoot)
        {
            Debug.Log("ShootBullet");
            Instantiate(_bulletPrefabs, _shootPoint.position, _shootPoint.rotation);
            shootingInterval = Time.time;  // Cập nhật thời gian bắn
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private const string shootParaname = "Shoot";
    [SerializeField] private GameObject _bulletPrefabs;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Animator anim;
    [SerializeField] private float shootingInterval;
    [SerializeField] private float shootingRanged;
    [SerializeField] private float _delayShoot = 1.5f;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        
    }
    private void Update()
    {
        ShootBullet();
    }
    public void ShootBullet()
    {
        bool isReadyToShoot = Time.time - shootingInterval > _delayShoot;
        var targetEnemy = CheckDistance.Instance.FindTargetEnemy();
        var distance = CheckDistance.Instance.CalculateDistanceFromPlayerToEnemy(transform, targetEnemy);

        if (targetEnemy == null || targetEnemy.GetComponent<EnemyHealth>().IsDead)
        {
            return;
        }

        if (isReadyToShoot && distance <= shootingRanged)
        {
            //Debug.Log("ShootBullet");
            anim.SetTrigger(shootParaname);
            Instantiate(_bulletPrefabs, _shootPoint.position, _shootPoint.rotation);
            shootingInterval = Time.time;  // Cập nhật thời gian bắn
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefabs;
    private float shootingInterval;
    [SerializeField]
    private float _distanceDetectionByPlayer;
    [SerializeField]
    private Transform _shootPoint;

    [SerializeField]
    private float _delayShoot;
    void Start()
    {
        shootingInterval = Time.time;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        DetectionByPlayer();
        //DetectionBoss();
    }

    public void DetectionByPlayer()
    {
        float distanceClosest = Mathf.Infinity;
        EnemyMovement enemyClosest = null;
        EnemyMovement[] allEnemies = GameObject.FindObjectsOfType<EnemyMovement>();

        foreach(EnemyMovement currentEnemy in allEnemies)
        {
            float distanceEnemyToPLayer = (currentEnemy.transform.position - transform.position).magnitude;
            if (distanceEnemyToPLayer < distanceClosest)
            {
                distanceClosest = distanceEnemyToPLayer;
                enemyClosest = currentEnemy;
            }
        }

        if (enemyClosest != null)
        {
            float distance = Vector3.Distance(transform.position, enemyClosest.transform.position);
            if (distance < _distanceDetectionByPlayer && Time.time - shootingInterval > _delayShoot)
            {
                Vector3 direction = enemyClosest.transform.position - transform.position;
                transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

                Instantiate(_bulletPrefabs, _shootPoint.position, _shootPoint.rotation).SetActive(true);
                shootingInterval = Time.time;
            }
        }
    }


    //public void DetectionBoss()
    //{
    //    GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        
    //    if(boss != null)
    //    {
    //        float distance = Vector3.Distance(transform.position, boss.transform.position);
    //        if (distance < _distanceDetectionByPlayer && Time.time - shootingInterval > _delayShoot)
    //        {
    //            Vector3 direction = boss.transform.position - transform.position;
    //            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

    //            Instantiate(_bulletPrefabs, _shootPoint.position, _shootPoint.rotation).SetActive(true);
    //            shootingInterval = Time.time;
    //        }
    //    }
    //}
}

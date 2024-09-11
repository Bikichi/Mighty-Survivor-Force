using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    private GameObject enemy;
    private bool _detection = true;

    [SerializeField]
    private float _speedBullet;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.forward * _speedBullet;

        Destroy(gameObject, 2.4f);
    }

    public void FindClosestEnemy()
    {
        if (_detection == true)
        {
            GameObject player = GameObject.Find("ShootPoint");

            float distanceTocloseEnemy = Mathf.Infinity;
            EnemyMovement closestEnemies = null;
            EnemyMovement[] allEnenies = GameObject.FindObjectsOfType<EnemyMovement>();
            foreach (EnemyMovement currentEnemy in allEnenies)
            {
                float distanceToEnemy = (currentEnemy.transform.position - player.transform.position).magnitude;
                if (distanceToEnemy < distanceTocloseEnemy)
                {
                    distanceTocloseEnemy = distanceToEnemy;
                    closestEnemies = currentEnemy;
                }
            }
        }
    }
}
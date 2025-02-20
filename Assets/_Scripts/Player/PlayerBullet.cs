using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private Transform _targetEnemy;
    [SerializeField] private float _speedBullet;
    [SerializeField] public float damageBullet;
    public EnemyHealth enemyHealth;

    public void Start()
    {
        enemyHealth = FindObjectOfType<EnemyHealth>();
        _targetEnemy = CheckDistance.Instance.FindTargetEnemy();
    }

    private void Update()
    {
        MoveBulletToTargetEnemy();
    }

    public void MoveBulletToTargetEnemy()
    {
        if (_targetEnemy == null) 
        { 
            Destroy(gameObject);
            return; 
        }
        MoveBullet(_targetEnemy);
    }

    public void MoveBullet(Transform enemy)
    {
        Vector3 targetPosition = enemy.position + new Vector3(0, enemy.GetComponent<Collider>().bounds.size.y / 2, 0); // Nhắm vào giữa thân enemy
        transform.LookAt(targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speedBullet * Time.deltaTime);
    }
}

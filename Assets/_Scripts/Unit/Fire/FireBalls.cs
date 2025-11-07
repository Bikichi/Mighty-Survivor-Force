using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBalls : PlayerBullet
{
    [Header("Fire Field Settings")]
    [SerializeField] private GameObject fireFieldPrefab;
    [SerializeField] private float damageMultiplier;


    protected override void Start()
    {
        base.Start();

        // Lấy PlayerStats từ CurrentPlayerInstance
        playerStats = ActivePlayerManager.Instance.CurrentPlayerInstance.GetComponent<PlayerStats>();
        damageBullet = playerStats.baseDamage * damageMultiplier;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void MoveBullet()
    {
        if (_targetEnemy == null)
        {
            SpawnFireField(new Vector3(transform.position.x, 1.58f, transform.position.z));
            Destroy(gameObject);
            return;
        }

        Vector3 enemyCenter = _targetEnemy.position + new Vector3(0, _targetEnemy.GetComponent<Collider>().bounds.size.y, 0);
        Vector3 direction = (enemyCenter - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        transform.Translate(Vector3.forward * _speedBullet * Time.deltaTime);
    }

    protected override void ApplyDamage(EnemyHealth enemyHealth)
    {
        enemyHealth.TakeDamage(damageBullet);
    }

    protected override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);

        if (col.CompareTag(Const.ENEMY_TAG))
        {
            SpawnFireField(col.transform.position);
        }
    }

    private void SpawnFireField(Vector3 position)
    {
        if (fireFieldPrefab != null)
        {
            GameObject fireField = Instantiate(fireFieldPrefab, position, Quaternion.identity);
        }
    }
}

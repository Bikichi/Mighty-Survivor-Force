using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBalls : PlayerBullet
{
    [Header("Fire Field Settings")]
    [SerializeField] private GameObject fireFieldPrefab;
    [SerializeField] private float damageMultiplier;

    [SerializeField] private float rotationSpeed = 6f;

    protected override void Start()
    {
        base.Start();
        damageBullet = PlayerStats.Instance.baseDamage * damageMultiplier;
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

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.Translate(Vector3.forward * _speedBullet * Time.deltaTime);
    }
    protected override void ApplyDamage(EnemyHealth enemyHealth, Collider col)
    {
        enemyHealth.TakeDamage(damageBullet);

        DamageUIManager.Instance.ShowDamageUI(damageBullet, col, false);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBalls : PlayerBullet
{
    [Header("Ice Field Settings")]
    [SerializeField] private GameObject iceFieldPrefab;
    [SerializeField] private float damageMultiplier;

    protected override void Start()
    {
        base.Start();
        AudioController.Instance.PlaySound(AudioController.Instance.iceDragon);
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
            SpawnIceField(new Vector3(transform.position.x, 1.58f, transform.position.z));
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
            SpawnIceField(col.transform.position);
        }
    }

    private void SpawnIceField(Vector3 position)
    {
        if (iceFieldPrefab != null)
        {
            GameObject iceField = Instantiate(iceFieldPrefab, position, Quaternion.identity);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBalls : PlayerBullet
{
    [Header("Ice Field Settings")]
    [SerializeField] private GameObject iceFieldPrefab;
    [SerializeField] private float damageMultiplier = 1.5f;

    protected override void Start()
    {
        base.Start();
        damageBullet = PlayerStats.Instance.baseDamage * damageMultiplier;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void ApplyDamage(EnemyHealth enemyHealth, Collider col)
    {
        enemyHealth.TakeDamage(damageBullet);
        //DamageUIManager.Instance.ShowDamageUI(damageBullet, col, false);
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

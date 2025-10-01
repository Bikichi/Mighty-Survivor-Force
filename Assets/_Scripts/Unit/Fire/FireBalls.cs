using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBalls : PlayerBullet
{
    [Header("Fire Field Settings")]
    [SerializeField] private GameObject fireFieldPrefab;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageByWeapon : MonoBehaviour
{
    public GameObject _hitEffect;

    [SerializeField] private float damageMultiplier = 1f;
    [SerializeField] private float weaponDamage;

    private PlayerStats playerStats;

    private void Start()
    {
        // Lấy PlayerStats từ CurrentPlayerInstance
        playerStats = ActivePlayerManager.CurrentPlayerInstance.GetComponent<PlayerStats>();
        CalculateWeaponDamage();
    }

    private void OnEnable()
    {
        if (playerStats != null)
            playerStats.onDamageChanged += CalculateWeaponDamage;
    }

    private void OnDisable()
    {
        if (playerStats != null)
            playerStats.onDamageChanged -= CalculateWeaponDamage;
    }

    private void CalculateWeaponDamage()
    {
        if (playerStats == null) return;
        weaponDamage = playerStats.baseDamage * damageMultiplier;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Const.ENEMY_TAG) /*|| col.CompareTag(Const.BOSS_TAG)*/)
        {
            EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(weaponDamage);

                Vector3 hitPosition = col.ClosestPoint(transform.position);
                Vector3 impactDirection = (col.transform.position - transform.position).normalized;

                if (_hitEffect != null)
                {
                    Quaternion hitRotation = Quaternion.LookRotation(-impactDirection);
                    GameObject effect = Instantiate(_hitEffect, hitPosition, hitRotation);
                    Destroy(effect, 1f);
                }
            }
        }
    }
}

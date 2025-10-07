using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageByWeapon : MonoBehaviour
{
    public GameObject _hitEffect;

    [SerializeField] private float damageMultiplier = 1f;
    [SerializeField] private float weaponDamage;


    private void Start()
    {
        CalculateWeaponDamage();
    }

    private void OnEnable()
    {
        PlayerStats.Instance.onDamageChanged += CalculateWeaponDamage;
    }

    private void OnDisable()
    {
        if (PlayerStats.Instance != null)
            PlayerStats.Instance.onDamageChanged -= CalculateWeaponDamage;
    }

    private void CalculateWeaponDamage()
    {
        weaponDamage = PlayerStats.Instance.baseDamage * damageMultiplier;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Const.ENEMY_TAG))
        {
            EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                var result = CritManager.Instance.CalculateCritDamage(weaponDamage);
                enemyHealth.TakeDamage(result.damage);

                //if (result.isCrit)
                //{
                //    Debug.Log($"CRIT HIT by {gameObject.name}! DamageCRIT: {result.damage}");
                //}

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

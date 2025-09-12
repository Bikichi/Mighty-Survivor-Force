using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageByWeapon : MonoBehaviour
{
    public float weaponDamage;
    public GameObject _hitEffect;

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

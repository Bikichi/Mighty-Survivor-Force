using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDealDamage : MonoBehaviour
{
    [SerializeField] private float mineDamage;
    [SerializeField] private float explosionRadius;
    [SerializeField] private GameObject explosionEffect;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Const.ENEMY_TAG))
        {       
            if (explosionEffect != null)
            {
                GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                Destroy(explosion, 1f);
            }
            
            Destroy(gameObject);
            
            BlowObjects();
        }
    }

    private void BlowObjects()
    {
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, explosionRadius);

        for (int i = 0; i < affectedObjects.Length; i++)
        {
            //nếu là enemy thì gây damage
            if (affectedObjects[i].CompareTag("Enemy"))
            {
                EnemyHealth enemy = affectedObjects[i].GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(mineDamage);
                }
            }
        }
    }
}

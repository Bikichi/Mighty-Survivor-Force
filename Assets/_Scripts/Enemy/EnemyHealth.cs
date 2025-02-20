using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : LivingEntity 
{
    [SerializeField] private GameObject _coinDrop;
    [SerializeField] private bool hasCoin;

    protected override void Die()
    {
        //Debug.Log("ENEMYDIE!!!");
        base.Die();
        if (hasCoin)
        {
            Instantiate(_coinDrop, transform.position, transform.rotation);
        }
        Destroy(gameObject);
        EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();
        enemySpawner.OnEnemyKilled();
    }
}

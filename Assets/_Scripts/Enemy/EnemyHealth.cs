using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : LivingEntity
{
    [SerializeField] private GameObject _coin;
    public PlayerBullet playerBullet;
    public static float _damgeTaken;
    private bool hasCoin;

    private void Die()
    {
        if (hasCoin)
        {
            Instantiate(_coin, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}

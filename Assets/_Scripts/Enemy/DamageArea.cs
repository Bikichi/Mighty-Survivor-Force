using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public int enemyDamage;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Const.PLAYER_TAG))
        {
            PlayerHealth playerHealth = col.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(enemyDamage);
        }
    }
}

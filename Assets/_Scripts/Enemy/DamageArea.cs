using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int enemyDamage;

    private void OnTriggerEnter(Collider col)
    {
        //tại sao Player vẫn không nhận Damage
        if (col.CompareTag(Const.PLAYER_TAG))
        {
            Debug.Log("Aaa!!!");
            playerHealth.TakeDamage(enemyDamage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public PlayerHealth playerHealth;
    private void OnTriggerEnter3D(Collider col, float damage)
    {
        if (col.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }
    }
}

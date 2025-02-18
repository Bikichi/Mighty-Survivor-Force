using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity    
{
    protected override void Die()
    {
        base.Die();
        Debug.Log("Player Die!!!");
    }
}

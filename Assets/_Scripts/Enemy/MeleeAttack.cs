using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : EnemyAttack
{
    public LayerMask attackMask;

    public override void Start()
    {
        base.Start();
    }
    public void DealDamageMelee()
    {
        Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, attackRanged, attackMask, QueryTriggerInteraction.Collide);    
        if (colInfo != null)
        {
            foreach (Collider player in colInfo)
            {
                PlayerHealth pl = player.GetComponent<PlayerHealth>();
                if (pl == null)
                {
                    Debug.LogError(player.name + " không có component PlayerHealth!");
                }
                else
                {
                    pl.TakeDamage(attackDamage);
                    Debug.Log("Gây sát thương lên " + player.name);
                }
            }
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    if (attackPoint = null)
    //    {
    //        return;
    //    }
    //    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    //}
}

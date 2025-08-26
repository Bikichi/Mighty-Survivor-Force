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
                if (pl != null)
                {
                    pl.TakeDamage(attackDamage);
                }
                else
                {
                    return;
                }
            }
        }
    }

    public override void OnAttackAnimationEnd()
    {
        //Debug.Log("Ranged attack animation ended.");
        base.OnAttackAnimationEnd();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRanged);
    }
}

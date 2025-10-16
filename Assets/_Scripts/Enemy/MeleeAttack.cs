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

        if (colInfo.Length > 0) //có player trong tầm đánh thì gây dame
        {
            PlayerHealth ph = colInfo[0].GetComponent<PlayerHealth>();
            ph.TakeDamage(attackDamage);
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

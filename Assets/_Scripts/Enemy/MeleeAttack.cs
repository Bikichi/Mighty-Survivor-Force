using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : EnemyAttack, ISkillStatus
{
    public LayerMask attackMask;
    public BossChargeSkill bossCharge;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        if (bossCharge != null && (bossCharge.isWindUp || bossCharge.isCharging))
        {
            return;
        }
        base.Attack(); 
    }
    public virtual void DealDamageMelee()
    {
        Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, attackRanged, attackMask, QueryTriggerInteraction.Collide);

        if (colInfo.Length > 0) //có player trong tầm đánh thì gây dame
        {
            PlayerHealth ph = colInfo[0].GetComponent<PlayerHealth>();

            if (ph != null)
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

    public virtual bool IsActive()
    {
        return isAttacking;
    }
}

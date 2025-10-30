using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : EnemyAttack, ISkillStatus
{
    public LayerMask attackMask;

    public List<MonoBehaviour> bossSkills = new List<MonoBehaviour>();
    public List<ISkillStatus> skillStatusList = new List<ISkillStatus>();

    protected void Awake()
    {
        BossComponentUtils.AddBossComponentInParent<BossChargeSkill>(gameObject, bossSkills);
        BossComponentUtils.AddBossComponentInParent<BossFireBreath>(gameObject, bossSkills);
        BossComponentUtils.AddBossComponentInParent<BossMultiShoot>(gameObject, bossSkills);
        //chỉ lấy những script nào có implement ISkillStatus
        foreach (var skill in bossSkills)
        {
            if (skill is ISkillStatus skillStatus)
            {
                skillStatusList.Add(skillStatus);
            }
        }
    }

    protected bool IsAnySkillActive()
    {
        foreach (var skill in skillStatusList)
        {
            if (skill.IsActive())
                return true;
        }
        return false;
    }

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
        if (IsAnySkillActive())
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

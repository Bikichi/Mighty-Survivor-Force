 using System.Collections.Generic;
using UnityEngine;

public class BossMovement : EnemyMovement
{
    [Header("Boss Skills")]
    public List<MonoBehaviour> bossSkills = new List<MonoBehaviour>();

    public List<ISkillStatus> skillStatusList = new List<ISkillStatus>();

    private void Awake()
    {
        BossComponentUtils.AddBossComponentInChildren<BossBigStrike>(gameObject, bossSkills);
        //chỉ lấy những script nào có implement ISkillStatus
        foreach (var skill in bossSkills)
        {
            if (skill is ISkillStatus skillStatus)
            {
                skillStatusList.Add(skillStatus);
            }
        }
    }

    private bool IsAnySkillActive()
    {
        foreach (var skill in skillStatusList)
        {
            if (skill.IsActive())
                return true;
        }
        return false;
    }

    protected override void MoveEnemy()
    {
        if (IsAnySkillActive())
        {
            return;
        }

        Vector3 direction = (targetPlayer.transform.position - transform.position).normalized;
        direction.y = 0;

        float distance = CheckDistance.Instance.CalculateDistanceToPlayer(targetPlayer.transform, transform);

        if (distance <= stoppingDistance)
        {
            isMoving = false;
            //rb.velocity = Vector3.zero;
        }
        else
        {
            isMoving = true;
            //rb.velocity = direction * enemyMoveSpeed;
            transform.Translate(direction * enemyMoveSpeed * Time.deltaTime, Space.World);
        }
    }


}

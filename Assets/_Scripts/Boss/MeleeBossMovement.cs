using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBossMovement : EnemyMovement
{
    public BossChargeSkill bossCharge; 

    protected override void MoveEnemy()
    {
        if (bossCharge.isWindUp || bossCharge.isCharging)
        {
            return;
        }

        base.MoveEnemy(); 
    }
}

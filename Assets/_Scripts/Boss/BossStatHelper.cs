using UnityEngine;

public static class BossStatHelper
{
    private const float multiplier = 1.5f;

    public static void IncreaseBossStats(GameObject boss)
    {
        BossHealth bossHealth = boss.GetComponent<BossHealth>();
        BossMovement movement = boss.GetComponent<BossMovement>();
        BossChargeSkill chargeSkill = boss.GetComponent<BossChargeSkill>();
        MeleeAttack meleeAttack = boss.GetComponentInChildren<MeleeAttack>();
        BossBigStrike bigStrike = boss.GetComponentInChildren<BossBigStrike>();
        BossFireBreath bossFireBreath = boss.GetComponent<BossFireBreath>();
        BossMultiShoot bossMultiShoot = boss.GetComponent<BossMultiShoot>();

        if (bossHealth != null)
            bossHealth.defense *= multiplier;

        if (movement != null)
            movement.enemyMoveSpeed *= multiplier;


        //chỉ tăng dame ở những đòn đánh
        if (meleeAttack != null)
        {
            meleeAttack.attackDamage *= multiplier;
            meleeAttack.attackCooldown /= multiplier;
        }

        if (bigStrike != null)
        {
            bigStrike.attackRanged *= multiplier;
            bigStrike.attackDamage *= multiplier;
            bigStrike.attackCooldown /= multiplier;
        }

        //những skill đặc biệt không tăng dame
        if (chargeSkill != null)
        {
            chargeSkill.chargeSpeed *= multiplier;
            chargeSkill.cooldown /= multiplier; 
        }

        if (bossFireBreath != null)
        {
            bossFireBreath.lerpSpeed *= multiplier;
            bossFireBreath.fireDuration *= multiplier;
            bossFireBreath.fireCooldown /= multiplier;
        }

        if (bossMultiShoot != null)
        {
            bossMultiShoot.fireCooldown /= multiplier;
        }

        Debug.Log($"[BossStatHelper] Applied {multiplier}x stat multiplier to boss: {boss.name}");
    }
}

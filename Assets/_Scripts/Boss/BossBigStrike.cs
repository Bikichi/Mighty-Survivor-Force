using System.Collections.Generic;
using UnityEngine;

public class BossBigStrike : MeleeAttack
{
    [Header("Boss Skill References")]

    private string bigStrikeAnimName = "BigStrike";

    public override void Start()
    {
        base.Start();
    }

    public override void Attack()
    {
        if (IsAnySkillActive())
        {
            return;
        }
        attackTimer += Time.deltaTime;

        bool isReadyToAttack = attackTimer >= attackCooldown;
        bool inRange = CheckDistance.Instance.CalculateDistanceToPlayer(targetPlayer.transform, transform) <= attackRanged;

        if (isReadyToAttack && inRange)
        {
            isAttacking = true;

            anim.SetTrigger(bigStrikeAnimName);
            //Debug.Log("Boss kích hoạt skill: CÚ ĐÁNH LỚN!");

            SpawnAttackFlash();
            attackTimer = 0f;
        }
    }


    //nếu dùng animation event, gọi DealDamageMelee ở class cha cũng đồng thời gọi luôn DealDamageMelee ở class con,
    //nên class con phải ghi đè lại logic DealDamageMelee về trống và dùng logic dealdamage riêng
    public override void DealDamageMelee()
    {
        return; //bỏ trống tránh gọi 2 lần
    }

    public void BigStrike_DealDamage()
    {
        base.DealDamageMelee();
    }

    public override void OnAttackAnimationEnd()
    {
        base.OnAttackAnimationEnd();
        //Debug.Log("Kết thúc animation Cú Đánh Lớn.");
    }
}

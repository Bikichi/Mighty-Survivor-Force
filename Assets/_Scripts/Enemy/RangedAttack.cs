using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangedAttack : EnemyAttack
{
    public GameObject enemyBulletPrefabs;
    public Transform shootPoint;
    public Quaternion bulletRotation;
    public override void Start()
    {
        base.Start();
    }
    public override void Attack()
    {
        attackTimer += Time.deltaTime;
        bool isReadyToAttack = attackTimer >= attackCooldown;
        bool canAttackPlayer = CheckDistance.Instance.CalculateDistanceToEnemy(targetPlayer.transform, transform) <= attackRanged;
        if (isReadyToAttack && canAttackPlayer)
        {
            isAttacking = true;
            anim.SetTrigger(attackParaname);

            Instantiate(enemyBulletPrefabs, shootPoint.position, bulletRotation);
            attackTimer = 0f;
        }
    }

    public override void OnAttackAnimationEnd()
    {
        //Debug.Log("Ranged attack animation ended.");
        base.OnAttackAnimationEnd();
    }
}

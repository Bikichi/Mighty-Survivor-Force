    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //public EnemyScriptableObject enemyData;
    public GameObject targetPlayer;
    public GameObject damageArea;
    private const string attackParaname = "Attack";
    public Animator anim;
    public float attackTimer;
    public float attackInterval;
    public float distanceToPlayer;

    private void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        damageArea = GameObject.FindGameObjectWithTag("DA"); // không chạy?
        attackTimer = attackInterval;
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        bool isReadyToAttack = attackTimer >= attackInterval;
        if (isReadyToAttack) 
        {
            Attack();
        }
    }

    public void Attack()
    {
        EnemyMovement em = FindObjectOfType<EnemyMovement>();
        if (CheckDistance.Instance.CalculateDistanceFromPlayerToEnemy(targetPlayer.transform, transform) <= distanceToPlayer) 
        {
            anim.SetTrigger(attackParaname);
            //damageArea.SetActive(true);
            //làm thế nào để bật tắt damageArea
            //khi attack thì đứng yên, attack xong mới được di chuyển
            attackTimer = 0f;
        }
    }
}

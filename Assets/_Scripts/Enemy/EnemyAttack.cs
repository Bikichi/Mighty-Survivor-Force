using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private const string attackParaname = "Attack";
    public GameObject targetPlayer;
    public Animator anim;
    public Transform attackPoint;
    public LayerMask attackMask;

    public float attackDamage;
    public float attackTimer;
    public float attackInterval;
    public float attackRanged;

    public bool isAttacking = false;

    public void Start()
    {
        anim = GetComponent<Animator>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        attackPoint = transform;
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
        if (CheckDistance.Instance.CalculateDistanceFromPlayerToEnemy(targetPlayer.transform, transform) <= attackRanged)
        {
            isAttacking = true;
            anim.SetTrigger(attackParaname);
            attackTimer = 0f;
        }
    }
    //Dùng Animetion Event
    //Hàm này được gọi khi animation Attack kết thúc
    public void OnAttackAnimationEnd()
    {
        isAttacking = false;
    }

    public void DealDamage()
    {
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, attackRanged, attackMask, QueryTriggerInteraction.Collide);
        if (colInfo != null)
        {
            foreach (Collider player in colInfo)
            {
                PlayerHealth pl = player.GetComponent<PlayerHealth>();
                if (pl == null)
                {
                    Debug.LogError(player.name + " không có component PlayerHealth!");
                }
                else
                {
                    pl.TakeDamage(attackDamage);
                    Debug.Log("Gây sát thương lên " + player.name);
                }
            }
        }

    }

    //private void OnDrawGizmosSelected()
    //{
    //    if (attackPoint = null)
    //    {
    //        return;
    //    }
    //    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    //}
}
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    protected const string attackParaname = "Attack";
    public GameObject targetPlayer;
    public Animator anim;
    public Transform attackPoint;

    public float attackDamage;
    public float attackTimer;
    public float attackCooldown;
    public float attackRanged;

    public bool isAttacking = false;

    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        attackPoint = transform;
        attackTimer = attackCooldown;
    }

    private void Update()
    {
        Attack();
    }

    public virtual void Attack()
    {
        attackTimer += Time.deltaTime;
        bool isReadyToAttack = attackTimer >= attackCooldown;
        if (isReadyToAttack && CheckDistance.Instance.CalculateDistanceFromPlayerToEnemy(targetPlayer.transform, transform) <= attackRanged)
        {
            isAttacking = true;
            anim.SetTrigger(attackParaname);
            attackTimer = 0f;
        }
    }
    //Dùng Animation Event
    //khi Animation Attack chạy xong gọi hàm
    //Lưu ý khi set Animation Event đảm bảo Animation Clip chạy tới đoạn có gắn Event ấy
    public void OnAttackAnimationEnd()
    {
        isAttacking = false;
    }
}
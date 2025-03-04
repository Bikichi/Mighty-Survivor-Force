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

    public GameObject attackFlashPrefab;
    public Transform attackFlashPoint;
    public float flashDuration = 1.5f;


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
        if (isReadyToAttack && CheckDistance.Instance.CalculateDistanceToEnemy(targetPlayer.transform, transform) <= attackRanged)
        {
            isAttacking = true;
            anim.SetTrigger(attackParaname);
            SpawnAttackFlash();
            attackTimer = 0f;
        }
    }
    public void SpawnAttackFlash()
    {
        if (attackFlashPrefab != null && attackFlashPoint != null)
        {
            Quaternion flippedRotation = attackFlashPoint.rotation * Quaternion.Euler(180, 0, 0);
            GameObject flash = Instantiate(attackFlashPrefab, attackFlashPoint.position, flippedRotation);
            Destroy(flash, flashDuration);
        }
    }
    //Dùng Animation Event
    //khi Animation Attack chạy xong gọi hàm
    //Lưu ý khi set Animation Event đảm bảo Animation Clip chạy tới đoạn có gắn Event ấy
    public virtual void OnAttackAnimationEnd()
    {
        isAttacking = false;
    }
}
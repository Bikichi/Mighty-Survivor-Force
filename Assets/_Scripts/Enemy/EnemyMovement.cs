using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public GameObject targetPlayer;
    [SerializeField] public EnemyAttack enemyAttack;
    [SerializeField] public Animator anim;
    [SerializeField] public Rigidbody rb;

    [Header("Movement Settings")]
    public float enemyMoveSpeed = 3f; // tốc độ gốc
    public float lerpSpeed = 8f;
    public float stoppingDistance = 2f;

    [Header("Animation Settings")]
    private const string runParaname = "Move";
    public float checkInterval = 0.05f; // kiểm tra mỗi 0.05 giây
    private float checkTimer = 0f;

    private Vector3 lastPosition;
    protected bool isMoving;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        enemyAttack = GetComponentInChildren<EnemyAttack>();

        targetPlayer = GameObject.FindGameObjectWithTag("Player");


        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (targetPlayer == null)
            return; //nếu không có player thì không làm gì cả

        RotateTowardsPlayer();
        MoveEnemy();

        checkTimer += Time.deltaTime;
        if (checkTimer >= checkInterval)
        {
            UpdateAnimationState();
            checkTimer = 0f;
        }
    }

    protected virtual void MoveEnemy()
    {
        Vector3 direction = (targetPlayer.transform.position - transform.position).normalized;
        direction.y = 0; // chỉ di chuyển theo chiều ngang

        float distance = CheckDistance.Instance.CalculateDistanceToPlayer(targetPlayer.transform, transform);

        bool isAttacking = enemyAttack != null && enemyAttack.isAttacking;

        if (distance <= stoppingDistance || isAttacking)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
            transform.Translate(direction * enemyMoveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void UpdateAnimationState()
    {
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        bool positionChanged = distanceMoved > 0.01f;

        bool isAttacking = enemyAttack != null && enemyAttack.isAttacking;

        if (anim != null)
        {
            if (!isAttacking && positionChanged)
                anim.SetBool(runParaname, true);
            else
                anim.SetBool(runParaname, false);
        }

        lastPosition = transform.position;
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (targetPlayer.transform.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, lerpSpeed * Time.deltaTime);
        }
    }
}

using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] public GameObject targetPlayer;
    public EnemyAttack enemyAttack;
    private const string runParaname = "Move";
    public Animator anim;
    public float enemyMoveSpeed;
    public float lerpSpeed;
    public float checkInterval = 0.05f; //kiểm tra mỗi 0.2 giây
    public float checkTimer = 0f;
    public Rigidbody rb;

    public float stoppingDistance;
    public bool isMoving;

    private Vector3 lastPosition;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        enemyAttack = GetComponentInChildren<EnemyAttack>();
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        //transform.LookAt(targetPlayer.transform, Vector3.up);
        RotateTowardsPlayer();
        MoveEnemy();
        checkTimer += Time.deltaTime;
        if (checkTimer >= checkInterval)
        {
            UpdateAnimationState();
            checkTimer = 0f; // Reset timer
        }
    }

    protected virtual void MoveEnemy()
    {
        Vector3 direction = (targetPlayer.transform.position - transform.position).normalized;
        direction.y = 0; //giữ nguyên chiều dọc để chỉ di chuyển theo chiều ngang

        float distance = CheckDistance.Instance.CalculateDistanceToPlayer(targetPlayer.transform, transform);

        if (distance <= stoppingDistance || enemyAttack.isAttacking)
        {
            isMoving = false;
            rb.velocity = Vector3.zero;
            return;
        }
        else if (distance > stoppingDistance && !enemyAttack.isAttacking)
        {

            isMoving = true;
            rb.velocity = direction * enemyMoveSpeed;
            //transform.Translate(enemyMoveSpeed * direction * Time.deltaTime);
        }
    }

    private void UpdateAnimationState()
    {
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        bool positionChanged = distanceMoved > 0.01f;

        if (!enemyAttack.isAttacking && positionChanged)
        {
            anim.SetBool(runParaname, true);
        }
        else if (enemyAttack.isAttacking || !positionChanged)
        {
            anim.SetBool(runParaname, false);
        }
        lastPosition = transform.position;
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (targetPlayer.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, lerpSpeed * Time.deltaTime);
    }
}

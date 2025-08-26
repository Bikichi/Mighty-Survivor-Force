using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] public GameObject targetPlayer;
    public EnemyAttack enemyAttack;
    private const string runParaname = "Move";
    public Animator anim;
    public float enemyMoveSpeed;
    public Rigidbody rb;

    public float distanceToPlayer;
    public bool isMoving;


    private Vector3 lastPosition;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        enemyAttack = GetComponentInChildren<EnemyAttack>();

        lastPosition = transform.position;
    }

    void Update()
    {
        transform.LookAt(targetPlayer.transform, Vector3.up);
        MoveEnemy();
        UpdateAnimationState();
    }

    public void MoveEnemy()
    {
        Vector3 direction = (targetPlayer.transform.position - transform.position).normalized;
        direction.y = 0; //giữ nguyên chiều dọc để chỉ di chuyển theo chiều ngang

        float distance = CheckDistance.Instance.CalculateDistanceToEnemy(targetPlayer.transform, transform);

        if (distance <= distanceToPlayer || enemyAttack.isAttacking)
        {
            isMoving = false;
            rb.velocity = Vector3.zero;
            return;
        }
        else if (distance > distanceToPlayer && !enemyAttack.isAttacking)
        {
            isMoving = true;
            rb.velocity = direction * enemyMoveSpeed;
        }
    }

    private void UpdateAnimationState()
    {
        bool positionChanged = (transform.position - lastPosition).sqrMagnitude > 0;

        if (!enemyAttack.isAttacking && positionChanged)
        {
            anim.SetBool(runParaname, true);
        }
        else if ((enemyAttack.isAttacking || !positionChanged))
        {
            anim.SetBool(runParaname, false);
        }
        lastPosition = transform.position;
    }
}

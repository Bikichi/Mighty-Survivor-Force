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


    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        enemyAttack = GetComponentInChildren<EnemyAttack>();
    }

    void Update()
    {
        transform.LookAt(targetPlayer.transform, Vector3.up);
        MoveEnemy();
    }

    public void MoveEnemy()
    {
        if (CheckDistance.Instance.CalculateDistanceToEnemy(targetPlayer.transform, transform) <= distanceToPlayer || enemyAttack.isAttacking)
        {
            anim.SetBool(runParaname, false);
            isMoving = false;
            return;
        }
        else if (CheckDistance.Instance.CalculateDistanceToEnemy(targetPlayer.transform, transform) > distanceToPlayer && !enemyAttack.isAttacking)
        {
            anim.SetBool(runParaname, true);
            isMoving = true;
            if (isMoving)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * enemyMoveSpeed);
            }
        }
    }
}
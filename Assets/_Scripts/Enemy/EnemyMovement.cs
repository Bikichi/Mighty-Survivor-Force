using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] public GameObject targetPlayer;
    private const string runParaname = "Run";
    public Animator anim;
    //public EnemyScriptableObject enemyData;
    public float enemyMoveSpeed;
    public Rigidbody rb;

    public float distanceToPlayer;
    public bool isMoving;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        transform.LookAt(targetPlayer.transform, Vector3.up);
        MoveEnemy();
    }

    public void MoveEnemy()
    {
        // Kiểm tra khoảng cách và dừng di chuyển nếu đủ gần
        if (CheckDistance.Instance.CalculateDistanceFromPlayerToEnemy(targetPlayer.transform, transform) <= distanceToPlayer)
        {
            anim.SetBool(runParaname, false);
            isMoving = false;
            return; // Dừng hàm tại đây nếu đủ gần
        }
        else
        {
            anim.SetBool(runParaname, true);
            isMoving = true;

            // Di chuyển quái vật về phía người chơi nếu đang di chuyển
            if (isMoving)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * enemyMoveSpeed);
            }
        }
    }
}

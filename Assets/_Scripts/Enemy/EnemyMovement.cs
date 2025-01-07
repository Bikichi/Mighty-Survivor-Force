using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject _targetPlayer;
    private const string runParaname = "Run";
    public Animator anim;
    //public EnemyScriptableObject enemyData;
    public float enemyMoveSpeed;
    public Rigidbody rb;

    public float distance;
    public bool isMoving;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.drag = 0f;
        //rb.angularDrag = 0f;
    }
    void Update()
    {
        transform.LookAt(_targetPlayer.transform, Vector3.up);
        MoveEnemy();
    }

    public void MoveEnemy()
    {
        // Kiểm tra khoảng cách và dừng di chuyển nếu đủ gần
        if (CheckDistance.Instance.CalculateDistanceFromPlayerToEnemy(_targetPlayer.transform, transform) <= distance)
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

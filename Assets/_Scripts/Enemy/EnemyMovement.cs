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
        if (CheckDistance.Instance.CalculateDistanceFromPlayerToEnemy(_targetPlayer.transform, transform) <= distance)
        {
            anim.SetBool(runParaname, false);
            isMoving = false;
            return;
        }
        else
        {
            anim.SetBool(runParaname, true);
            isMoving = true;

            transform.Translate(Vector3.forward * Time.deltaTime * enemyMoveSpeed);
            Vector3 direction = (_targetPlayer.transform.position - transform.position).normalized;
            //Vector3 movement = direction * Time.deltaTime * enemyMoveSpeed;

            //// Di chuyển thông qua Rigidbody và áp dụng một lực nhỏ để tránh va chạm mạnh
            //Vector3 currentVelocity = rb.velocity;
            //rb.velocity = new Vector3(currentVelocity.x, movement.y, currentVelocity.z); // Chỉ thay đổi vận tốc trên mặt đất
        }
    }
}

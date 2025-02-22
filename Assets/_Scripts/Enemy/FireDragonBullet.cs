using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDragonBullet : MonoBehaviour
{
    [SerializeField] GameObject targetPlayer;
    [SerializeField] private float _speedBullet;
    public float damageBullet;
    private Vector3 targetPosition;
    private Vector3 moveDirection;

    public void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        if (targetPlayer != null)
        {
            targetPosition = targetPlayer.transform.position + new Vector3(0, targetPlayer.GetComponent<Collider>().bounds.size.y / 2, 0);
            moveDirection = (targetPosition - transform.position).normalized;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        MoveBullet();
    }

    public void MoveBullet()
    {
        transform.Translate(moveDirection * _speedBullet * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Const.WALL_TAG)) //nếu đối tượng này va chạm với đối tượng có tag thì thực thi
        {
            //Debug.Log("Va chạm với tường!");
            Destroy(gameObject); ; //hủy viên đạn
        }
    }
}
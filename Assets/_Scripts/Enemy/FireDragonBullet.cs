using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDragonBullet : MonoBehaviour
{
    [SerializeField] GameObject targetPlayer;
    [SerializeField] private float _speedBullet;
    public float damageBullet;
    private Vector3 targetPosition;

    public void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        if (targetPlayer != null)
        {
            targetPosition = targetPlayer.transform.position + new Vector3(0, targetPlayer.GetComponent<Collider>().bounds.size.y / 2, 0);
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
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speedBullet * Time.deltaTime);
        // Nếu đạn đã đến đích thì hủy
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
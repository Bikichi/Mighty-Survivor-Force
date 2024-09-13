using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float _speedBullet;
    [SerializeField] private Transform _closetEnemy;
    [SerializeField] private float _damageBullet;

    public void MoveBullet(Transform enemy)
    {
        Vector3 targetPosition = enemy.position + new Vector3(0, enemy.GetComponent<Collider>().bounds.size.y / 2, 0); // Nhắm vào giữa enemy
        transform.LookAt(targetPosition);
        transform.Translate(Vector3.forward * Time.deltaTime * _speedBullet);
    }

    public void Start()
    {
        _closetEnemy = CheckDistance.Instance.FindClosestEnemy();
    }

    private void Update()
    {
        MoveBullet(_closetEnemy);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Const.ENEMY_TAG)) //nếu đối tượng này va chạm với đối tượng có tag là ENEMY_TAG thì thực thi
        {
            Debug.Log("BANG!!!");
            Destroy(gameObject); //hủy đối tượng phương thức gắn vào
        }
    }
}

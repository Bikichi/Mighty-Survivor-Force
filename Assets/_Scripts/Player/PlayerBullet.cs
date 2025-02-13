using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private Transform _targetEnemy;
    [SerializeField] private float _speedBullet;
    [SerializeField] public float damageBullet;

    public void Start()
    {
        _targetEnemy = CheckDistance.Instance.FindTargetEnemy();
    }

    private void Update()
    {
        MoveBulletToClosestEnemy();
    }

    public void MoveBullet(Transform enemy)
    {
        Vector3 targetPosition = enemy.position + new Vector3(0, enemy.GetComponent<Collider>().bounds.size.y / 2, 0); // Nhắm vào giữa thân enemy
        transform.LookAt(targetPosition);
        transform.Translate(Vector3.forward * Time.deltaTime * _speedBullet);
    }

    public void MoveBulletToClosestEnemy()
    {
        if (_targetEnemy == null) { return; }
        MoveBullet(_targetEnemy);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Const.ENEMY_TAG)) //nếu đối tượng này va chạm với đối tượng có tag là ENEMY_TAG thì thực thi
        {
            EnemyHealth eh = FindObjectOfType<EnemyHealth>();
            eh.TakeDamage(damageBullet);
            Destroy(gameObject); ; //hủy đối tượng mà phương thức gắn vào
        }
    }
}

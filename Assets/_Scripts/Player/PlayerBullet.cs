using UnityEngine;

public class PlayerBullet : BaseBullet
{
    [SerializeField] private Transform _targetEnemy;

    protected override void Start()
    {
        base.Start();
        _targetEnemy = CheckDistance.Instance.FindTargetEnemy();
    }

    protected override void MoveBullet()
    {
        if (_targetEnemy == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetPosition = _targetEnemy.position + new Vector3(0, _targetEnemy.GetComponent<Collider>().bounds.size.y / 2, 0);
        transform.LookAt(targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speedBullet * Time.deltaTime);
    }

    protected override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        if (col.CompareTag(Const.ENEMY_TAG))
        {
            EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageBullet);

                Vector3 hitPosition = col.ClosestPoint(transform.position); //lấy vị trí va chạm gần nhất
                Vector3 impactDirection = (col.transform.position - transform.position).normalized; //hướng vật thể va chạm tới Enemy

                if (_hitEffect != null)
                {
                    Quaternion hitRotation = Quaternion.LookRotation(-impactDirection); //quay ngược lại hướng va chạm
                    GameObject effect = Instantiate(_hitEffect, hitPosition, hitRotation); //effect sinh ra với hướng ngược với hướng của vật thể lao tới
                    Destroy(effect, 1f);
                }

                Destroy(gameObject, 0.2f);
            }
        }
    }
}
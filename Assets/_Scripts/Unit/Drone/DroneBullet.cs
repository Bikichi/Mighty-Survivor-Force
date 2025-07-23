using UnityEngine;

public class DroneBullet : BaseBullet
{
    [SerializeField] private Transform _targetEnemy;

    protected override void Start()
    {
        base.Start();
        _targetEnemy = CheckDistance.Instance.FindTargetEnemy();
    }

    protected override void MoveBullet()
    {
        //tìm mục tiêu mới nếu mục tiêu cũ chết thay vì huỷ luôn viên đạn
        if (_targetEnemy == null || !_targetEnemy.gameObject.activeInHierarchy)
        {
            Transform newTarget = CheckDistance.Instance.FindTargetEnemy();
            if (newTarget != null)
            {
                _targetEnemy = newTarget;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        Vector3 enemyCenter = _targetEnemy.position + new Vector3(0, _targetEnemy.GetComponent<Collider>().bounds.size.y / 2, 0);
        Vector3 direction = (enemyCenter - transform.position).normalized;

        transform.Translate(direction * _speedBullet * Time.deltaTime, Space.World);
        transform.forward = direction;
    }
    private void OnTriggerEnter(Collider col)
    {
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
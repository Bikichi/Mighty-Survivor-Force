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
        //transform.forward = direction;
    }
}
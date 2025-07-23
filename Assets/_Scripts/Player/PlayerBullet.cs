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

}
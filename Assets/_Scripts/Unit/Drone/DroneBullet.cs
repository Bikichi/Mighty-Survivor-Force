using UnityEngine;

public class DroneBullet : PlayerBullet
{
    [SerializeField] private float damageMultiplier;
    [SerializeField] private float rotationSpeed;

    protected override void Start()
    {
        damageBullet = PlayerStats.Instance.baseDamage * damageMultiplier;
        _targetEnemy = CheckDistance.Instance.FindLowestHealthEnemy();
    }

    //Không crit
    protected override void ApplyDamage(EnemyHealth enemyHealth)
    {
        enemyHealth.TakeDamage(damageBullet);
    }

    protected override void MoveBullet()
    {
        if (_targetEnemy == null || !_targetEnemy.gameObject.activeInHierarchy)
        {
            Transform newTarget = CheckDistance.Instance.FindLowestHealthEnemy();
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

        Vector3 enemyCenter = _targetEnemy.position + new Vector3(0, _targetEnemy.GetComponent<Collider>().bounds.size.y, 0);
        Vector3 direction = (enemyCenter - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.Translate(Vector3.forward * _speedBullet * Time.deltaTime);
    }
}

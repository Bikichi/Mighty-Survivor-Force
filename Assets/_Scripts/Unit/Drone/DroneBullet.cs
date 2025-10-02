using UnityEngine;

public class DroneBullet : PlayerBullet
{
    [SerializeField] private float damageMultiplier = 0.5f;

    protected override void Start()
    {
        damageBullet = PlayerStats.Instance.baseDamage * damageMultiplier;
        _targetEnemy = CheckDistance.Instance.FindLowestHealthEnemy();
    }

    //Không crit
    protected override void ApplyDamage(EnemyHealth enemyHealth, Collider col)
    {
        enemyHealth.TakeDamage(damageBullet);
        DamageUIManager.Instance.ShowDamageUI(damageBullet, col, false);
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

        transform.Translate(direction * _speedBullet * Time.deltaTime, Space.World);
        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }
    }
}

using UnityEngine;
public class PlayerBullet : BaseBullet
{
    [SerializeField] protected Transform _targetEnemy;
    private PlayerStats playerStats;

    protected override void Start()
    {
        base.Start();

        playerStats = ActivePlayerManager.CurrentPlayerInstance.GetComponent<PlayerStats>();
        damageBullet = playerStats.baseDamage;
        _targetEnemy = CheckDistance.Instance.FindClosestEnemy();
    }

    protected override void MoveBullet()
    {
        if (_targetEnemy == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetPosition = _targetEnemy.position
                               + new Vector3(0, _targetEnemy.GetComponent<Collider>().bounds.size.y / 2, 0);
        transform.LookAt(targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speedBullet * Time.deltaTime);
    }

    protected override void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Const.ENEMY_TAG))
        {
            EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                ApplyDamage(enemyHealth);
                PlayHitEffect(col);
                Destroy(gameObject, 0.1f);
            }
        }
    }

    protected virtual void ApplyDamage(EnemyHealth enemyHealth)
    {
        var result = CritManager.Instance.CalculateCritDamage(damageBullet);
        enemyHealth.TakeDamage(result.damage, result.isCrit);

    }

    protected virtual void PlayHitEffect(Collider col)
    {
        if (_hitEffect == null) return;

        Vector3 hitPosition = col.ClosestPoint(transform.position);
        Vector3 impactDirection = (col.transform.position - transform.position).normalized;
        Quaternion hitRotation = Quaternion.LookRotation(-impactDirection);

        GameObject effect = Instantiate(_hitEffect, hitPosition, hitRotation);
        Destroy(effect, 1f);
    }
}

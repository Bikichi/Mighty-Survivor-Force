using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] protected float _speedBullet;
    [SerializeField] public float damageBullet;
    [SerializeField] protected GameObject _hitEffect;

    protected virtual void Start() { }

    protected virtual void Update()
    {
        MoveBullet();
    }

    protected abstract void MoveBullet();

    protected virtual void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Const.WALL_TAG))
        {
            Destroy(gameObject);
        }

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

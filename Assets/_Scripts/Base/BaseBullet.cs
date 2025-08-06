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
    }
}

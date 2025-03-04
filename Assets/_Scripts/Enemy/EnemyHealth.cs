using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : LivingEntity
{
    [SerializeField] private GameObject _coinDrop;
    [SerializeField] private bool _hasCoin;
    [SerializeField] private GameObject _bulletHitEffect;
    [SerializeField] private GameObject _sawBladeHitEffect;
    [SerializeField] private GameObject _swordHitEffect;
    public float deathAnimationTime;
    public Animator _anim;

    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    protected override void Die()
    {
        base.Die();
        DisableEnemyActions();
        StartCoroutine(HandleDeath());
        EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();
        enemySpawner.OnEnemyKilled();
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(deathAnimationTime);

        if (_hasCoin)
        {
            Instantiate(_coinDrop, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    public void DisableEnemyActions()
    {
        EnemyMovement movementScript = GetComponent<EnemyMovement>();
        if (movementScript != null) movementScript.enabled = false;

        MeleeAttack meleeAttack = GetComponentInChildren<MeleeAttack>();
        if (meleeAttack != null) meleeAttack.enabled = false;

        RangedAttack rangedAttack = GetComponentInChildren<RangedAttack>();
        if (rangedAttack != null) rangedAttack.enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        Vector3 hitPosition = col.ClosestPoint(transform.position); //lấy vị trí va chạm gần nhất
        Vector3 impactDirection = (transform.position - col.transform.position).normalized; //hướng vật thể va chạm tới Enemy

        if (col.CompareTag(Const.PLAYERBULLET_TAG))
        {
            PlayerBullet playerBullet = col.GetComponent<PlayerBullet>();
            TakeDamage(playerBullet.damageBullet);
            Destroy(col.gameObject, 0.2f);

            if (_bulletHitEffect != null)
            {
                Quaternion hitRotation = Quaternion.LookRotation(-impactDirection); //quay ngược lại hướng va chạm
                GameObject effect = Instantiate(_bulletHitEffect, hitPosition, hitRotation); //effect sinh ra với hướng ngược với hướng của vật thể lao tới
                Destroy(effect, 1f);
            }
        }

        if (col.CompareTag(Const.SAWBLADE_TAG))
        {
            SawBlade sawBlade = FindObjectOfType<SawBlade>();
            TakeDamage(sawBlade.sawBladeDamage);

            if (_sawBladeHitEffect != null)
            {
                Quaternion hitRotation = Quaternion.LookRotation(-impactDirection);
                GameObject effect = Instantiate(_sawBladeHitEffect, hitPosition, hitRotation);
                Destroy(effect, 1f);
            }
        }

        if (col.CompareTag(Const.SWORD_TAG))
        {
            SwordController swordController = col.GetComponent<SwordController>();
            TakeDamage(swordController.attackDamage);

            if (_swordHitEffect != null)
            {
                Quaternion hitRotation = Quaternion.LookRotation(-impactDirection);
                GameObject effect = Instantiate(_swordHitEffect, hitPosition, hitRotation);
                Destroy(effect, 1f);
            }
        }
    }
}

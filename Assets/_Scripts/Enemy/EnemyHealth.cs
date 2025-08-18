using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : LivingEntity
{
    [SerializeField] private GameObject _coinDrop;
    [SerializeField] private bool _hasCoin;
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
        if (enemySpawner != null)
        {
            enemySpawner.OnEnemyKilled();
        }
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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : LivingEntity
{
    public float deathAnimationTime;
    public Animator _anim;

    [SerializeField] protected List<MonoBehaviour> componentsToDisable;

    protected EnemyLootDrop lootDrop;

    protected virtual void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        lootDrop = GetComponent<EnemyLootDrop>();
    }

    public override void TakeDamage(float damage, bool isCrit = false)
    {
        float finalDamage = Mathf.Max(damage - defense, 1);

        currentHealth = Mathf.Max(currentHealth - finalDamage, 0);
        onHealthChange?.Invoke(currentHealth, maxHealth);
        DamageUIManager.Instance.ShowDamageUI(finalDamage, GetComponent<Collider>(), isCrit);
        if (currentHealth <= 0 && !IsDead)
        {
            Die();
        }
    }

    protected override void Die()
    {
        base.Die();
        DisableEnemyActions();
        StartCoroutine(HandleDeath());

        EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();

        if (enemySpawner != null)
            enemySpawner.OnEnemyKilled();
    }

    protected virtual IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(deathAnimationTime);

        //Rơi coin thường
        if (lootDrop != null)
        {
            lootDrop.DropNormalLoot(transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

    protected virtual void DisableEnemyActions()
    {
        foreach (var comp in componentsToDisable)
        {
            comp.enabled = false;
        }

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.velocity = Vector3.zero;
    }
}

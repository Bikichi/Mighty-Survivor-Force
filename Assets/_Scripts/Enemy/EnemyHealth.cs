using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : LivingEntity
{
    public float deathAnimationTime;
    public Animator _anim;

    [SerializeField] protected List<MonoBehaviour> componentsToDisable;
    [SerializeField] protected DamageUIManager damageUIManager;

    protected EnemyLootDrop lootDrop;
    protected override void Awake()
    {
        base.Awake();
        damageUIManager = FindAnyObjectByType<DamageUIManager>();
    }
    protected virtual void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        lootDrop = GetComponent<EnemyLootDrop>();
    }

    public override void TakeDamage(float damage, bool isCrit = false)
    {
        AudioController.Instance.PlaySound(AudioController.Instance.hitEnemy);
        float finalDamage = Mathf.Max(damage - defense, 1);

        currentHealth = Mathf.Max(currentHealth - finalDamage, 0);
        onHealthChange?.Invoke(currentHealth, maxHealth);
        damageUIManager.ShowDamageUI(finalDamage, GetComponent<Collider>(), isCrit);
        if (currentHealth <= 0 && !IsDead)
        {
            Die();
        }
    }

    protected override void Die()
    {
        base.Die();
        AudioController.Instance.PlaySound(AudioController.Instance.enemyDeath);
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

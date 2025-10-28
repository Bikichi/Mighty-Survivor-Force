using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    [SerializeField] private GameObject attackPath;
    [SerializeField] private GameObject statsBars;
    protected override void Start()
    {
        base.Start();
        BossComponentUtils.AddBossComponent<BossBigStrike>(gameObject, componentsToDisable);
    }

    public override void TakeDamage(float damage, bool isCrit = false)
    {
        var phaseController = GetComponent<BossPhaseController>();

        if (phaseController != null && phaseController.isPrePhase2Active)
        {
            DamageUIManager.Instance.ShowDamageUI(0, GetComponent<Collider>(), isCrit);
            return;
        }

        float finalDamage = Mathf.Max(damage - defense, 1);
        float newHealth = currentHealth - finalDamage;

        if (phaseController != null && !phaseController.isPhase2)
        {
            float minHealth = maxHealth * 0.5f;
            if (newHealth < minHealth)
                newHealth = minHealth;
        }

        currentHealth = Mathf.Max(newHealth, 0f);
        onHealthChange?.Invoke(currentHealth, maxHealth);

        //Show damage UI
        DamageUIManager.Instance.ShowDamageUI(finalDamage, GetComponent<Collider>(), isCrit);
        if (currentHealth <= 0 && !IsDead)
        {
            Die();
        }
    }

    protected override IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(deathAnimationTime);

        //boss rơi nhiều coin
        if (lootDrop != null)
        {
            lootDrop.DropBossLoot(transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

    protected override void DisableEnemyActions()
    {
        base.DisableEnemyActions();

        if (attackPath != null)
        {
            attackPath.SetActive(false);
        }
        statsBars.SetActive(false);
    }
}

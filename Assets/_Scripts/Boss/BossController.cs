using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyHealth enemyHealth; // tham chiếu trực tiếp
    [SerializeField] private GameObject phase2AuraPrefab;
    [SerializeField] private Animator bossAnimator;

    [Header("Phase 2 Settings")]
    [SerializeField] private float prePhase2Duration;
    [SerializeField] private List<MonoBehaviour> scriptsToDisableDuringPrePhase2;

    [SerializeField] private bool isPhase2 = false;
    [SerializeField] private bool isPrePhase2Active = false;


    private void Start()
    {
        GetSpecialSkillComponents();
    }
    private void Update()
    {
        CheckPhase2Trigger();
    }
    private void OnValidate()
    {
        scriptsToDisableDuringPrePhase2?.RemoveAll(item => item == null);
    }

    private void CheckPhase2Trigger()
    {
        if (!isPhase2 && !isPrePhase2Active)
        {
            // Kiểm tra HP ≤ 50%
            if (enemyHealth.currentHealth <= enemyHealth.maxHealth * 0.5f)
            {
                StartCoroutine(PrePhase2());
            }
        }
    }

    private IEnumerator PrePhase2()
    {
        isPrePhase2Active = true;

        foreach (var script in scriptsToDisableDuringPrePhase2)
        {
            script.enabled = false;
        }

        bossAnimator.SetBool("isPrePhase2", true);
        phase2AuraPrefab.SetActive(true);

        float originalDefense = enemyHealth.defense;
        enemyHealth.defense *= 9999f;

        yield return new WaitForSeconds(prePhase2Duration);
        
        enemyHealth.defense = originalDefense;

        IncreaseBossStats();

        bossAnimator.SetBool("isPrePhase2", false);

        isPhase2 = true;
        isPrePhase2Active = false;

        foreach (var script in scriptsToDisableDuringPrePhase2)
        {
            script.enabled = true;
        }

        Debug.Log("Boss entered Phase 2!");
    }

    public void GetSpecialSkillComponents()
    {
        BossBigStrike bossStrike = GetComponentInChildren<BossBigStrike>();
        if (bossStrike != null && !scriptsToDisableDuringPrePhase2.Contains(bossStrike))
        {
            scriptsToDisableDuringPrePhase2.Add(bossStrike);
        }
    }

    private void IncreaseBossStats()
    {
        //hệ số
        const float defenseMultiplier = 9999f;
        const float speedMultiplier = 1.5f;
        const float cooldownMultiplier = 1.25f;
        const float damageMultiplier = 1.5f;

        BossHealth bossHealth = GetComponent<BossHealth>();
        MeleeBossMovement movement = GetComponent<MeleeBossMovement>();
        BossChargeSkill chargeSkill = GetComponent<BossChargeSkill>();
        MeleeAttack meleeAttack = GetComponentInChildren<MeleeAttack>();
        BossBigStrike bigStrike = GetComponentInChildren<BossBigStrike>();

        if (bossHealth != null)
            bossHealth.defense *= defenseMultiplier;

        if (movement != null)
            movement.enemyMoveSpeed *= speedMultiplier;

        if (chargeSkill != null)
        {
            chargeSkill.chargeSpeed *= cooldownMultiplier;
            chargeSkill.cooldown /= cooldownMultiplier;
            chargeSkill.windUpTime /= cooldownMultiplier;
        }

        if (meleeAttack != null)
        {
            meleeAttack.attackDamage *= damageMultiplier;
            meleeAttack.attackCooldown /= cooldownMultiplier;
        }

        if (bigStrike != null)
        {
            bigStrike.attackDamage *= damageMultiplier;
            bigStrike.attackCooldown /= cooldownMultiplier;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseController : MonoBehaviour, ISkillStatus
{
    [Header("References")]
    [SerializeField] private BossHealth bossHealth; // tham chiếu trực tiếp
    [SerializeField] private GameObject phase2AuraPrefab;
    [SerializeField] private GameObject attackPath;
    [SerializeField] private Animator bossAnimator;

    [Header("Phase 2 Settings")]
    [SerializeField] private float prePhase2Duration;
    [SerializeField] private List<MonoBehaviour> scriptsToDisableDuringPrePhase2;

    [SerializeField] public bool isPhase2 = false;
    [SerializeField] public bool isPrePhase2Active = false;


    private void Start()
    {
        BossComponentUtils.AddBossComponentInChildren<BossBigStrike>(gameObject, scriptsToDisableDuringPrePhase2);
        bossHealth = GetComponent<BossHealth>();

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
            if (bossHealth.currentHealth <= bossHealth.maxHealth * 0.5f)
            {
                StartCoroutine(PrePhase2());
            }
        }
    }

    private IEnumerator PrePhase2()
    {
        isPrePhase2Active = true;

        AudioController.Instance.PlaySoundMultipleTimes(AudioController.Instance.bossPrePhase2, 2, prePhase2Duration / 2);

        BossFireBreath fireBreath = GetComponent<BossFireBreath>();
        if (fireBreath != null)
            fireBreath.StopFire();
        foreach (var script in scriptsToDisableDuringPrePhase2)
        {
            script.enabled = false;
        }
        bossAnimator.SetBool("isPrePhase2", true);
        phase2AuraPrefab.SetActive(true);

        if (attackPath != null)
        {
            attackPath.SetActive(false);
        }


        yield return new WaitForSeconds(prePhase2Duration);

        BossStatHelper.IncreaseBossStats(gameObject);

        bossAnimator.SetBool("isPrePhase2", false);

        isPhase2 = true;
        isPrePhase2Active = false;

        foreach (var script in scriptsToDisableDuringPrePhase2)
        {
            script.enabled = true;
        }

        //Debug.Log("Boss entered Phase 2!");
    }
    public virtual bool IsActive()
    {
        return isPrePhase2Active;
    }
}

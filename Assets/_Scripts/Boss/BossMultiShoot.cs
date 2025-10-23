using System.Collections.Generic;
using UnityEngine;

public class BossMultiShoot : MonoBehaviour, ISkillStatus
{
    [Header("Spawn Settings")]
    public Transform[] spawnPoints;
    public GameObject bulletPrefab;

    [Header("Shoot Settings")]
    [SerializeField] private float fireCooldown;
    [SerializeField] private float fireTimer = 0f;
    [SerializeField] private bool isShooting;
    
    [Header("Animation")]
    [SerializeField] private Animator bossAnimator;
    [SerializeField] private string shootTriggerName = "Shoot";
    [SerializeField] private float shootAnimDuration;

    [Header("Boss Skills")]
    public List<MonoBehaviour> bossSkills = new List<MonoBehaviour>();

    public List<ISkillStatus> skillStatusList = new List<ISkillStatus>();

    private void Awake()
    {
        BossComponentUtils.AddBossComponent<BossBigStrike>(gameObject, bossSkills);
        //chỉ lấy những script nào có implement ISkillStatus
        foreach (var skill in bossSkills)
        {
            if (skill is ISkillStatus skillStatus)
            {
                skillStatusList.Add(skillStatus);
            }
        }
    }

    private void Start()
    {
        bossAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        fireTimer += Time.deltaTime;
        if (IsAnySkillActive())
        {
            return;
        }
        if (fireTimer >= fireCooldown)
        {
            FireMulti();
            fireTimer = 0f;
        }
    }

    public void FireMulti()
    {
        isShooting = true;
        bossAnimator.SetTrigger(shootTriggerName);
        foreach (Transform spawn in spawnPoints)
        {
            if (spawn == null) continue;
            Instantiate(bulletPrefab, spawn.position, spawn.rotation);
        }
        Invoke(nameof(EndShooting), shootAnimDuration); // tắt sau 1 khoảng animation duration
    }

    private void EndShooting()
    {
        isShooting = false;
    }

    public virtual bool IsActive()
    {
        return isShooting;
    }

    private bool IsAnySkillActive()
    {
        foreach (var skill in skillStatusList)
        {
            if (skill.IsActive())
                return true;
        }
        return false;
    }
}

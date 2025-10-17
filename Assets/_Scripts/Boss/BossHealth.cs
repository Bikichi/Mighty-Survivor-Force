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

        AddBossComponent<BossBigStrike>();
        AddBossComponent<BossChargeSkill>();
    }

    //generic thêm component vào danh sách nếu tồn tại và chưa có
    private void AddBossComponent<T>() where T : MonoBehaviour
    {
        T comp = GetComponentInChildren<T>();
        if (comp != null && !componentsToDisable.Contains(comp))
        {
            componentsToDisable.Add(comp);
        }
    }

    protected override void DisableEnemyActions()
    {
        base.DisableEnemyActions();
        attackPath.SetActive(false);
        statsBars.SetActive(false);
    }
}

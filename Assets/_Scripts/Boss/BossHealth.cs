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
        attackPath.SetActive(false);
        statsBars.SetActive(false);
    }
}

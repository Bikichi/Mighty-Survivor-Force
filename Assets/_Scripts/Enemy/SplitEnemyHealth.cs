using System.Collections;
using UnityEngine;

public class SplitEnemyHealth : EnemyHealth
{
    private SplitEnemySpawner splitSpawner;

    protected override void Start()
    {
        base.Start();
        splitSpawner = GetComponent<SplitEnemySpawner>();
    }

    protected override IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(deathAnimationTime);

        if (lootDrop != null)
            lootDrop.DropNormalLoot(transform.position, transform.rotation);

        //gọi hàm sinh quái con khi chết
        splitSpawner.SpawnChildren(transform.position, transform.rotation);

        // Hủy bản thân
        Destroy(gameObject);
    }
}

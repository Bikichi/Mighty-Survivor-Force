using System.Collections;
using UnityEngine;

public class SplitEnemyHealth : EnemyHealth
{
    [SerializeField] private SplitEnemySpawner splitSpawner;

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
        if (splitSpawner != null) 
            splitSpawner.SpawnChildren(transform.position, transform.rotation);

        // Hủy bản thân
        Destroy(gameObject);
    }
}

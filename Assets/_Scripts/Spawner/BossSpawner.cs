using UnityEngine;
using System.Collections;

public class BossSpawner : MonoBehaviour
{
    [Header("Boss Settings")]
    [SerializeField] private Transform bossSpawnPoint;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float delayBeforeSpawn = 4f; // thời gian trễ trước khi boss xuất hiện để hiển thị ui

    [SerializeField] private GameObject currentBoss;
    [SerializeField] private bool hasSpawned;

    public void SpawnBoss(GameObject bossPrefab)
    {
        if (hasSpawned) return;

        StartCoroutine(SpawnBossWithDelay(bossPrefab));
    }

    private IEnumerator SpawnBossWithDelay(GameObject bossPrefab)
    {
        yield return new WaitForSeconds(delayBeforeSpawn); 

        currentBoss = Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);

        BossHealth bossHealth = currentBoss.GetComponent<BossHealth>();
        bossHealth.onDeath.AddListener(OnBossKilled);

        hasSpawned = true;
    }

    private void OnBossKilled()
    {
        hasSpawned = false;
        currentBoss = null;
    }
}

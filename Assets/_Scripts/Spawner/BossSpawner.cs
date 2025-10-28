using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [Header("Boss Settings")]
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform bossSpawnPoint;
    [SerializeField] private EnemySpawner enemySpawner;

    private GameObject currentBoss;
    private bool hasSpawned;

    void Start()
    {
        if (bossPrefab == null || bossSpawnPoint == null)
        {
            Debug.LogWarning("⚠️ BossSpawner: Chưa gán prefab hoặc spawn point!");
        }
        Invoke("SpawnBoss", 3f);
    }

    public void SpawnBoss()
    {
        if (hasSpawned)
            return;

        currentBoss = Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);

        enemySpawner.enemiesAlive++;

        BossHealth bossHealth = currentBoss.GetComponent<BossHealth>();
        //khi boss chết thì báo lại EnemySpawner
        bossHealth.onDeath.AddListener(OnBossKilled);

        hasSpawned = true;
    }

    private void OnBossKilled()
    {
        enemySpawner.OnEnemyKilled(); // cập nhật totalEnemiesKilled + enemiesAlive--

        hasSpawned = false;
        currentBoss = null;
    }
}

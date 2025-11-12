using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public float waveInterval = 5f;
    public float spawnDistance = 10f;

    [Header("Runtime Data")]
    public int enemiesAlive;
    public int currentWaveIndex;
    public float totalEnemiesKilled;
    
    [SerializeField] private GameObject waveAlertUI;
    [SerializeField] private ScoreUI scoreUI;
    [SerializeField] private BossSpawner bossSpawner;
    [SerializeField] private CheckAlertUIManager checkAlertUIManager;



    public UnityEvent onWaveCompleted;

    private bool _isWaveTransitioning;
    private float spawnTimer;

    public List<WaveData> waves = new List<WaveData>();

    public Transform[] spawnPositions;
    public List<Transform> unusedSpawnPoints = new List<Transform>();

    void Awake()
    {
        waves.AddRange(GetComponentsInChildren<WaveData>(true));
        checkAlertUIManager = FindAnyObjectByType<CheckAlertUIManager>();
    }

    void Start()
    {
        SpawnPointManager.Instance.ResetSpawnPoints(unusedSpawnPoints, spawnPositions);
        CheckBossSpawn(waves[currentWaveIndex]);
    }

    void Update()
    {
        if (_isWaveTransitioning) return;
        if (currentWaveIndex >= waves.Count) return;

        WaveData currentWave = waves[currentWaveIndex];

        if (currentWave.IsCompleted() && enemiesAlive == 0 && !checkAlertUIManager.IsAnyWaveAlertActive())
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= currentWave.turnInterval && enemiesAlive < currentWave.maxEnemiesAllowed && !checkAlertUIManager.IsAnyWaveAlertActive())
        {
            SpawnEnemies(currentWave);
        }
    }


    IEnumerator BeginNextWave()
    {
        _isWaveTransitioning = true;
        yield return new WaitForSeconds(waveInterval);

        currentWaveIndex++;

        //nếu đã hoàn thành toàn bộ waves
        if (currentWaveIndex >= waves.Count)
        {
            GameFlowManager.Instance.OnAllWavesCompleted();
            yield break;
        }
        else
        {
            //nếu vẫn còn wave tiếp theo
            spawnTimer = 0f;
            yield return new WaitForSeconds(2.0f);
            FindObjectOfType<ShowLearningSkillUI>().Show();
            yield return new WaitForSeconds(0.25f);
            onWaveCompleted?.Invoke();
            CheckBossSpawn(waves[currentWaveIndex]);
        }
        _isWaveTransitioning = false;
    }

    private void CheckBossSpawn(WaveData wave)
    {
        if (wave == null || bossSpawner == null) return;

        if (wave.hasBoss && wave.bossPrefab != null)
        {
            bossSpawner.SpawnBoss(wave.bossPrefab);
            enemiesAlive++;
            wave.totalSpawned++;
        }
    }

    private void SpawnEnemies(WaveData wave)
    {
        if (wave.IsCompleted()) return;

        TurnData turn = wave.GetCurrentTurn();
        if (turn == null) return;
        if (enemiesAlive >= wave.maxEnemiesAllowed) return;

        foreach (var type in turn.enemyTypes)
        {
            if (type.spawnedCount < type.enemyCount)
            {
                var spawnPos = SpawnPointManager.Instance.GetSpawnPositionFarFromPlayer(unusedSpawnPoints, spawnDistance, spawnPositions);
                Instantiate(type.enemyPrefab, spawnPos, Quaternion.identity);
                AudioController.Instance.PlaySound(AudioController.Instance.enemySpawn);
                enemiesAlive++;
                type.spawnedCount++;
                wave.totalSpawned++;

                if (enemiesAlive >= wave.maxEnemiesAllowed)
                    break;
            }
        }

        spawnTimer = 0f;

        if (turn.IsCompleted())
        {
            wave.NextTurn();
            SpawnPointManager.Instance.ResetSpawnPoints(unusedSpawnPoints, spawnPositions);
        }
    }


    public void OnEnemyKilled()
    {
        enemiesAlive--;
        totalEnemiesKilled++;;

        scoreUI.UpdateScoreUI();

        WaveData currentWave = waves[currentWaveIndex];
        currentWave.enemiesKilled++;

        //spawn tiếp nếu chưa đạt giới hạn của wave
        if (enemiesAlive < currentWave.maxEnemiesAllowed && spawnTimer >= currentWave.turnInterval)
        {
            SpawnEnemies(currentWave);
        }
    }
    public List<WaveData> GetAllWaves() => waves;
}

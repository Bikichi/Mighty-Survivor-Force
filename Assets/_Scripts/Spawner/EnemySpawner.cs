using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; 
        public int waveQuota; //Tổng số các quái vật được sinh ra ở wave này
        public float spawnInterval; //Khoảng thời gian giữa các lần sinh quái trong wave
        public float spawnCount; //số quái trong wave đã được spawn 
    }

    [System.Serializable]
    public class EnemyGroup //Class quản lý các thông tin của enemy
    {
        public string enemyName;
        public int enemyCount; //tổng số quái sẽ spwan
        public int spawnCount; //số quái trong nhóm đã được spawn
        public GameObject enemyPrefab;
    }

    public List<Wave> waves; //Danh sách của tất cả các wave trong ván đấu
    public int currentWaveCount; //chỉ mục của wave hiện tại

    public Transform[] spawnPositions;

    [Header("Spawner Attributes")]
    public float spawnTimer; //Mốc thời gian spawn
    public float waveInterval; //Khoảng thời gian giũa các wave

    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false;

    public void Start()
    {
        CalculateWaveQuota();
    }
    public void Update()
    {
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount >= waves[currentWaveCount].waveQuota)
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);

        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    public void CalculateWaveQuota()
    {
        int currentWaveQuota = 0; //đặt lại số lượng quái đẫ spawn về 0 khi wave mới bắt đầu
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups) //tính tổng số kẻ địch (waveQuota) của wave hiện tại bằng cách cộng dồn enemyCount của từng enemyGroup. 
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        //Debug.Log(currentWaveQuota);
    }

    public Vector3 GetRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, spawnPositions.Length);
        Vector3 spawnPoint = new Vector3(spawnPositions[randomIndex].position.x,
                                         transform.position.y,
                                         spawnPositions[randomIndex].position.z);
        return spawnPoint;
    }

    void SpawnEnemies()
    {
        if (waves[currentWaveCount].spawnCount >= waves[currentWaveCount].waveQuota)
        {
            return; // Nếu đã spawn đủ quái trong wave, dừng lại
        }

        if (enemiesAlive >= maxEnemiesAllowed)
        {
            maxEnemiesReached = true;
            return;
        }

        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            if (enemyGroup.spawnCount < enemyGroup.enemyCount)
            {
                Instantiate(enemyGroup.enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);
                enemiesAlive++;
                enemyGroup.spawnCount++;
                waves[currentWaveCount].spawnCount++;

                if (enemiesAlive >= maxEnemiesAllowed)
                {
                    maxEnemiesReached = true;
                    break;
                }
            }
        }
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;
        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false; // Cho phép spawn thêm nếu chưa đạt giới hạn
            SpawnEnemies(); // Gọi spawn ngay khi có slot trống
        }
    }
}
    
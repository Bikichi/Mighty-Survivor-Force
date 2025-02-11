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
        public float spawnInterval; //Khoảng thời gian sinh quái
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


    public void Start()
    {
        CalculateWaveQuota();
        SpawnEnemies();
    }

    public void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups) //tính tổng số kẻ địch (waveQuota) của wave hiện tại bằng cách cộng dồn enemyCount của từng enemyGroup. 
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }

    public Transform GetRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, spawnPositions.Length);
        Transform spawnPoint = spawnPositions[randomIndex];
        return spawnPoint;
    }

    void SpawnEnemies()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota)
        {
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    Debug.Log("Spawn!");
                    Instantiate(enemyGroup.enemyPrefab, GetRandomSpawnPosition().position, Quaternion.identity);
                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                }
            }
        }
    }
}
    
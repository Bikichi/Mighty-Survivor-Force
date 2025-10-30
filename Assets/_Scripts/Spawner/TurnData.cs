using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyTypeData
{
    public GameObject enemyPrefab;
    public int enemyCount;
    public int spawnedCount;
}

[System.Serializable]
public class TurnData
{
    public List<EnemyTypeData> enemyTypes = new List<EnemyTypeData>();

    public bool IsCompleted()
    {
        foreach (var type in enemyTypes)
        {
            if (type.spawnedCount < type.enemyCount)
                return false;
        }
        return true;
    }

    public int TotalEnemies()
    {
        int total = 0;
        foreach (var type in enemyTypes)
        {
            total += type.enemyCount;
        }
        return total;
    }
}



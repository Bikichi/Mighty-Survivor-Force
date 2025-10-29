using System.Collections.Generic;
using UnityEngine;

public class WaveData : MonoBehaviour
{
    [Header("Wave Info")]
    public string waveName;
    public float turnInterval = 2f;
    public int maxEnemiesAllowed; //số quái vật tồn tại ở 1 thơi điểm tối đa cho phép
    public List<TurnData> turns = new List<TurnData>();

    [Header("Boss Info")]
    public bool hasBoss;
    public GameObject bossPrefab;

    public int currentTurnIndex = 0;
    public int totalSpawned = 0;
    public int enemiesKilled = 0;

    public TurnData GetCurrentTurn()
    {
        if (currentTurnIndex < turns.Count)
            return turns[currentTurnIndex];
        return null;
    }

    public void NextTurn()
    {
        if (currentTurnIndex < turns.Count - 1)
            currentTurnIndex++;
    }

    public bool IsCompleted()
    {
        return currentTurnIndex >= turns.Count - 1 && totalSpawned >= TotalQuota();
    }

    public int TotalQuota()
    {
        int total = 0;
        foreach (var turn in turns)
        {
            total += turn.TotalEnemies();
        }

        if (hasBoss && bossPrefab != null)
            total += 1;

        return total;
    }
}

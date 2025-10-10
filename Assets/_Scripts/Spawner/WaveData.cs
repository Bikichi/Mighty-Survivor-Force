using System.Collections.Generic;
using UnityEngine;

public class WaveData : MonoBehaviour
{
    [Header("Wave Info")]
    public string waveName;
    public float turnInterval = 2f;
    public int maxEnemiesAllowed; // max quái sống trong wave này
    public List<TurnData> turns = new List<TurnData>();

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
        return total;
    }
}

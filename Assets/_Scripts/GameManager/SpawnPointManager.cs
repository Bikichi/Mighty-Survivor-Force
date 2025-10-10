using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : Singleton<SpawnPointManager>
{
    public void ResetSpawnPoints(List<Transform> unusedSpawnPoints, Transform[] sourcePoints)
    {
        unusedSpawnPoints.Clear();
        unusedSpawnPoints.AddRange(sourcePoints);
    }

    public Vector3 GetRandomSpawnPosition(List<Transform> unusedSpawnPoints)
    {
        int randomIndex = Random.Range(0, unusedSpawnPoints.Count);
        Transform chosen = unusedSpawnPoints[randomIndex];
        unusedSpawnPoints.RemoveAt(randomIndex);
        return chosen.position;
    }

    public Transform[] GetAllSpawnPoints(Transform[] sourcePoints)
    {
        return sourcePoints;
    }
}

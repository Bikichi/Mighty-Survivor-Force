using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : Singleton<SpawnPointManager>
{
    public Transform playerTransform;
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

    public Vector3 GetSpawnPositionFarFromPlayer(List<Transform> unusedSpawnPoints, float minDistance, Transform[] allSpawnPoints)
    {
        if (unusedSpawnPoints.Count == 0)
        {
            SpawnPointManager.Instance.ResetSpawnPoints(unusedSpawnPoints, allSpawnPoints);
        }
        //lọc ra những spawn point đủ xa
        List<Transform> validPoints = new List<Transform>();

        foreach (var point in unusedSpawnPoints)
        {
            float distance = Vector3.Distance(playerTransform.position, point.position);
            if (distance >= minDistance)
            {
                validPoints.Add(point);
            }
        }

        //lấy ngẫu nhiên 1 điểm trong các điểm hợp lệ
        int randomIndex = Random.Range(0, validPoints.Count);
        Transform chosen = validPoints[randomIndex];
        unusedSpawnPoints.Remove(chosen);

        return chosen.position;
    }


    public Transform[] GetAllSpawnPoints(Transform[] sourcePoints)
    {
        return sourcePoints;
    }
}

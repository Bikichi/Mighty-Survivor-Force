using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public static SpawnPointManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    public Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
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
        var farPoints = unusedSpawnPoints
            .Where(p => Vector3.Distance(playerTransform.position, p.position) >= minDistance)
            .ToList();


            //lấy ngẫu nhiên 1 điểm trong các điểm hợp lệ
        int randomIndex = Random.Range(0, farPoints.Count);
        Transform chosen = farPoints[randomIndex];
        unusedSpawnPoints.Remove(chosen);

        return chosen.position;
    }


    public Transform[] GetAllSpawnPoints(Transform[] sourcePoints)
    {
        return sourcePoints;
    }
}

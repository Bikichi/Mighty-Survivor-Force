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

        //Vì tất cẩ SpawnPoints chỉ được reset khi turn spawn xong, mà ở đây lại đang dùng tạm xoá spawnpoint trước đấy đã spawn để tránh trùng
        //nếu số lượng quái trong turn mà nhiều mà số spawnpoints còn lại không đủ hoặc đủ nhưng không thoải mãi điều kiện Distance
        //sẽ gây lỗi crash game nên phải thêm điều kiện kiểm tra

        //nếu KHÔNG có điểm nào thỏa khoảng cách → reset points
        if (farPoints.Count == 0)
        {
            ResetSpawnPoints(unusedSpawnPoints, allSpawnPoints);

            //lấy lại danh sách sau reset
            farPoints = unusedSpawnPoints
                .Where(p => Vector3.Distance(playerTransform.position, p.position) >= minDistance)
                .ToList();

            //nếu vẫn không có, fallback lại cho kĩ
            if (farPoints.Count == 0)
            {
                return allSpawnPoints[Random.Range(0, allSpawnPoints.Length)].position;
            }
        }



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

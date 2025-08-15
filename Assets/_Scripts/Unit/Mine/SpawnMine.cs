using UnityEngine;

public class SpawnMine : MonoBehaviour
{
    [SerializeField] private GameObject minePrefab;
    [SerializeField] private float spawnTimeInterval = 2f;
    [SerializeField] private int maxMines = 5;

    [SerializeField] private float timer;
    [SerializeField] private Transform playerTransform;

    void Start()
    {
        timer = 0f;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTimeInterval)
        {
            GameObject[] mines = GameObject.FindGameObjectsWithTag("Mine");

            if (mines.Length < maxMines)
            {
                SpawnMineAtPlayer();
            }

            timer = 0f;
        }
    }

    void SpawnMineAtPlayer()
    {
        if (playerTransform != null && minePrefab != null)
        {
            Vector3 spawnPos = playerTransform.position;
            Instantiate(minePrefab, spawnPos, Quaternion.identity);
        }
    }
}

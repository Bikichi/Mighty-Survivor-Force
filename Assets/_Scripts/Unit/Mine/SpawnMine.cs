using UnityEngine;
using System.Collections;
using System;

public class SpawnMine : MonoBehaviour
{
    [SerializeField] private GameObject minePrefab;
    [SerializeField] private float spawnTimeInterval = 2f;
    [SerializeField] private int maxMines = 5;

    [SerializeField] private float timer;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float destroyInterval;
    [SerializeField] private float checkRadius;

    void Start()
    {
        timer = spawnTimeInterval / 2;
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
        }
    }

    void SpawnMineAtPlayer()
    {
        Collider[] nearbyObjects = Physics.OverlapSphere(playerTransform.position, checkRadius);

        foreach (var obj in nearbyObjects)
        {
            if (obj.CompareTag("Mine"))
            {
                return;
            }
        }
        Vector3 spawnPos = playerTransform.position;
        var newMine = Instantiate(minePrefab, spawnPos, Quaternion.identity);
        Destroy(newMine, destroyInterval);

        timer = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        if (playerTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(playerTransform.position, checkRadius);
        }
    }
}

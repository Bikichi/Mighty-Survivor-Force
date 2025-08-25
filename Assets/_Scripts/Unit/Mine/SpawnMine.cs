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
            var newMine = Instantiate(minePrefab, spawnPos, Quaternion.identity);
            Destroy(newMine, destroyInterval);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMine : MonoBehaviour
{
    [SerializeField] private GameObject minePrefab;  
    [SerializeField] private float spawnTimeInterval = 2f;

    [SerializeField] private float timer;

    [SerializeField] private Transform playerTransfom;

    void Start()
    {
        timer = 0f;
        playerTransfom = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //if (VariableStatic.isLearnMineSkill = false) return;
        
        timer += Time.deltaTime;

        if (timer >= spawnTimeInterval)
        {
            SpawnMineAtPlayer();
            timer = 0f; 
        }
    }

    void SpawnMineAtPlayer()
    {
        if (playerTransfom != null && minePrefab != null)
        {
            Vector3 spawnPos = playerTransfom.position;
            Instantiate(minePrefab, spawnPos, Quaternion.identity);
        }
    }
}

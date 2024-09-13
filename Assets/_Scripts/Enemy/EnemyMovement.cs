using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject _targetPlayer;
    
    [SerializeField] private GameObject _enemyBullet;

    [SerializeField] private float _speed;

    void Update()
    {
        MoveEnemy();
    }

    public void MoveEnemy()
    {
        if (CheckDistance.Instance.CalculateDistanceFromPlayerToEnemy(_targetPlayer.transform, transform) <= 3.5f)
        {
            return;
        } 
        else
        {
            transform.LookAt(_targetPlayer.transform, Vector3.up);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
    }
}

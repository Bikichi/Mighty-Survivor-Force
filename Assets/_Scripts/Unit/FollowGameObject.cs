using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    [SerializeField] private Transform enemyTransform;

    void Update()
    {
        FollowEnemy();
    }
    public void FollowEnemy()
    {
        transform.position = enemyTransform.position; //đi theo vị trí Enemy
    }

}

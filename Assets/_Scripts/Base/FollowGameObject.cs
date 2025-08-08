using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    [SerializeField] private Transform enemyTransform;

    void Update()
    {
        SawBladeFollow();
    }
    public void SawBladeFollow()
    {
        transform.position = enemyTransform.position; //đi theo vị trí Enemy
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float _speedBullet;

    public void MoveBullet(Transform enemy)
    {
        transform.LookAt(enemy.transform, Vector3.up);
        transform.Translate(Vector3.forward * Time.deltaTime * _speedBullet);
    }

    private void Update()
    {
        MoveBullet(CheckDistance.Instance.FindClosestEnemy());
    }
}

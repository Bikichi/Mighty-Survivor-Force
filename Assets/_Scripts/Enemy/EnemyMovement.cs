using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject _targetPlayer;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject bullet;

    [SerializeField] private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.forward * _speed;
    }

    void Update()
    {
        transform.LookAt(_targetPlayer.transform, Vector3.up);
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);

    }
}

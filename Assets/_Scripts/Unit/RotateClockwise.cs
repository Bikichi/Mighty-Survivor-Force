using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateClockwise : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        transform.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
    }
}
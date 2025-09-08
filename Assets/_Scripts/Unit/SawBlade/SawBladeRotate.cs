using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBladeRotate : MonoBehaviour
{
    public float spinSpeed = 200f;

    void Update()
    {
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime); //xoay quanh trục Y
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation; // Lưu góc quay ban đầu
    }

    void LateUpdate()
    {
        transform.rotation = initialRotation; // Giữ nguyên góc quay ban đầu
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 offset;
    public float smoothTime;
    public Vector3 currentVelocity = Vector3.zero;

    private float xPosMin = -12.5f, xPosMax = 12.5f;

    private void Awake()
    {
        if (followTarget != null)
            offset = transform.position - followTarget.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = followTarget.position + offset;

        // Tính toán vị trí camera mới dựa trên SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        // Giới hạn vị trí camera sau khi SmoothDamp tính toán xong
        ClampXPosCamera();
    }

    public void ClampXPosCamera()
    {
        // Giới hạn giá trị x của camera
        float xPos = Mathf.Clamp(transform.position.x, xPosMin, xPosMax);
        //float zPos = Mathf.Clamp(transform.position.z, zPosMin, zPosMax);

        // Cập nhật vị trí camera với giá trị x đã bị giới hạn
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }
}
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
        MoveCameraFlowPlayer();
    }

    public void MoveCameraFlowPlayer()
    {
        Vector3 targetPosition = followTarget.position + offset;

        // Tính toán vị trí camera mới dựa trên SmoothDamp, chỉ tính trên trục x và y
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        if (followTarget.position.z > 8 || followTarget.position.z < -17)
        {
            // Nếu điều kiện thỏa mãn, không di chuyển camera theo trục z (giữ nguyên vị trí z hiện tại của camera)
            newPosition.z = transform.position.z;
        }

        // Cập nhật vị trí của camera với newPosition
        transform.position = newPosition;

        // Giới hạn vị trí camera sau khi SmoothDamp tính toán xong (giới hạn trục x nếu cần)
        ClampXPosCamera();
    }

    public void ClampZPosCamera()
    {

    }

    public void ClampXPosCamera()
    {
        // Giới hạn giá trị x của camera
        float xPos = Mathf.Clamp(transform.position.x, xPosMin, xPosMax);
        // Cập nhật vị trí camera với giá trị x đã bị giới hạn
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }
}
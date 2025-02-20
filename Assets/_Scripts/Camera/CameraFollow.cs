using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    
    public Vector3 offset;
    public Vector3 currentVelocity = Vector3.zero;

    public float smoothTime;
    public float xPosMin, xPosMax;

    private void Awake()
    {
        FindOffsetPosition();
    }

    private void LateUpdate()
    {
        MoveCameraFlowPlayer();
    }

    public void FindOffsetPosition()
    {
        if (followTarget != null)
            offset = transform.position - followTarget.position;
    }

    public void MoveCameraFlowPlayer()
    {
        Vector3 cameraPosition = followTarget.position + offset;

        // Tính toán vị trí camera mới dựa trên SmoothDamp
        Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, cameraPosition, ref currentVelocity, smoothTime);

        // Nếu chỉ truyền newCameraPosition mà không có từ khóa "ref" thì sẽ chỉ là truyền tham trị của biến vào hàm 
        // truyền tham trị chỉ thay đổi giá trị của biến trong hàm truyền vào mà không thay đổi giá trị của biến gốc
        // sử dụng từ khóa ref truyền tham chiếu sẽ thay đổi cả giá trị của biến gốc, bằng việc truyền địa chỉ của biến gốc
        ClampXPosCamera(ref newCameraPosition);
        ClampZPosCamera(ref newCameraPosition);

        // Cập nhật vị trí camera sau khi giới hạn
        transform.position = newCameraPosition;
    }

    public void ClampXPosCamera(ref Vector3 newPosition)
    {
        // Giới hạn giá trị x của camera
        float xPos = Mathf.Clamp(newPosition.x, xPosMin, xPosMax);
        newPosition.x = xPos; // Cập nhật newPosition
    }

    public void ClampZPosCamera(ref Vector3 newPosition)
    {
        // Giới hạn giá trị z của camera dựa trên điều kiện vị trí của nhận vật. Không thể làm tương tự như trục X bởi vì camera đang nhìn nhân vật theo góc chéo
        if (followTarget.position.z > 8 || followTarget.position.z < -15.5)
        {
            // Nếu điều kiện thỏa mãn, giữ nguyên z, tức là camera không di chuyển theo trục Z
            newPosition.z = transform.position.z;
        }
    }
}
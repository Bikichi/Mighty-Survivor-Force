using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera mainCamera;
    public RectTransform rectTransform;

    private void Awake()
    {
        mainCamera = Camera.main;
        LookTowardCamera();
    }

    private void Update()
    {
        LookTowardCamera();
    }
    public void LookTowardCamera()
    {
        // Xoay đối tượng về phía camera
        transform.forward = mainCamera.transform.forward;

        Vector3 currentRotation = rectTransform.rotation.eulerAngles;

        rectTransform.transform.rotation = Quaternion.Euler(currentRotation.x, 0, currentRotation.z);
    }
}

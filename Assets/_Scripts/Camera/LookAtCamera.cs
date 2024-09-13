using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera mainCamera;
    public void FlowCamera()
    {
        transform.LookAt(mainCamera.transform, Vector3.up);
    }
}

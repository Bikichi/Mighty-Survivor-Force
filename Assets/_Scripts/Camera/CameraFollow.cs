using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform followTarget;
	public Vector3 offset;
	public float smoothTime;
	public Vector3 currentVelocity = Vector3.zero;

	private void Awake()
	{
		if(followTarget != null)
            offset = transform.position - followTarget.position;
	}

    private void LateUpdate()
    {
        Vector3 targetPosition = followTarget.position + offset;	
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
	public Rigidbody myRigidbody;

    public Vector3 velocity;

	void Start()
	{
		myRigidbody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate()
	{
		myRigidbody.MovePosition (myRigidbody.position + velocity * Time.fixedDeltaTime);
	}

	public void Move(Vector3 _velocity)
	{
		velocity = _velocity;
	}

	public void LookAt(Vector3 _lookPoint)
	{
		_lookPoint.y = transform.position.y;
		transform.LookAt (_lookPoint);
	}
}

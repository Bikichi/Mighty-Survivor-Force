using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
[RequireComponent (typeof (WeaponController))]
public class Player : LivingEntity
{
	[Header ("Controls")]
	public float moveSpeed;

	[Header ("Base Values")]
	public Vector3 startingPosition;

	// References
	Camera mainCamera;
	PlayerController controller;
	WeaponController weaponController;
	
	GameObject instantiatedPlayer;

	protected override void Start()
	{
		base.Start ();

		mainCamera = Camera.main;
		controller = GetComponent<PlayerController> ();
		weaponController = GetComponent<WeaponController> ();
		Initialize();

	}

	void Update()
	{
		if (IsActive && !IsDead)
		{
			Vector3 moveInput = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
			Vector3 moveVelocity = moveInput.normalized * moveSpeed;

			controller.Move (moveVelocity);

			Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);
			Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
			float rayDistance;

			if (groundPlane.Raycast (ray, out rayDistance))
			{
				Vector3 point = ray.GetPoint (rayDistance);
				controller.LookAt (point);
			}

			if (Input.GetMouseButton (0))
				weaponController.Shoot ();
		}
	}

	public override void Initialize()
	{
		base.Initialize ();
		transform.position = startingPosition;
	}

	protected override void Die()
	{
		base.Die ();

	}
}

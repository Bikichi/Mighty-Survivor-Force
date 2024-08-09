using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	public Transform weaponHold;
	public Weapon startignWeapon;
	public Weapon equipedWeapon;

	private void Start()
	{
		if (startignWeapon != null)
			EquipWeapon (startignWeapon);
	}

	public void EquipWeapon(Weapon weapon)
	{
		if (equipedWeapon != null)
			Destroy (equipedWeapon.gameObject);

		equipedWeapon = Instantiate(weapon, weaponHold.position, weaponHold.rotation, weaponHold);
	}

	public void Shoot()
	{
		if (equipedWeapon != null)
			equipedWeapon.Shoot ();
	}
}

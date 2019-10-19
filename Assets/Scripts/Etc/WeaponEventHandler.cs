using System;
using UnityEditor;
using UnityEngine;
public class WeaponEventHandler : MonoBehaviour
{
	#region attack_behavior
	public static GameObject bulletPrefab;

	public int curWeaponId = 0;
	public WeaponManager weaponManager;
	Weapon curWeapon;

	void SwitchWeapon()
	{
		if(Input.mouseScrollDelta.y != 0)
		{
			int weaponCount = this.weaponManager.GetWeaponAmount();
			this.curWeaponId += (int) (Input.mouseScrollDelta.y);
			this.curWeaponId = (this.curWeaponId + weaponCount) % weaponCount;

			//Instantiate Weapon to Scence
			this.curWeapon = this.weaponManager.SetUpWeaponInstance(this.curWeaponId, this.gameObject);
		}
	}

	void shoot()
	{
		if(Input.GetMouseButton(0))
		{
			this.curWeapon.Attack();
		}
	}

	void Start()
	{
		this.curWeapon = this.weaponManager.SetUpWeaponInstance(this.curWeaponId, gameObject);
	}

	void Update()
	{
		shoot();
		SwitchWeapon();
	}
	#endregion
}
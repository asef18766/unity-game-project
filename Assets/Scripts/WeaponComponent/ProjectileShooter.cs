using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
	[SerializeField] I_WeaponArgs weaponArg;
	bool blockCoroutine = false;

	IEnumerator onAtk()
	{
		yield return this.weaponArg.atkMethod.Attack(transform.position, transform.rotation, new WaitForSecondsRealtime(this.weaponArg.reload));
		this.weaponArg.ammoRemain--;
		this.blockCoroutine = false;
	}

	void _Shoot()
	{
		CoroutineRunner.RunCoroutine(this.onAtk());
	}

	public bool Shoot()
	{
		if(this.weaponArg.ammoRemain > 0)
		{
			if(!this.blockCoroutine)
			{
				this.blockCoroutine = true;
				this._Shoot();
				return true;
			}
		}
		return false;
	}
}
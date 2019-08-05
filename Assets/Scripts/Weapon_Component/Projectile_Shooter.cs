using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Shooter : MonoBehaviour {
	[SerializeField]I_Weapon_Arg weapon_arg;
	bool block_coroutine=false;

	IEnumerator on_atk()
    {
        yield return weapon_arg.atk_method.Attack(this.transform.position,this.transform.rotation,new WaitForSecondsRealtime(weapon_arg.reload));
		weapon_arg.ammo_remain--;
        block_coroutine=false;
    }
	void _Shoot()
	{
		CoroutineRunner.RunCoroutine(on_atk());
	}
	public bool Shoot()
	{
        if(weapon_arg.ammo_remain>0)
        {
            if(!block_coroutine)
			{
				block_coroutine=true;
				_Shoot();
				return true;
			}
        }
		return false;
	}
}

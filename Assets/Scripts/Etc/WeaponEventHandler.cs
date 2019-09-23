using System;
using UnityEngine;
using UnityEditor;
public class WeaponEventHandler:MonoBehaviour
{
    #region attack_behavior
		public static GameObject bullet_prefab;
		
		public int cur_weapon_id=0;
		public Weapon_Manager weapon_m;
		Weapon cur_weapon;
		void SwitchWeapon()
		{
			if(Input.mouseScrollDelta.y!=0)
			{
				int WP_Count=weapon_m.GetWeaponAmount();
				cur_weapon_id+=(int)(Input.mouseScrollDelta.y);
				cur_weapon_id=(cur_weapon_id+WP_Count)%WP_Count;

				//Instantiate Weapon to Scence
				cur_weapon=weapon_m.SetUpWeaponInstance(cur_weapon_id,this.gameObject);
			}
		}
		void shoot()
		{
			if(Input.GetMouseButton(0))
			{
				cur_weapon.Attack();
			}
		}
        void Start()
        {
            cur_weapon=weapon_m.SetUpWeaponInstance(cur_weapon_id,this.gameObject);
        }
        void Update()
        {
            shoot();
            SwitchWeapon();
        }
	#endregion
}
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName= "Weapons/Create Weapon")]
public class Weapon:I_Weapon
{
    bool block_coroutine=false;
    public void OnEnable()
    {
        ammo_remain=ammo_Capasity;
        atk_method.set_dmg(dmg);
        block_coroutine=false;
    }
    IEnumerator on_atk(Vector3 cur_pos,Quaternion dir)
    {
        yield return atk_method.Attack(cur_pos,dir,new WaitForSecondsRealtime(reload));
        block_coroutine=false;
    }
    public override void Attack(Vector3 cur_pos,Quaternion dir,Vector3[] enemy_pos)
    {
        if(block_coroutine==false)
        {
            block_coroutine=true;
            ammo_remain--;
            Debug.Log("attack!!");
            _I_weapon.GetComponent<WeaponAnimationController>().PlayAnimation();
            CoroutineRunner.RunCoroutine(on_atk(cur_pos,dir));
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class Pistol:I_Weapon
{
    override public void init()
    {
        dmg=10;
        reload=0.1f;
        ammo_Capasity=(float)(1.0/0);
        ammo_remain=ammo_Capasity;
    }
    IEnumerator on_atk(Vector3 cur_pos,Quaternion dir)
    {
        MonoBehaviour.Instantiate(Player.bullet_prefab,cur_pos,dir);
        yield return new WaitForSecondsRealtime(reload);
        block_coroutine=false;
    }
    bool block_coroutine=false;
    public override void Attack(Vector3 cur_pos,Quaternion dir,Vector3[] enemy_pos)
    {
        if(block_coroutine==false)
        {
            block_coroutine=true;
            CoroutineRunner.RunCoroutine(on_atk(cur_pos,dir));
        }
            
    }
    
}
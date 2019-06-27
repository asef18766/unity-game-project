using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Collections;
public abstract class I_Weapon:ScriptableObject
{
    public int dmg;
    public float reload;
    public float ammo_Capasity;
    public float ammo_remain;
    public I_AtkMethod atk_method; 
    public GameObject skin;

    public GameObject _I_weapon;
    public abstract void Attack(Vector3 cur_pos,Quaternion dir,Vector3[] enemy_pos);
    public virtual void Act(Vector3 cur_pos,Quaternion dir,Vector3[] enemy_pos)
    {
        if(ammo_remain>0)
        {
            Attack(cur_pos,dir,enemy_pos);
        }
    }
}
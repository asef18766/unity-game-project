using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(menuName= "Weapons/Create WeaponManager")]
public class Weapon_Manager:ScriptableObject
{
    public List<I_Weapon> weapon_list=new List<I_Weapon>();
    public I_Weapon request(int id)
    {
        if( id<weapon_list.Count && id>=0)
            return weapon_list[id];
        return null;
    }
}

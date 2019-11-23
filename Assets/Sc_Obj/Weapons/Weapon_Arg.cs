using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName= "Weapons/Create Weapon Argument")]
public class Weapon_Arg:I_WeaponArgs
{
    public void OnEnable()
    {
        ammoRemain=ammo_Capasity;
        atkMethod.set_dmg(dmg);
    }
}
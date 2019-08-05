using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName= "Weapons/Create Weapon Argument")]
public class Weapon_Arg:I_Weapon_Arg
{
    public void OnEnable()
    {
        ammo_remain=ammo_Capasity;
        atk_method.set_dmg(dmg);
    }
}
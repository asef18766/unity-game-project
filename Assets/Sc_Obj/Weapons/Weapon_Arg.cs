using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Create Weapon Argument")]
public class WeaponArg : I_WeaponArgs
{
    public void OnEnable()
    {
        ammoRemain = ammoCapasity;
        atkMethod.set_dmg(dmg);
    }
}
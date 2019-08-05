using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Collections;
public abstract class I_Weapon_Arg:ScriptableObject
{
    public int dmg;
    public float reload;
    public float ammo_Capasity;
    public float ammo_remain;
    public I_AtkMethod atk_method;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Collections;
public abstract class I_WeaponArgs : ScriptableObject
{
    public int dmg;
    public float reload;
    public float ammoCapasity;
    public float ammoRemain;
    public I_AtkMethod atkMethod;
}
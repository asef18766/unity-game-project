using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack Method/Create I_Attack_Method")]
public class I_AtkMethod : ScriptableObject
{
    protected float dmg;
    public void set_dmg(float val)
    {
        dmg = val;
    }
    virtual public IEnumerator Attack(Vector3 curPos, Quaternion dir, IEnumerator callback)
    {
        yield return callback;
    }
}
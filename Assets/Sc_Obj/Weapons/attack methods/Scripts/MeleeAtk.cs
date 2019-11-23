using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Atk : I_AtkMethod
{
    override public IEnumerator Attack(Vector3 curPos, Quaternion dir, IEnumerator callback)
    {
        yield return callback;
    }
}
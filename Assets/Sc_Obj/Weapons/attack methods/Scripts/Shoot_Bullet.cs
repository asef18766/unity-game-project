using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "Attack Method/Create Shoot_Bullet")]
public class Shoot_Bullet:I_AtkMethod
{
    public Bullet bullet;
    override public IEnumerator Attack(Vector3 cur_pos,Quaternion dir,IEnumerator callback)
    {
        Bullet ins=MonoBehaviour.Instantiate(bullet,cur_pos,dir);
        ins.atk=dmg;
        yield return callback;
    }
}
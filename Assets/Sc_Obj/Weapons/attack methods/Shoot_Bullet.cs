using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "Attack Method/Create Shoot_Bullet")]
public class Shoot_Bullet:I_AtkMethod
{
    void OnEnable()
    {
        bullet.atk=dmg;
    }
    public Bullet bullet;
    override public IEnumerator Attack(Vector3 cur_pos,Quaternion dir,IEnumerator callback)
    {
        MonoBehaviour.Instantiate(bullet,cur_pos,dir);
        yield return callback;
    }
}
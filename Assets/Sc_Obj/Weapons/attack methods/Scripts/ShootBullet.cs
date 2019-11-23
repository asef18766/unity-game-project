using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack Method/Create ShootBullet")]
public class ShootBullet : I_AtkMethod
{
    public Bullet bullet;
    override public IEnumerator Attack(Vector3 curPos, Quaternion dir, IEnumerator callback)
    {
        Bullet ins = MonoBehaviour.Instantiate(bullet, curPos, dir);
        ins.atk = dmg;
        yield return callback;
    }
}
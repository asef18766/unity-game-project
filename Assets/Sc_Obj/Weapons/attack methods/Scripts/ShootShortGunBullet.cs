using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack Method/Create Shoot ShortGun Bullet")]
public class ShootShortGunBullet : I_AtkMethod
{
    void OnEnable()
    {
        degree = new float[bulletAmount];

        float ang = extentionAngle / (bulletAmount - 1);
        degree[0] = 0;
        if(bulletAmount % 2 == 0)
        {
            degree[0] -= ang / 2;
            degree[0] -= (ang * (bulletAmount - 2) / 2);
        }
        else
        {
            degree[0] -= (ang * (bulletAmount - 1) / 2);
        }
        for(int u = 1; u != bulletAmount; ++u)
        {
            degree[u] = (degree[0]) + ang * u;
        }
    }
    public float extentionAngle = 90;
    public int bulletAmount;
    public Bullet bullet;
    float[] degree;
    override public IEnumerator Attack(Vector3 curPos, Quaternion dir, IEnumerator callback)
    {
        for(int u = 0; u != bulletAmount; ++u)
        {
            Bullet ins = MonoBehaviour.Instantiate(bullet, curPos, Quaternion.Euler(dir.eulerAngles + new Vector3(0, 0, degree[u])));
            ins.atk = dmg;
        }
        yield return callback;
    }
}
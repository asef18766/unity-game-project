using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "Attack Method/Create Shoot ShortGun Bullet")]
public class Shoot_ShortGunBullet:I_AtkMethod
{
    void OnEnable()
    {
        degree=new float[bullet_amount];
        
        float ang=extention_angle/(bullet_amount-1);
        degree[0]=0;
        if(bullet_amount%2==0)
        {
            degree[0]-=ang/2;
            degree[0]-=(ang*(bullet_amount-2)/2);
        }
        else
        {
            degree[0]-=(ang*(bullet_amount-1)/2);
        }
        for(int u=1;u!=bullet_amount;++u)
        {
            degree[u]=(degree[0])+ang*u;
        }
    }
    public float extention_angle=90;
    public int bullet_amount;
    public Bullet bullet;
    float[] degree;
    override public IEnumerator Attack(Vector3 cur_pos,Quaternion dir,IEnumerator callback)
    {
        for(int u=0;u!=bullet_amount;++u)
        {
            Bullet ins=MonoBehaviour.Instantiate(bullet,cur_pos,Quaternion.Euler(dir.eulerAngles+new Vector3(0,0,degree[u])));
            ins.atk=dmg;
        }   
        yield return callback;
    }
}
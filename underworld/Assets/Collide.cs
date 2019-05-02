using System;
using System.Collections.Generic;
using UnityEngine;
class Collide
{
    public enum Method{Box,Circle};
    public static Collider2D[] AreaGetCollideByTag(Vector3 start_pos,float radius,Method m,string tag)
    {
        List<Collider2D> ret=new List<Collider2D>();
        Collider2D[] r=null;

        if(m==Method.Circle)
            r=Physics2D.OverlapCircleAll(start_pos,radius);
        else if(m==Method.Circle)
            r=Physics2D.OverlapBoxAll(start_pos,new Vector2(radius,radius),0);
        
        for(int u=0;u!=r.Length;++u)
        {
            if(r[u].tag==tag)
                ret.Add(r[u]);
        }
        return ret.ToArray();
    }
    public static Collider2D[] RayGetCollideByTag(Vector3 start_pos,float dis,Vector3 dir,string tag)
    {
        List<Collider2D> ret=new List<Collider2D>();
        RaycastHit2D[] rh=Physics2D.RaycastAll(start_pos,dir,dis);
        for(int u=0;u!=rh.Length;++u)
        {
            if(rh[u].collider.tag==tag)
            ret.Add(rh[u].collider);
        }
        return ret.ToArray();
    }
}
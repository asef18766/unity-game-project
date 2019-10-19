using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
class Collide
{
    public enum Method { Box, Circle }

    public static Collider2D[] AreaGetCollideByTag(Vector3 startPos, float radius, Collide.Method m, string tag)
    {
        Collider2D[] r = null;

        if(m == Collide.Method.Circle)
            r = Physics2D.OverlapCircleAll(startPos, radius);
        else if(m == Collide.Method.Box)
            r = Physics2D.OverlapBoxAll(startPos, new Vector2(radius, radius), 0);

        // filter collider by tag
        r = r
            .Select(c => c)
            .Where(c => c.tag.Equals(tag))
            .ToArray();

        return r;
    }

    public static Collider2D[] RayGetCollideByTag(Vector3 startPos, float dis, Vector3 dir, string tag)
    {
        Collider2D[] ret = Physics2D.RaycastAll(startPos, dir, dis)
            .Select(h => h.collider)
            .Where(c => c.tag.Equals(tag))
            .ToArray();
        return ret;
    }
}
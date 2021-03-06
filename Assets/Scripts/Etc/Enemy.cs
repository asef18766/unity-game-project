using System;
using UnityEngine;
public class Enemy : Entity
{
    public int type = 0;
    public float health = 50.0f;

    // Use this for initialization
    void Start()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void destroy()
    {
        DestroyImmediate(this.gameObject);
        DestroyImmediate(this);
    }

    void Update()
    {
        if(health <= 0)
            destroy();
    }

    void hurt(float dmg)
    {
        Debug.Log("hurt for " + dmg.ToString() + " point");
        health -= dmg;
    }

    public override void interact(string behavior, int[] arg)
    {
        if(behavior.Equals("HURT"))
            hurt(arg[0]);
    }
}
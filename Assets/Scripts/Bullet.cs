using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity {
	public string tar_tag="";
	public int atk=0;
	public float vec=0.0f;
	public float destroy_time=0.0f;
	public bool destroy=false;
	public static string[] D_objtag;
	Vector3 dir;
	void move()
	{
		dir=transform.position;
		transform.localPosition+=new Vector3(vec,0,0);
		dir=transform.position-dir;
	}
	public override void spirite_update()
	{
		move();
	}
	
	// Use this for initialization
	void Start ()
	{
		StartCoroutine(Ondestroy());
	}
	IEnumerator Ondestroy()
	{
		yield return new WaitForSeconds(destroy_time);
		destroy=true;
	}
	// Update is called once per frame
	void Update () 
	{
		spirite_update();
		if(OnEnter())
			interact_other();
		if(destroy)
		{
			DestroyImmediate(this.gameObject);
			DestroyImmediate(this);
		}
	}
	void interact_other()
	{
		for(int u=0;u!=hit_enemy.Length;++u)
		{
			
		}
	}
	Collider2D[] hit_enemy;
	Collider2D[] hit_player;
	Collider2D[] hit_building;
	bool OnEnter()
	{
		hit_enemy   =Collide.RayGetCollideByTag(transform.position,transform.localScale.magnitude,dir,"enemy" );
		hit_player  =Collide.RayGetCollideByTag(transform.position,transform.localScale.magnitude,dir,"player");
		hit_building=Collide.RayGetCollideByTag(transform.position,transform.localScale.magnitude,dir,"building");
		return hit_enemy.Length!=0;
	}
	public override void interact(string behavior)
	{
		if(behavior=="destroy")
			destroy=true;
	}
}

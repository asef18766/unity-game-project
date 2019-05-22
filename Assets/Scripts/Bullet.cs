using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity {
	public int atk=0;
	public float vec=2.0f;
	public float destroy_time=0.1f;
	public bool destroy_f=false;
	public static string[] D_objtag;
	Vector3 dir;
	void move()
	{
		dir=transform.position;
		transform.Translate(new Vector3(vec,0,0));
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
		yield return new WaitForSecondsRealtime(destroy_time);
		destroy_f=true;
	}
	// Update is called once per frame
	void Update () 
	{
		spirite_update();
		if(OnEnter())
			interact_other();
		if(destroy_f)
		{
			DestroyImmediate(this.gameObject);
			DestroyImmediate(this);
		}
	}
	void interact_other()
	{
		//enemy process
		for(int u=0;u!=hit_enemy.Length;++u)
		{
			hit_enemy[u].GetComponent<Enemy>().interact("HURT",new int[1]{atk});
			destroy_f=true;
		}
		
		//building process
		if(hit_building.Length!=0)
		{
			destroy_f=true;
		}

	}
	Collider2D[] hit_enemy;
	Collider2D[] hit_player;
	Collider2D[] hit_building;
	
	bool OnEnter()
	{
		float d_r=GetComponent<SpriteRenderer>().bounds.size.magnitude;
		hit_enemy   =Collide.RayGetCollideByTag(transform.position,d_r,dir,"enemy" );
		hit_player  =Collide.RayGetCollideByTag(transform.position,d_r,dir,"player");
		hit_building=Collide.RayGetCollideByTag(transform.position,d_r,dir,"building");

		//TODO:impove return method
		return hit_enemy.Length!=0||hit_building.Length!=0||hit_player.Length!=0;
	}
	public override void interact(string behavior,int[] arg)
	{
		if(behavior=="DESTROY")
			destroy_f=true;
	}
}

using System;
using UnityEngine;
public class Player : Entity {
	// Use this for initialization
	public float detect_radius=40;
	public enum  walk_dir{UP,DOWN,LEFT,RIGHT};
	public GameObject sp;
	
	Transform tf;
	Vector2 moving_dir;
	
	void Start () 
	{
		tf=GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		move(playerControler());
		interact();
		spirite_update();
	}
	protected override void spirite_update()
	{
		Vector2 v2 = Camera.main.ScreenToViewportPoint( Input.mousePosition );
		v2.x-=0.5f;
		v2.y-=0.5f;
		float ang=(float)(Math.Acos(v2.normalized.x)*360/(Math.PI*2));
		if(v2.y<0)
			ang=-ang;
		Debug.Log("ang:"+ang);
		ang=(ang+360)%360;
		tf.rotation=Quaternion.Euler(0,0,ang);
		Debug.Log(v2);
	}
	walk_dir[] playerControler()
	{
		walk_dir[] ret=new walk_dir[4];
		int element=0;
		if(Input.GetKey(KeyCode.A))
		{
			ret[element]=walk_dir.LEFT;
			element++;
		}
		if(Input.GetKey(KeyCode.W))
		{
			ret[element]=walk_dir.UP;
			element++;
		}
		if(Input.GetKey(KeyCode.D))
		{
			ret[element]=walk_dir.RIGHT;
			element++;
		}
		if(Input.GetKey(KeyCode.S))
		{
			ret[element]=walk_dir.DOWN;
			element++;
		}
		if(element!=4)
			Array.Resize(ref ret,element);
		return ret;
	}
	void move(walk_dir[] w_dir)
	{
		float x=tf.position.x,
			  y=tf.position.y;
		
		for(int u=0;u!=w_dir.Length;++u)
		{
			if(w_dir[u]==walk_dir.LEFT)
				tf.position+=((new Vector3(-1, 0, 0))*Time.deltaTime*tf.localScale.x);

			if(w_dir[u]==walk_dir.UP)
				tf.position+=((new Vector3( 0, 1, 0))*Time.deltaTime*tf.localScale.x);

			if(w_dir[u]==walk_dir.RIGHT)
				tf.position+=((new Vector3( 1, 0, 0))*Time.deltaTime*tf.localScale.x);

			if(w_dir[u]==walk_dir.DOWN)
				tf.position+=((new Vector3( 0,-1, 0))*Time.deltaTime*tf.localScale.x);
		}
		
		moving_dir.x=tf.position.x-x;
		moving_dir.y=tf.position.y-y;
		//Debug.Log(dir);
	}
	protected override void interact(string behavior)
	{
		Collider2D[] enemy=Collide.AreaGetCollideByTag(tf.position,detect_radius,Collide.Method.Circle,"enemy");
		Collider2D[] npc  =Collide.AreaGetCollideByTag(tf.position,detect_radius,Collide.Method.Circle,"npc");
		Collider2D[] item =Collide.AreaGetCollideByTag(tf.position,detect_radius,Collide.Method.Circle,"item");

		if(enemy.Length!=0)
		{
			sp.transform.position=enemy[0].transform.position;
		}
		else
		{
			sp.transform.position=tf.position;
		}
	}
}

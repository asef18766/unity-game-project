using System;
using UnityEngine;
public class enity : MonoBehaviour {
	// Use this for initialization
	Transform tf;
	Vector2 dir;
	public enum  walk_dir{UP,DOWN,LEFT,RIGHT};
	void Start () {
		tf=GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		move(playerControler());
		interact();
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
				tf.Translate((new Vector3(-1, 0, 0))*Time.deltaTime*tf.localScale.x);

			if(w_dir[u]==walk_dir.UP)
				tf.Translate((new Vector3( 0, 1, 0))*Time.deltaTime*tf.localScale.x);

			if(w_dir[u]==walk_dir.RIGHT)
				tf.Translate((new Vector3( 1, 0, 0))*Time.deltaTime*tf.localScale.x);

			if(w_dir[u]==walk_dir.DOWN)
				tf.Translate((new Vector3( 0,-1, 0))*Time.deltaTime*tf.localScale.x);
		}
		
		dir.x=tf.position.x-x;
		dir.y=tf.position.y-y;
		//Debug.Log(dir);
	}
	void interact()
	{
		float le=(float)Math.Sqrt(dir.x*dir.x+dir.y*dir.y);
		float sin=dir.y/le;
		float cos=dir.x/le;

		RaycastHit2D[] r={
			Physics2D.Raycast(new Vector2(tf.position.x,tf.position.y),dir,500,10,0),
			Physics2D.Raycast(new Vector2(tf.position.x,tf.position.y),new Vector2(cos-sin,cos+sin),500,10,0),
			Physics2D.Raycast(new Vector2(tf.position.x,tf.position.y),new Vector2(cos+sin,cos-sin),500,10,0)
			};
		
		for( int u=0 ; u!=3 ; ++u )
			if(r[u].collider)
			{
				Debug.DrawLine(tf.position,r[u].transform.position,Color.red,0.1f,true);
				switch(r[u].collider.tag)
				{
					case "enemy":
						Debug.Log("enemy spotted.");
						break;
					case "npc":
						Debug.Log("let's having talk.");
						break;
					case "item":
						Debug.Log("something is on the floor.");
						break;
				}
			}
	}
}

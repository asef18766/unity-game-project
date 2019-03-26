using System;
using UnityEngine;
public class enity : MonoBehaviour {
	// Use this for initialization
	public float detect_radius=40;
	public GameObject sp;
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
		Collider2D[] r=Physics2D.OverlapCircleAll(tf.position,detect_radius);
		for( int u=0 ; u!=r.Length ; ++u )
			if(r[u])
				switch(r[u].tag)
				{
					case "enemy":
						Debug.Log("enemy spotted.");
						sp.transform.position=r[u].transform.position;
						break;
					case "npc":
						Debug.Log("let's having talk.");
						break;
					case "item":
						Debug.Log("something is on the floor.");
						break;
				}
		
		if(r.Length==1)
			sp.transform.position=tf.position;
	}
}

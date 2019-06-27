using System;
using UnityEngine;
public class Player : Entity {
	// Use this for initialization
	public float detect_radius=40;
	public enum  walk_dir{UP,DOWN,LEFT,RIGHT};
	public GameObject sp;
	public static GameObject bullet_prefab;
	Vector2 moving_dir;
	public int cur_weapon_id=0;
	public Weapon_Manager weapon_m;
	GameObject cur_weapon;
	void LoadPlayerData()
	{
		PlayerData.LoadPlayerData();
		tf.position=PlayerData.player_Pos;
		bullet_prefab=Resources.Load("Prefabs/Bullet") as GameObject;
	}
	void SetUpWeapon()
	{
		GameObject i_weapon=Instantiate(weapon_m.weapon_list[cur_weapon_id].skin,tf);
		cur_weapon=i_weapon;
		weapon_m.weapon_list[cur_weapon_id]._I_weapon=cur_weapon;
	}
	void Start () 
	{
		tf=GetComponent<Transform>();
		LoadPlayerData();
		SetUpWeapon();
	}
	void changeweapon()
	{
		if(Input.mouseScrollDelta.y!=0)
		{
			cur_weapon_id+=(int)(Input.mouseScrollDelta.y);
			cur_weapon_id=(cur_weapon_id%weapon_m.weapon_list.Count+weapon_m.weapon_list.Count)%weapon_m.weapon_list.Count;

			if(cur_weapon!=null)
				Destroy(cur_weapon);
			//Instantiate Weapon to Scence
			SetUpWeapon();
		}
	}
	// Update is called once per frame
	void Update ()
	{
		move(playerControler());
		shoot();
		detect();
		spirite_update();
		changeweapon();
	}
	void shoot()
	{
		if(Input.GetMouseButton(0))
		{
			weapon_m.request(cur_weapon_id).Act(tf.position,tf.rotation,null);
		}
	}
	public override void spirite_update()
	{
		Vector2 v2 = Camera.main.ScreenToViewportPoint( Input.mousePosition );
		v2.x-=0.5f;
		v2.y-=0.5f;
		float ang=(float)(Math.Acos(v2.normalized.x)*360/(Math.PI*2));
		if(v2.y<0)
			ang=-ang;
		ang=(ang+360)%360;
		tf.rotation=Quaternion.Euler(0,0,ang);
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
	}
	void detect()
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
	public override void interact(string behavior,int[] arg)
	{
	}
}

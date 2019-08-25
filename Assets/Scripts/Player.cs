using System;
using UnityEngine;
public class Player : Entity {
	#region walking_behavior

		public enum  walk_dir{UP,DOWN,LEFT,RIGHT};
		Vector2 moving_dir;
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
	
	#endregion
	#region self_sprite_update
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
	#endregion
	#region attack_behavior
		public static GameObject bullet_prefab;
		
		public int cur_weapon_id=0;
		public Weapon_Manager weapon_m;
		Weapon cur_weapon;
		void SwitchWeapon()
		{
			if(Input.mouseScrollDelta.y!=0)
			{
				int WP_Count=weapon_m.GetWeaponAmount();
				cur_weapon_id+=(int)(Input.mouseScrollDelta.y);
				cur_weapon_id=(cur_weapon_id+WP_Count)%WP_Count;

				//Instantiate Weapon to Scence
				cur_weapon=weapon_m.SetUpWeaponInstance(cur_weapon_id,this.gameObject);
			}
		}
		void shoot()
		{
			if(Input.GetMouseButton(0))
			{
				cur_weapon.Attack();
			}
		}
	#endregion
	#region collision_behavior
		void OnCollisionStay2D(Collision2D collisionInfo)
		{
			
			if(collisionInfo.gameObject.tag=="Npc")
			{
				sp.transform.position=collisionInfo.transform.position;
				Npc interact_npc=collisionInfo.gameObject.GetComponent<Npc>();

				if(interact_npc!=null&&Input.GetKeyDown(KeyCode.Space))
					Fungus.Flowchart.BroadcastFungusMessage("interact:"+interact_npc.NpcName);
			}
		}
		void OnTriggerStay2D(Collider2D collisionInfo)
		{
			if(collisionInfo.gameObject.tag=="item")
			{
				sp.transform.position=collisionInfo.transform.position;
				
				if(Input.GetKeyDown(KeyCode.Space))
				{
					if(collisionInfo.gameObject.GetComponent<Item>()!=null)
					{
						Debug.Log("picked up");
						GameObject NewItem=collisionInfo.gameObject;
						Destroy(collisionInfo.gameObject);
					}
					else
					{
						Debug.Log("can not picked up!!");
					}
				}
			}
		}
	#endregion
	
	public GameObject sp;
	
	void LoadPlayerData()
	{
		PlayerData.LoadPlayerData();
		tf.position=PlayerData.player_Pos;
		bullet_prefab=Resources.Load("Prefabs/Bullet") as GameObject;
	}
	void Start () 
	{
		tf=GetComponent<Transform>();
		LoadPlayerData();
		cur_weapon=weapon_m.SetUpWeaponInstance(cur_weapon_id,this.gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		move(playerControler());
		shoot();
		spirite_update();
		SwitchWeapon();
	}
	
}

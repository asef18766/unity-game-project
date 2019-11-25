using System;
using UnityEngine;
public class Player : Entity
{
	#region walking_behavior

	public enum WalkDir { UP, DOWN, LEFT, RIGHT }
	Vector2 movingDir;

	Player.WalkDir[] playerController()
	{
		Player.WalkDir[] ret = new Player.WalkDir[4];
		int element = 0;

		if(Input.GetKey(KeyCode.A))
		{
			ret[element] = Player.WalkDir.LEFT;
			element++;
		}
		if(Input.GetKey(KeyCode.W))
		{
			ret[element] = Player.WalkDir.UP;
			element++;
		}
		if(Input.GetKey(KeyCode.D))
		{
			ret[element] = Player.WalkDir.RIGHT;
			element++;
		}
		if(Input.GetKey(KeyCode.S))
		{
			ret[element] = Player.WalkDir.DOWN;
			element++;
		}
		if(element != 4)
			Array.Resize(ref ret, element);

		return ret;
	}

	void move(Player.WalkDir[] wDir)
	{
		float x = tf.position.x;
		float y = tf.position.y;

		for(int u = 0; u != wDir.Length; ++u)
		{
			if(wDir[u] == Player.WalkDir.LEFT)
				tf.position += (Vector3.left * (Time.deltaTime * tf.localScale.x));
			else if(wDir[u] == Player.WalkDir.UP)
				tf.position += (Vector3.up * (Time.deltaTime * tf.localScale.x));
			else if(wDir[u] == Player.WalkDir.RIGHT)
				tf.position += (Vector3.right * (Time.deltaTime * tf.localScale.x));
			else if(wDir[u] == Player.WalkDir.DOWN)
				tf.position += (Vector3.down * (Time.deltaTime * tf.localScale.x));
		}

		movingDir.x = tf.position.x - x;
		movingDir.y = tf.position.y - y;
	}

	#endregion
	#region self_sprite_update
	public override void spiriteUpdate()
	{
		Vector2 v2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		v2.x -= 0.5f;
		v2.y -= 0.5f;
		float ang = (float) (Math.Acos(v2.normalized.x) * 360 / (Math.PI * 2));
		if(v2.y < 0)
			ang = -ang;
		ang = (ang + 360) % 360;
		tf.rotation = Quaternion.Euler(0, 0, ang);
	}
	#endregion
	#region collision_behavior
	void OnCollisionStay2D(Collision2D collisionInfo)
	{
		if(collisionInfo.gameObject.tag.Equals("Npc"))
		{
			Npc interactNpc = collisionInfo.gameObject.GetComponent<Npc>();

			if(interactNpc != null && Input.GetKeyDown(KeyCode.Space))
			{
				Fungus.Flowchart.BroadcastFungusMessage("interact:" + interactNpc.NpcName);
				Debug.Log("interact:" + interactNpc.NpcName);
			}

		}
	}

	void OnTriggerStay2D(Collider2D collisionInfo)
	{
		if(collisionInfo.gameObject.tag.Equals("item"))
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				if(collisionInfo.gameObject.GetComponent<Item>() != null)
				{
					Debug.Log("picked up");
					GameObject NewItem = collisionInfo.gameObject;
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

	void LoadPlayerData()
	{
		PlayerData.LoadPlayerData();
		tf.position = PlayerData.playerPos;
		WeaponEventHandler.bulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
	}
	void Start()
	{
		tf = GetComponent<Transform>();
		LoadPlayerData();
	}

	// Update is called once per frame
	void Update()
	{
		move(playerController());
		spiriteUpdate();
	}
}
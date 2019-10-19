using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity
{
	public bool distroyOnhitEnemy = true;
	public float atk = 0;
	public float vec = 2.0f;
	public float destroyTime = 0.1f;
	[Tooltip("Whether the entity should be destroyed or not")]
	public bool destroyTrigger = false;
	[Tooltip("Object tags which can collide with this entity")]
	public static string[] collidableTags;
	Vector3 dir;

	Collider2D[] hitEnemy;
	Collider2D[] hitPlayer;
	Collider2D[] hitBuilding;

	void move()
	{
		dir = transform.position;
		transform.Translate(new Vector3(vec, 0, 0));
		dir = transform.position - dir;
	}

	public override void spiriteUpdate()
	{
		move();
	}

	// Use this for initialization
	void Start()
	{
		StartCoroutine(Ondestroy());
	}

	IEnumerator Ondestroy()
	{
		yield return new WaitForSecondsRealtime(this.destroyTime);
		this.destroyTrigger = true;
	}

	// Update is called once per frame
	void Update()
	{
		this.spiriteUpdate();
		if(this.OnEnter())
			this.interactOther();
		if(this.destroyTrigger)
		{
			DestroyImmediate(gameObject);
			DestroyImmediate(this);
		}
	}

	void interactOther()
	{
		//enemy process
		for(int u = 0; u != this.hitEnemy.Length; ++u)
		{
			this.hitEnemy[u].GetComponent<Enemy>().interact("HURT", new int[1]
			{
				(int) atk
			});
			this.destroyTrigger = this.distroyOnhitEnemy;
		}

		//building process
		if(this.hitBuilding.Length != 0)
		{
			this.destroyTrigger = true;
		}

	}

	bool OnEnter()
	{
		float d_r = GetComponent<SpriteRenderer>().bounds.size.magnitude;
		this.hitEnemy = Collide.RayGetCollideByTag(transform.position, d_r, dir, "enemy");
		this.hitPlayer = Collide.RayGetCollideByTag(transform.position, d_r, dir, "player");
		this.hitBuilding = Collide.RayGetCollideByTag(transform.position, d_r, dir, "building");

		//TODO: impove return method
		return this.hitEnemy.Length != 0 || this.hitBuilding.Length != 0 || this.hitPlayer.Length != 0;
	}

	public override void interact(string behavior, int[] arg)
	{
		if(behavior.Equals("DESTROY"))
			this.destroyTrigger = true;
	}
}
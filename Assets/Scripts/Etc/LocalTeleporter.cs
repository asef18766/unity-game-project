using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalTeleporter : MonoBehaviour
{

	Collider2D[] tar;
	public float detectRadius = 1;
	public float delayTime = 2;
	[Tooltip("Destination (LocalTeleporter)")]
	public LocalTeleporter des;
	public bool avaible = true;

	Vector3 desPos;
	Vector3 locPos;

	void Start()
	{
		this.desPos = this.des.transform.position;
		this.locPos = transform.position;
	}

	void Update()
	{
		if(this.detect() && this.avaible)
			StartCoroutine(this.teleport());
	}

	IEnumerator teleport()
	{
		this.des.avaible = false;
		
		// set all touched entity to destination
		for(int u = 0; u != tar.Length; ++u)
		{
			tar[u].transform.position = this.desPos;
		}

		yield return new WaitForSeconds(this.delayTime);
		this.des.avaible = true;
	}

	// return whether player touch this teleporter
	bool detect()
	{
		tar = Collide.AreaGetCollideByTag(this.locPos, this.detectRadius, Collide.Method.Circle, "Player");

		if(tar.Length != 0)
			return true;
		return false;
	}
}
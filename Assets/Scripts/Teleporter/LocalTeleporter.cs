using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalTeleporter : BaseTeleporter
{
	Collider2D[] tar;
	[Tooltip("Destination (LocalTeleporter)")]
	public LocalTeleporter des;

	void Start()
	{
	}

	void Update()
	{
		if(this.detect() && this.avaible)
			StartCoroutine(this.teleport());
	}

	new IEnumerator teleport()
	{
		this.des.avaible = false;

		// set all touched entity to destination
		for(int u = 0; u != tar.Length; ++u)
		{
			tar[u].transform.position = this.des.transform.position;
		}

		yield return new WaitForSeconds(this.delayTime);
		this.des.avaible = true;
	}

	// return whether player touch this teleporter
	new bool detect()
	{
		tar = Collide.AreaGetCollideByTag(this.transform.position, this.detectRadius, Collide.Method.Circle, "Player");

		if(tar.Length != 0)
			return true;
		return false;
	}
}
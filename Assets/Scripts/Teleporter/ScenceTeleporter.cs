using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceTeleporter : BaseTeleporter
{
	public string desScenceName;
	Vector3 locPos;
	public Vector3 desPos;

	IEnumerator init()
	{
		yield return new WaitForSeconds(delayTime);
		avaible = true;
	}

	void Start()
	{
		this.locPos = transform.position;
		StartCoroutine(this.init());
	}

	void Update()
	{
		if(this.detect() && this.avaible)
			this.teleport();
	}

	new void teleport()
	{
		PlayerData.playerPos = this.desPos;
		PlayerData.SavePlayerData();
		SceneManager.LoadScene(this.desScenceName);
	}

	new bool detect()
	{
		Collider2D[] tar = Collide.AreaGetCollideByTag(this.locPos, this.detectRadius, Collide.Method.Circle, "Player");

		if(tar.Length != 0)
			return true;
		return false;
	}
}
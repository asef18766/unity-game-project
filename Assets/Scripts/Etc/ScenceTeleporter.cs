using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceTeleporter : MonoBehaviour
{
	public string desScenceName;
	public float detectRadius = 1;
	public float delayTime = 2;
	public bool avaible = false;
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

	void teleport()
	{
		PlayerData.player_Pos = this.desPos;
		PlayerData.SavePlayerData();
		SceneManager.LoadScene(this.desScenceName);
	}

	bool detect()
	{
		Collider2D[] tar = Collide.AreaGetCollideByTag(this.locPos, this.detectRadius, Collide.Method.Circle, "Player");

		if(tar.Length != 0)
			return true;
		return false;
	}
}
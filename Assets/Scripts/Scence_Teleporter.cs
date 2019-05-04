using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scence_Teleporter : MonoBehaviour {
	public string Des_Scence_Name;
	public float detect_radius=1;
	public float delay_time=2;
	public bool avaible=false;
	Vector3 locpos;
	public Vector3 des_pos;
	// Use this for initialization
	IEnumerator init()
	{
		yield return new WaitForSeconds(delay_time);
		avaible=true;
	}
	void Start ()
	{
		locpos=GetComponent<Transform>().position;
		StartCoroutine(init());
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(detect()&&avaible)
			teleport();
			
	}
	void teleport()
	{
		PlayerData.player_Pos=des_pos;
		PlayerData.SavePlayerData();
		SceneManager.LoadScene(Des_Scence_Name);
	}
	bool detect()
	{
		Collider2D[] tar;
		tar=Collide.AreaGetCollideByTag(locpos,detect_radius,Collide.Method.Circle,"Player");
		if(tar.Length!=0)
			return true;

		return false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Local_Teleporter: MonoBehaviour {

	Collider2D[] tar;
	public float detect_radius=1;
	public float delay_time=2;
	public Local_Teleporter des;
	public bool avaible=true;

	Vector3 despos;
	Vector3 locpos;
	void Start () {
		despos=des.GetComponent<Transform>().position;
		locpos=GetComponent<Transform>().position;
	}
	
	// Update is called once per frame
	void Update () {
		if(detect()&&avaible)
			StartCoroutine(teleport());
	}
	IEnumerator teleport()
	{
		des.avaible=false;
		for(int u=0;u!=tar.Length;++u)
		{
			tar[u].transform.position=despos;
		}
		yield return new WaitForSeconds(delay_time);
		des.avaible=true;
	}
	bool detect()
	{
		tar=Collide.AreaGetCollideByTag(locpos,detect_radius,Collide.Method.Circle,"Player");
		if(tar.Length!=0)
			return true;

		return false;
	}
}
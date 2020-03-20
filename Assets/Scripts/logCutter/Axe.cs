using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector3(mousePosition.x , mousePosition.y , transform.position.z);
	}
	void OnTriggerEnter2D(Collider2D c)
	{
		if(Input.GetMouseButton(0))
		{
			print("pressed");
			if(c.tag == "log")
			{
				c.gameObject.GetComponent<Log>().Cut();
			}
		}
			
	}
}

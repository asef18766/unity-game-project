using System;
using UnityEngine;
public class Entity : MonoBehaviour {
	// Use this for initialization
	Transform tf;
	
	void Start () 
	{
		tf=GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	protected virtual void spirite_update()
	{

	}
	protected virtual void interact(string behavior)
	{
		
	}
}

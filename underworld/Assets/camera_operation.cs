﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_operation : MonoBehaviour {

	// Use this for initialization
	public GameObject tar;
	Transform tf;
	void Start () {
		tf=GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		camera_follow();
	}
	void camera_follow()
	{
		GetComponent<Transform>().position=new Vector3(tar.GetComponent<Transform>().position.x,
												   	   tar.GetComponent<Transform>().position.y,
														   GetComponent<Transform>().position.z);
	}
}

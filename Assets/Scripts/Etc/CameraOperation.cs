using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOperation : MonoBehaviour
{
	public GameObject tar;
	Transform tf;

	void Start()
	{
		tf = GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update()
	{
		this.cameraFollow();
	}
	
	void cameraFollow()
	{
		Vector3 tarPos = tar.transform.position;
		this.tf.position = new Vector3(tarPos.x, tarPos.y, this.tf.position.z);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    [SerializeField] float killTime;
	[SerializeField] Sprite destroyed;
	enum LogState
	{
		Normal,
		Destroyed
	}
	LogState logState = LogState.Normal;
    // Use this for initialization
    void Start()
    {
		Destroy(this.gameObject , killTime);
    }
    // Update is called once per frame
    void Update()
    {
		if(logState == LogState.Destroyed)
		{
			GetComponent<SpriteRenderer>().sprite = destroyed;
		}
    }
	public void Cut()
	{
		logState = LogState.Destroyed;
	}
}

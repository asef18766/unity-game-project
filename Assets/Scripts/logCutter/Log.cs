﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogCutter
{
	public class Log : IDestroyable
	{
		[SerializeField] Sprite destroyed;
		enum LogState
		{
			Normal,
			Destroyed
		}
		LogState logState = LogState.Normal;
		GameController gameController;
		// Use this for initialization
		void Start()
		{
			gameController = GameController.instance;
		}
		// Update is called once per frame
		void Update()
		{
			if(logState == LogState.Destroyed)
			{
				GetComponent<SpriteRenderer>().sprite = destroyed;
			}
		}
		public override void Cut()
		{
			logState = LogState.Destroyed;
			gameController.StartCoroutine("addScore");
		}
	}
}
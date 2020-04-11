using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGame.Fishing
{
	public class PowerUI : MonoBehaviour
	{
		public GamePlay gamePlay;
		[SerializeField]
		public Image progressBar;
		private Text text;

		// Use this for initialization
		void Start()
		{
			this.text = GetComponent<Text>();
		}

		// Update is called once per frame
		void Update()
		{
			this.progressBar.fillAmount = this.gamePlay.PowerRatio;
			this.text.text = String.Format("{0:F2}", this.gamePlay.power);
		}
	}

}
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
		private Image progressBar;
		[SerializeField]
		private Image targetArea;
		[SerializeField]
		private Text text;
		private float barLength;

		private void Start()
		{
			// calculate bar length
			Vector3[] vs = new Vector3[4];
			this.progressBar.rectTransform.GetWorldCorners(vs);
			this.barLength = vs[3].x - vs[0].x;
		}

		private void Update()
		{
			this.progressBar.fillAmount = this.gamePlay.PowerRatio;
			this.text.text = String.Format("{0:F2}", this.gamePlay.power);
			this.updateTarget();
		}

		private void updateTarget()
		{
			// hide
			if(!this.gamePlay.fish)
			{
				this.targetArea.rectTransform.SetRight(this.barLength);
			}
			else
			{
				// left
				float lRatio = this.gamePlay.fish.lowerBound / this.gamePlay.upperBound;
				float rRatio = this.gamePlay.fish.upperBound / this.gamePlay.upperBound;
				this.targetArea.rectTransform.SetLeft(lRatio * this.barLength);
				this.targetArea.rectTransform.SetRight((1 - rRatio) * this.barLength);
			}
		}
	}
}
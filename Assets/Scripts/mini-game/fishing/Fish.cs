using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.Fishing
{
	public class Fish : MonoBehaviour
	{
		public FishData fishData;
		public int upperBound { get { return this.fishData.upperBound; } }
		public int lowerBound { get { return this.fishData.lowerBound; } }
		public float duration { get { return this.fishData.duration; } }
		// current hp 
		public int hp;

		private void Start()
		{
			this.hp = this.fishData.hp;
		}
	}
}
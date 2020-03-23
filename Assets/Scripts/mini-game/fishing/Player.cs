using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.Fishing
{
	public class Player : MonoBehaviour
	{
		public KeyCode hit;
		private int fishCount;
		private int score;

		// get a new fish
		public void GetFish(Fish fish)
		{
			this.fishCount++;
			FishData fd = fish.fishData;
			this.score += fd.hp * (fd.upperBound - fd.lowerBound);
		}

		public bool Hit()
		{
			// Debug.Log("Hit!");
			return Input.GetKeyDown(this.hit);
		}
	}
}
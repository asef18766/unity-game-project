using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.Fishing
{
	[CreateAssetMenu(fileName = "new Fish", menuName = "Mini game/fishing/Fish")]
	public class FishData : ScriptableObject
	{
		public int upperBound;
		public int lowerBound;
		public float duration;
		// max hp
		public int hp;
		public Sprite sprite;
	}
}
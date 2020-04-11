using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.Fishing
{
	public class GamePlay : MonoBehaviour
	{
		public float gameDuraiton;
		// power bar upper bound
		public float upperBound;
		// the delta of power for each second
		public float powerDelta;
		public float power { get; private set; }
		public float PowerRatio
		{
			get
			{
				return this.power / this.upperBound;
			}
		}

		[SerializeField]
		private float highRST;
		[SerializeField]
		private float lowRST;
		public GameObject fishPrefab;
		public FishData[] fishs;
		// where the fish appear
		[SerializeField]
		private Transform lake;
		// player instance
		[SerializeField]
		private Player player;
		// current fish
		public Fish fish { get; private set; }
		// the fish appear or not
		private bool appear { get { return this.fish; } }
		// remain time before the fish dive into water (ratio)
		public float RemainTimeRatio
		{
			get
			{
				return this.fish ? Mathf.Clamp(this.remainTime / this.fish.duration, 0f, 1f) : 0;
			}
		}
		private float remainTime;

		private void Start()
		{
			StartCoroutine(this.gameCycle());
			StartCoroutine(this.waitForEnd());
		}

		// private void Update()
		// {
		// 	// update remain time
		// 	if(this.remainTime >= 0)
		// 	{
		// 		this.remainTime -= Time.deltaTime;
		// 		if(this.remainTime <= 0)
		// 		{
		// 			this.removeFish();
		// 		}
		// 	}
		// }

		private IEnumerator setInterval(float waitFor, System.Action callback)
		{
			yield return new WaitForSeconds(waitFor);
			callback();
		}

		// record the remain time when fish disappear
		private IEnumerator timingFish()
		{
			this.remainTime = this.fish.duration;
			while(this.remainTime > 0)
			{
				this.remainTime -= Time.deltaTime;
				yield return null;
			}
			this.remainTime = 0;
			yield return null;
		}

		// is that a fish appear?
		// if yes, update the related variable
		private void chooseFish()
		{
			float r = Random.Range(0, 1f);
			// a new fish coming, cheers!
			if(r < 0.2f)
			{
				GameObject go = Instantiate(this.fishPrefab, this.lake);
				FishData fd = this.fishs[Random.Range(0, this.fishs.Length - 1)];
				// update sprite
				go.GetComponent<SpriteRenderer>().sprite = fd.sprite;
				// update fish data
				Fish f = go.GetComponent<Fish>();
				f.fishData = fd;
				// set fish instance
				this.fish = f;
				StartCoroutine(this.setInterval(f.duration, this.removeFish));
				StartCoroutine(this.timingFish());
			}
		}

		private void removeFish()
		{
			if(!this.fish)
			{
				Debug.LogError("Call `removeFish` when fish haven't appeared!");
				return;
			}
			this.power = 0;
			Destroy(this.fish.gameObject);
			this.fish = null;
		}

		private IEnumerator waitForEnd()
		{
			yield return new WaitForSeconds(this.gameDuraiton);
			// game end
		}

		private IEnumerator gameCycle()
		{
			while(true)
			{
				if(this.appear)
				{
					// detect player input, update power bar
					if(this.player.Hit())
					{
						this.power += this.powerDelta;
					}
					this.power -= (this.powerDelta * Time.deltaTime / Mathf.Lerp(this.highRST, this.lowRST, this.power / this.upperBound));
					this.power = Mathf.Clamp(this.power, 0, this.upperBound);
					// should hit the fish?
					if(this.power >= this.fish.lowerBound && this.power <= this.fish.upperBound)
					{
						this.fish.hp--;
					}
					// is the fish dead?
					if(this.fish.hp <= 0)
					{
						// you got a fresh fish, bro
						this.player.GetFish(this.fish);
						// good bye
						this.removeFish();
						yield return new WaitForSeconds(1);
					}
					yield return true;
				}
				else
				{
					// fish, fish, where are you?
					this.chooseFish();
					// next round
					// if fish not appears, wait for 1 second
					// else no wait
					yield return new WaitForSeconds(this.fish ? 0 : 1);
				}
			}
		}
	}
}
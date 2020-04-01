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
		[SerializeField]
		private float highRST;
		[SerializeField]
		private float lowRST;
		public GameObject fishPrefab;
		public FishData[] fishs;
		public float power { get; private set; }
		// where the fish appear
		[SerializeField]
		private Transform lake;
		// player instance
		[SerializeField]
		private Player player;
		// current fish
		private Fish fish;
		// the fish appear or not
		private bool appear { get { return this.fish; } }
		// remain time before the fish dive into water (ratio)
		private float remainTime
		{
			get
			{
				return this.fish ? this.fish.duration : 0;
			}
		}

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

		// is that a fish appear?
		// if yes, update the related variable
		private Fish chooseFish()
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
				StartCoroutine(this.setInterval(f.duration, this.removeFish));
				return f;
			}
			return null;
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
					this.fish = this.chooseFish();
					// next round
					// if fish not appears, wait for 1 second
					// else no wait
					yield return new WaitForSeconds(this.fish ? 0 : 1);
				}
			}
		}
	}
}
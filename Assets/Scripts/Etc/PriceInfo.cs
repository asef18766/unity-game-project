using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

// use for store object name and price
[CreateAssetMenu()]
public class PriceInfo : ScriptableObject
{
	public PriceDict price;

	public void onBuy(string name, int count)
	{
		if(!this.price.ContainsKey(name))
		{
			Debug.Log("Error: the item [" + name + "] not found!");
			return;
		}

		int old = price[name];
		price[name] += (int) (count / old * Random.Range(0.8f, 1.2f));
	}

	public void onSell(string name, int count)
	{
		if(!this.price.ContainsKey(name))
		{
			Debug.Log("Error: the item [" + name + "] not found!");
			return;
		}

		int old = price[name];
		price[name] -= (int) (count / old * Random.Range(0.8f, 1.2f));
		price[name] = Mathf.Max(price[name], 5);
	}
}
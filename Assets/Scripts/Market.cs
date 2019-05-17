using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
	public PriceInfo info;

	public void buy(string name, int count)
	{
		info.onBuy(name, count);
	}

	public void sell(string name, int count)
	{
		info.onSell(name, count);
	}

	// randomly simulate the trading(?
	// call this when a round end, i gusee
	public void updateMarket()
	{
		List<string> productNames = new List<string>(info.price.Keys);
		foreach(string n in productNames)
		{
			if(Random.Range(0f, 1f) > 0.5)
				info.onBuy(n, Random.Range(1, 100));
			else
				info.onSell(n, Random.Range(1, 100));
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// use for store object name and price
[CreateAssetMenu()]
public class PriceInfo : ScriptableObject {
	public List<string> keys;
	public List<int> vals;

	public Dictionary<string, int> price { get; private set; }
	
	private void OnEnable()
	{
		this.price = new Dictionary<string, int>();
		for(int i=0, l=this.keys.Count ; i<l ; i++) this.price.Add(this.keys[i], this.vals[i]);
	}

	public void onBuy(string name, int count)
	{
		if(!this.price.ContainsKey(name))
		{
			Debug.Log("Error: the item [" + name + "] not found!");
			return;
		}

		int old = price[name];
		price[name] += (int)(count / old * Random.Range(0.8f, 1.2f));
	}

	public void onSell(string name, int count)
	{
		if(!this.price.ContainsKey(name))
		{
			Debug.Log("Error: the item [" + name + "] not found!");
			return;
		}

		int old = price[name];
		price[name] -= (int)(count / old * Random.Range(0.8f, 1.2f));
		price[name] = Mathf.Max(price[name], 5);
	}

	// call this to dump the dict value into list (for serialize)
	public void save()
	{
		this.keys.Clear();
		this.vals.Clear();

		foreach(var kv in this.price)
		{
			this.keys.Add(kv.Key);
			this.vals.Add(kv.Value);
		}
	}
}

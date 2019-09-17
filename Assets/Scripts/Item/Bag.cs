using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bag : MonoBehaviour
{
	private Dictionary<int, ItemCount> content;

	void Start()
	{
		content = new Dictionary<int, ItemCount>();
	}

	public List<ItemCount> getItemList()
	{
		return content.Values.ToList();
	}

	// insert n items into bag
	public bool updateItem(Item other, int n)
	{
		if(content.ContainsKey(other.getId()))
			return content[other.getId()].updateCount(n);
		else
		{
			ItemCount ic = ScriptableObject.CreateInstance<ItemCount>();
			// set item
			ic.updateCount(n);
			content.Add(ic.id, ic);

			return true;
		}
	}
}
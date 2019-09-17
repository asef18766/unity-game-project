using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCount : ScriptableObject
{
	public const int MAX_COUNT = 99;

	public int count { private set; get; }
	public int id { get { return item.getId(); } }
	private Item item;


	public bool same(ItemCount other)
	{
		return item.getId() == other.id;
	}

	public bool use()
	{
		if(item == null || count == 0)
		{
			return false;
		}

		item.use_item();
		count--;

		// TODO: item empty event

		return true;
	}

	public bool updateCount(int delta)
	{
		int newCount = count + delta;

		// invalid count
		if(newCount < 0 || newCount > MAX_COUNT) return false;

		// update
		count = newCount;
		return true;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemCount : ScriptableObject
{
	void OnEnable()
	{
		id_controller=ItemInstanceManager.Get_Id_Manager_Instance();
		id=id_controller.GetIdByItem(item);
	}
	public const int MAX_COUNT = 99;
	private static ItemInstanceManager id_controller;
	public int count { private set; get; }
	public int id;
	private Item item;

	#region overload_operators
	public static bool operator==(ItemCount x,ItemCount y)
	{
		return x.count == y.count && x.id==y.id;
	}
	
	public static bool operator!=(ItemCount x,ItemCount y)
	{
		return x.count != y.count || x.id!=y.id;
	}
	public static bool operator>(ItemCount x,ItemCount y)
	{
		return x.count > y.count && x.id==y.id;
	}
	public static bool operator<(ItemCount x,ItemCount y)
	{
		return x.count < y.count && x.id==y.id;
	}
	public static bool operator>=(ItemCount x,ItemCount y)
	{
		return x.count >= y.count && x.id==y.id;
	}
	public static bool operator<=(ItemCount x,ItemCount y)
	{
		return x.count <= y.count && x.id==y.id;
	}
	public override bool Equals(object other)
	{
		return ((other is ItemCount) && (this == ((ItemCount) other)));
	}
	public override int GetHashCode()
	{
		return count.GetHashCode()^id.GetHashCode()^item.GetHashCode();
	}

	#endregion
	
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

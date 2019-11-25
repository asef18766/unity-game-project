using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemCount
{
	public ItemCount(Item i)
	{
		id_controller=ItemInstanceManager.Get_Id_Manager_Instance();
		id=id_controller.GetIdByItem(i);
		count=0;
		item=i;
	}
	public ItemCount(int i)
	{
		id_controller=ItemInstanceManager.Get_Id_Manager_Instance();
		id=i;
		count=0;
		item=id_controller.GetItemById(id);
	}
	public ItemCount(int m_id,int m_count)
	{
		id_controller=ItemInstanceManager.Get_Id_Manager_Instance();
		id=m_id;
		count=m_count;
		item=id_controller.GetItemById(id);
	}
	public const int MAX_COUNT = 99;
	private static ItemInstanceManager id_controller=ItemInstanceManager.Get_Id_Manager_Instance();
	[SerializeField]public int count;
	[SerializeField]public int id;
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
	public void init()
	{
		item=id_controller.GetItemById(id);
	}
	bool _SetItemInstance()
	{
		if(item==null)
			item=id_controller.GetItemById(id);
		
		return item!=null;
	}
	public bool use(ItemEvent e)
	{
		if(item==null && _SetItemInstance()==false)
			return false;
		if(item == null || count == 0)
		{
			return false;
		}

		item.useItem(e);
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bag : MonoBehaviour
{
	private Dictionary<int, ItemCount> content;
	private static ItemInstanceManager id_controller;

	void Start()
	{
		content = new Dictionary<int, ItemCount>();
		id_controller=ItemInstanceManager.Get_Id_Manager_Instance();
	}

	public List<ItemCount> getItemList()
	{
		return content.Values.ToList();
	}
	public bool checkItem(ItemCount req)
	{
		if(!content.ContainsKey(req.id))
			return false;
		if(content[req.id].count<=req.count)
			return false;
		
		return true;
	}
	public bool removeItem(ItemCount req)
	{
		if(!content.ContainsKey(req.id))
			return false;
		if(!content[req.id].updateCount(content[req.id].count-req.count))
			return false;
		if(content[req.id].count==0)
			content.Remove(req.id);
		
		return true;
	}
	public bool updateItem(ItemCount req)
	{
		if(content.ContainsKey(req.id))
			return content[req.id].updateCount(req.count);
		else
		{
			ItemCount ic = ScriptableObject.CreateInstance<ItemCount>();
			// set item
			ic.updateCount(req.count);
			content.Add(ic.id, ic);

			return true;
		}
	}
	// insert n items into bag
	public bool updateItem(Item other, int n)
	{
		int item_id=id_controller.GetIdByItem(other);
		if(content.ContainsKey(item_id))
			return content[item_id].updateCount(n);
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
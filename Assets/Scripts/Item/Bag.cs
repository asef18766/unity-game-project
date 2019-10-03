using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName= "Items/Create Bag")]
public class Bag : ScriptableObject
{
	public static Bag bag_ins;
	private static ItemInstanceManager id_controller;

	[System.Serializable]public class Num_Item_Dictionary : SerializableDictionary<Item, int>{}

	[SerializeField]
	public Num_Item_Dictionary content;
	private Dictionary<int, ItemCount> _content;

	void OnEnable()
	{
		bag_ins=this;
		_content = new Dictionary<int, ItemCount>();
		id_controller=ItemInstanceManager.Get_Id_Manager_Instance();

	}
	void _AddEditToPrivateField()
	{
		foreach(var i in content)
		{
			
		}
	}
	public List<ItemCount> getItemList()
	{
		return _content.Values.ToList();
	}
	public bool checkItem(ItemCount req)
	{
		if(!_content.ContainsKey(req.id))
			return false;
		if(_content[req.id].count<=req.count)
			return false;
		
		return true;
	}
	public bool removeItem(ItemCount req)
	{
		if(!_content.ContainsKey(req.id))
			return false;
		if(!_content[req.id].updateCount(_content[req.id].count-req.count))
			return false;
		if(_content[req.id].count==0)
			_content.Remove(req.id);
		
		return true;
	}
	public bool updateItem(ItemCount req)
	{
		if(_content.ContainsKey(req.id))
			return _content[req.id].updateCount(req.count);
		else
		{
			ItemCount ic = ScriptableObject.CreateInstance<ItemCount>();
			// set item
			ic.updateCount(req.count);
			_content.Add(ic.id, ic);

			return true;
		}
	}
	// insert n items into bag
	public bool updateItem(Item other, int n)
	{
		int item_id=id_controller.GetIdByItem(other);
		if(_content.ContainsKey(item_id))
			return _content[item_id].updateCount(n);
		else
		{
			ItemCount ic = ScriptableObject.CreateInstance<ItemCount>();
			// set item
			ic.updateCount(n);
			_content.Add(ic.id, ic);

			return true;
		}
	}
}
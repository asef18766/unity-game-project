using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName = "Items/Create Bag")]
public class Bag : ScriptableObject
{
	[SerializeField] Bag _bag_ins;
	public static Bag bag_ins;
	private static ItemInstanceManager id_controller;
	const int MAX_ITEM_AMOUNT = 64;

	public List<ItemCount> content;

	void OnEnable()
	{
		bag_ins=_bag_ins;
		id_controller=ItemInstanceManager.Get_Id_Manager_Instance();
		foreach(var i in content)
			i.init();
		if(Bag.bag_ins==null)
		{
			Debug.Log("can not find bag ins");
		}
	}
	public List<ItemCount> getItemList()
	{
		return content;
	}
	public int checkItem(int _id)
	{
		return content.FindIndex(x => x.id == _id);
	}
	public int checkItem(ItemCount req)
	{
		return content.FindIndex(x => x.id == req.id && x.count <= req.count);
	}
	public bool removeItem(ItemCount req)
	{
		int index = checkItem(req);

		if(index == -1)
			return false;

		return content[index].updateCount(content[index].count - req.count);
	}
	public bool updateItem(ItemCount req)
	{
		int index = checkItem(req.id);
		if(index != -1)
			return content[index].updateCount(req.count);

		content.Add(req);
		return true;
	}
	// modify n items into bag
	public bool updateItem(Item other, int n)
	{
		int item_id = id_controller.GetIdByItem(other);
		int index = checkItem(item_id);
		if(index != -1)
			return content[index].updateCount(n);
		else
		{
			content.Add(new ItemCount(item_id, n));
			return true;
		}
	}
	public bool addItem(ItemCount it)
	{
		int index = checkItem(it.id);
		if(index != -1)
		{
			do
			{
				bool NotOverStack = content[index].updateCount(content[index].count + it.count);
				if(NotOverStack)
					return true;
				index = content.FindIndex(index, x => x.id == it.id);
			} while(index != -1);
		}

		if(content.Count == MAX_ITEM_AMOUNT)
			return false;

		content.Add(it);
		return true;
	}
	public Sprite GetItemSprite(int index)
	{
		if(content.Count <= index && index < 0)
			return null;
		return id_controller.GetItemById(content[index].id).GetItemSprite();

	}
	public string GetItemName(int index)
	{
		if(content.Count <= index && index < 0)
			return null;
		return id_controller.GetItemById(content[index].id).GetItemName();
	}
	public int GetItemCount(int index)
	{
		if(content.Count <= index && index < 0)
			return -1;
		return content[index].count;
	}
	public string GetItemDescription(int index)
	{
		if(bag_ins.content.Count <= index && index < 0)
			return null;
		return id_controller.GetItemById(content[index].id).GetHelpText();
	}
	public int GetBagSize()
	{
		return content.Count;
	}
	public bool CheckItemEvent(int index, ItemEvent e)
	{
		return id_controller.GetItemById(bag_ins.content[index].id).isUsable();
	}
	public bool ExecuteItemEvent(int index, ItemEvent e)
	{
		return bag_ins.content[index].use(e);
	}
}
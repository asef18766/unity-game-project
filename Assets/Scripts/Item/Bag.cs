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
		bag_ins = _bag_ins;
		id_controller = ItemInstanceManager.Get_Id_Manager_Instance();
		foreach(var i in content)
			i.init();
		if(Bag.bag_ins == null)
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

	private bool checkValid()
	{
		return false;
	}

	public bool updateItemByDelta(ItemCount req)
	{
		int index = checkItem(req.id);
		if(index != -1)
			return content[index].updateCountByDelta(req.count);

		content.Add(req);
		return true;
	}

	// set item to desired count
	public bool updateItem(ItemCount req)
	{
		int index = checkItem(req.id);
		if(index != -1)
			return content[index].updateCount(req.count);

		content.Add(req);
		return true;
	}

	// transfer item to other bag by delta
	public bool transferItem(Bag other, ItemCount ic)
	{
		return other.updateItemByDelta(ic);
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
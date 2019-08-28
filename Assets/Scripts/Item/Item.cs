using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	[SerializeField] int id=-1;
	[SerializeField][TextArea] string help_text;
	[SerializeField] I_ItemUsage usage=null;
	public Item(Item i)
	{
		int id=i.getId();
		help_text=i.get_help_text();
		usage=i.usage;

	}
	void Start()
	{
		if(id==-1)
			Debug.Log("item id does not assigned");
	}
	public bool isUsable(){ return usage!=null; }
	public int getId(){ return id; }
	public string get_help_text(){ return help_text; }
	public void use_item()
	{
		usage.use();
	}

	public static GameObject InstatiateItem(GameObject item)
	{
		GameObject n_item=Instantiate(item);
		foreach(Behaviour behaviour in n_item.GetComponentsInChildren<Behaviour>())
    		behaviour.enabled = true;
		return n_item;
	}
}

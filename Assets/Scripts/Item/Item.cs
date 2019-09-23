using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName= "Items/Create Item")]
public class Item : ScriptableObject {
	#region  overload_operators
	public static bool operator==(Item a,Item b)
	{
		return a.help_text==b.help_text && a.usage==b.usage && a.item_texture==b.item_texture;
	}
	public static bool operator!=(Item a,Item b)
	{
		return a.help_text!=b.help_text || a.usage!=b.usage || a.item_texture!=b.item_texture;
	}
	public override bool Equals(object other)
	{
		return ((other is Item) && (this == ((Item) other)));
	}
	public override int GetHashCode()
	{
		return help_text.GetHashCode()^usage.GetHashCode()^item_texture.GetHashCode();
	}
	#endregion
	[SerializeField][TextArea] string help_text;
	[SerializeField] I_ItemUsage usage=null;
	[SerializeField] GameObject item_texture;
	public Item(Item i)
	{
		help_text=i.get_help_text();
		usage=i.usage;
		item_texture=i.item_texture;
	}
	public bool isUsable(){ return usage!=null; }
	public string get_help_text(){ return help_text; }
	public void use_item()
	{
		usage.use();
	}

	public GameObject InstatiateItem(Transform pos)
	{
		GameObject n_item=Instantiate(item_texture,pos);
		foreach(Behaviour behaviour in n_item.GetComponentsInChildren<Behaviour>())
    		behaviour.enabled = true;
		return n_item;
	}
}

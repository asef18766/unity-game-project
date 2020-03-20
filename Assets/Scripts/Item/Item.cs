using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;  
using UnityEngine;
public class Item : MonoBehaviour {
	#region  overload_operators
	public override bool Equals(object other)
	{
		return ((other is Item) && (this == ((Item) other)));
	}
	/*
	public override int GetHashCode()
	{
		using (MD5 md5hash = MD5.Create())  
        {
			byte[] bytes = md5hash.ComputeHash(Encoding.UTF8.GetBytes("HELP_TEXT:"+help_text+
																	  ";USAGE:"+((usage==null)? "NULL":usage.GetHashCode().ToString())+
																	  ";ITEM_TEXTURE:"+item_texture.GetHashCode().ToString()
																	  ));
			StringBuilder builder = new StringBuilder();  
                for (int i = 0; i < bytes.Length; i++)  
                {  
                    builder.Append(bytes[i].ToString("x2"));  
                }  
                return builder.GetHashCode(); 
		}
	}*/
	#endregion
	public static GameObject pickableItemInstance;
	List<I_ItemUsage> usages=new List<I_ItemUsage>();
	[SerializeField] string item_name;

	[SerializeField][TextArea] string help_text;
	[SerializeField] Sprite item_texture;
	public string GetItemName()
	{
		return item_name;
	}
	public string GetHelpText()
	{
		return help_text;
	}
	public Sprite GetItemSprite()
	{
		return item_texture;
	}
	public Item(Item i)
	{
		help_text=i.help_text;
		usages=i.usages;
		item_texture=i.item_texture;
	}
	public bool isUsable(){ return usages.Count!=0; }

	public void useItem(ItemEvent e)
	{
		foreach(I_ItemUsage usage in usages.FindAll( (u)=>{return u.itemEvent==e;} ))
			usage.use();
	}

	public GameObject InstatiateItem(Transform pos)
	{
		GameObject n_item=Instantiate(pickableItemInstance,pos);
		n_item.GetComponent<SpriteRenderer>().sprite=item_texture;
		foreach(Behaviour behaviour in n_item.GetComponentsInChildren<Behaviour>())
    		behaviour.enabled = true;
		return n_item;
	}
	void Start()
	{
		item_texture=GetComponent<SpriteRenderer>().sprite;
	}
}

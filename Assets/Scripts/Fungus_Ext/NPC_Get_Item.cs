using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class NPC_Get_Item : Command {
	public Bag p_inv;
	public List<ItemCount> tar_item;
	Flowchart flag;
	public override void OnEnter()
	{
		//WIP
		/* 
		foreach(var it in tar_item)
		{
			if(!p_inv.checkItem(it))
				flag.GetBooleanVariable("123");
		}*/
		
	}

}

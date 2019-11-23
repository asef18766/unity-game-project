using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDropItemEvent : I_ItemUsage
{
	void Reset()
	{
		itemEvent=ItemEvent.Drop;
	}
	override public bool use()
	{
		return true;
	}
}

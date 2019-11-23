using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAddMoneyItemEvent : I_ItemUsage
{
	[SerializeField] int AddMoney=0;
	void Reset()
	{
		itemEvent=ItemEvent.Use;
	}
	override public bool use()
	{
		PlayerData.money+=AddMoney;
		return true;
	}
}

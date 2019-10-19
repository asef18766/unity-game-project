using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameInfo
{
	public int times{get;set;}
	public int highest_record{get;set;}
}
public class PlayerData
{
	public static Vector3 player_Pos = new Vector3(-0.426f, 0.435f, 0.0f);
	[SerializeField]public static int money;
	[SerializeField]public static int reputation;
	[SerializeField]public static List<GameInfo> record=new List<GameInfo>();
	public static void SavePlayerData()
	{
		PlayerPrefs.SetInt("player_money",money);
		PlayerPrefs.SetInt("player_repu",reputation);
		PlayerPrefs.SetFloat("player_x", player_Pos.x);
		PlayerPrefs.SetFloat("player_y", player_Pos.y);
		PlayerPrefs.SetFloat("player_z", player_Pos.z);
		for(int i=0;i!=record.Count;++i)
		{
			PlayerPrefs.SetInt("highest_game_record"+"["+i.ToString()+"]",record[i].highest_record);
			PlayerPrefs.SetInt("game_play_time"+"["+i.ToString()+"]",record[i].times);
		}
	}
	public static void LoadPlayerData()
	{
		money=PlayerPrefs.GetInt("player_money",0);
		reputation=PlayerPrefs.GetInt("player_repu",0);
		player_Pos.x = PlayerPrefs.GetFloat("player_x", 0.0f);
		player_Pos.y = PlayerPrefs.GetFloat("player_y", 0.0f);
		player_Pos.z = PlayerPrefs.GetFloat("player_z", 0.0f);
		for(int i=0;i!=record.Count;++i)
		{
			record[i].highest_record = PlayerPrefs.GetInt("highest_game_record"+"["+i.ToString()+"]",0);
			record[i].times = PlayerPrefs.GetInt("game_play_time"+"["+i.ToString()+"]",record[i].times);

		}
	}
}
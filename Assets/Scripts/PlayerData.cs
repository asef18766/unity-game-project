using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
	public static Vector3 player_Pos=new Vector3(-0.426f,0.435f,0.0f);
	public static void SavePlayerData()
	{
		PlayerPrefs.SetFloat("player_x",player_Pos.x);
		PlayerPrefs.SetFloat("player_y",player_Pos.y);
		PlayerPrefs.SetFloat("player_z",player_Pos.z);
	}
	public static void LoadPlayerData()
	{
		player_Pos.x=PlayerPrefs.GetFloat("player_x",0.0f);
		player_Pos.y=PlayerPrefs.GetFloat("player_y",0.0f);
		player_Pos.z=PlayerPrefs.GetFloat("player_z",0.0f);
	}
}

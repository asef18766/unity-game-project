using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
[CreateAssetMenu(menuName= "Mission/Create Mission")]
[System.Serializable]
public class Mission : ScriptableObject {
	public enum MissionState
	{
		Locked,
		UnLocked,
		OnGoing,
		Passed
	}
	public MissionState cur_state=MissionState.Locked;
	[SerializeField] List<Mission> pre_request;
	[SerializeField] List<I_Requirement> requirements;
	[SerializeField] List<I_Reward> reward_action;
	
	[SerializeField] Dictionary<MissionState,List<KeyValuePair<Flowchart,string> > > rediraction;
	
	public bool check_pre_request()
	{
		if(cur_state==MissionState.UnLocked)
			return true;
		
		foreach(var i in pre_request)
			if(i.cur_state!=MissionState.Passed)
				return false;
		cur_state=MissionState.UnLocked;
		return true;
	}
	public void accept_mission()
	{
		if(cur_state!=MissionState.UnLocked)
		{
			Debug.Log("error use of accept mission");
			return;
		}
		cur_state=MissionState.OnGoing;
	}
	public void complete_mission()
	{
		if(cur_state!=MissionState.OnGoing)
		{
			Debug.Log("error use of complete mission");
			return;
		}
		cur_state=MissionState.Passed;

		foreach(var i in reward_action)
			i.send_reward();
	}
}

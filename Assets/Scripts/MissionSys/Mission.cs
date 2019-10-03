using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
[System.Serializable]
public class MissionDes{
	[SerializeField]public Mission.MissionState state;
	[SerializeField]public Flowchart fc;
	[SerializeField]public string block_name;
}

[System.Serializable]
public class Mission : MonoBehaviour {
	
	public enum MissionState
	{
		Locked,
		UnLocked,
		OnGoing,
		Passed
	}
	[SerializeField] public string mission_name="";
	[SerializeField] public MissionState cur_state=MissionState.Locked;
	[SerializeField] List<Mission> pre_request;
	[SerializeField] List<I_Requirement> requirements;
	[SerializeField] List<I_Reward> reward_action;
	
	[SerializeField] List<MissionDes> rediraction;

	[SerializeField] public Dictionary<Mission.MissionState,Block> _mapping_state=new Dictionary<MissionState, Block>();
	
	void Start()
	{
		foreach(var i in rediraction)
		{
			if(i.fc.FindBlock(i.block_name)==null)
			{
				Debug.Log("null block finded");
			}
			_mapping_state.Add(i.state,i.fc.FindBlock(i.block_name));
		}
			
	}
	public bool check_pre_request()
	{
		if(cur_state==MissionState.Passed)
			return true;
		
		if(cur_state==MissionState.Locked)
		{
			foreach(var i in pre_request)
				if(i.cur_state!=MissionState.Passed)
					return false;
			cur_state=MissionState.UnLocked;
			return true;
		}
		
		return false;
		
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
	public bool complete_mission()
	{
		if(cur_state!=MissionState.OnGoing)
		{
			Debug.Log("error use of complete mission");
			return false;
		}
		foreach(var i in requirements)
			if(!i.check_require())
				return false;
		
		cur_state=MissionState.Passed;

		foreach(var i in reward_action)
			i.send_reward();
		return true;
	}
}

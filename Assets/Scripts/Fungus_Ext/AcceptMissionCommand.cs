using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Fungus;

namespace Fungus
{
    [CommandInfo("Flow",
             "Accept Mission",
             "Accept Specific Mission")]
    public class AcceptMissionCommand : Command
    {
        [SerializeField] Mission target_mission;
        public override void OnEnter()
        {
            if(target_mission.cur_state!=Mission.MissionState.UnLocked)
            {
                Debug.Log("mission accepting calling with wrong condiction");
                return;
            }
            target_mission.accept_mission();

            if(target_mission.cur_state!=Mission.MissionState.OnGoing)
            {
                Debug.Log("failing mission accepting");
            }
            Continue();
        }
    }
}

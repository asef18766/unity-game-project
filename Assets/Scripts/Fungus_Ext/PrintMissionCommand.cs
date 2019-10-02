using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Fungus;

namespace Fungus
{
	[CommandInfo("Flow",
             "Print Mission",
             "List all avalible missing by menu command")]
	public class PrintMissionCommand :  Command
	{
		[Tooltip("Text to display on the menu button")]
        [SerializeField] protected string text = "Option Text";

		[Tooltip("Notes about the option text for other authors, localization, etc.")]
        [SerializeField] protected string description = "";

		[Tooltip("Notes about the option text for other authors, localization, etc.")]
        [SerializeField] protected List<Mission> dest=new List<Mission>();

		private static List<Menu> _pre_command=new List<Menu>();
		private static List<Block> _controlled_block=new List<Block>();

		// Use this for initialization
		void _create_menu_command(string menu_dialog,Block targetBlock)
		{
			GameObject g=new GameObject();
			g.transform.parent=GetFlowchart().gameObject.transform;
			
			Menu m=g.AddComponent<Menu>();

			if(m==null)
			{
				Debug.Log("null menu");
				return;
			}
			m.SetStandardText(menu_dialog);
			m.targetBlock=targetBlock;
			
			if(m.targetBlock==null)
			{
				Debug.Log("null Block");
				return;
			}
			_controlled_block.Add(ParentBlock);
			_pre_command.Add(m);
			int cur_command=ParentBlock.CommandList.FindIndex(item=> item==ParentBlock.ActiveCommand);
			ParentBlock.CommandList.Insert(cur_command,m);
			ParentBlock.CommandList[ParentBlock.CommandList.Count-1].enabled=true;
		}
		public static void remove_precommand()
		{
			foreach(var i in _pre_command)
			{
				foreach(var b in _controlled_block)
					if(b.CommandList.Remove(i))
						break;
				DestroyImmediate(i.gameObject);
			}
			_controlled_block.Clear();
			_pre_command.Clear();
				
		}
		public override void OnEnter()
		{
			foreach(var i in dest)
			{
				i.check_pre_request();
				if(i.cur_state==Mission.MissionState.Locked)
					continue;
				switch(i.cur_state)
				{
					case Mission.MissionState.UnLocked:
					if(i._mapping_state.ContainsKey(i.cur_state))
						_create_menu_command("<new>"+i.mission_name,i._mapping_state[i.cur_state]);
					else
						Debug.Log("do not contain key");
					break;

					case Mission.MissionState.OnGoing:
					_create_menu_command("<On Going>"+i.mission_name,i._mapping_state[i.cur_state]);
					break;

					default:
					break;
				}
			}
			Continue();
		}
	}
}


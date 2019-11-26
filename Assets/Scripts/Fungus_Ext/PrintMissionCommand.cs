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
		[SerializeField] protected MenuDialog setMenuDialog;
		public MenuDialog SetMenuDialog  { get { return setMenuDialog; } set { setMenuDialog = value; } }

		// Use this for initialization
		void _create_menu_command(string menu_dialog,Block targetBlock)
		{
			var menuDialog = MenuDialog.GetMenuDialog();
			if (menuDialog != null)
			{
				menuDialog.SetActive(true);

				menuDialog.AddOption(menu_dialog, true, false, targetBlock);
			}
		}
		public override void OnEnter()
		{
			foreach(var i in dest)
			{
				i.check_pre_request();
				
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


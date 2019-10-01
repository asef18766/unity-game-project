using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Fungus;

namespace Fungus
{
    [CommandInfo("Flow",
             "Clear Menu Command",
             "Clear \"Menu\" Command Produce By \"Print Mission\" Command")]
	public class ClearMenuCommand : Command
	{
		public override void OnEnter()
		{
			PrintMissionCommand.remove_precommand();
			Continue();
		}
	}
}
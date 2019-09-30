using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Fungus;

namespace Fungus
{
	[CommandInfo("Flow",
             "Test Commannd",
             "Just a test")]
	public class TestCommand :  Command
	{
		[Tooltip("Text to display on the menu button")]
        [SerializeField] protected string text = "Option Text";

		[Tooltip("Notes about the option text for other authors, localization, etc.")]
        [SerializeField] protected string description = "";

		[Tooltip("Notes about the option text for other authors, localization, etc.")]
        [SerializeField] protected List<Block> dest;

		// Use this for initialization
		public override void OnEnter()
		{
			GameObject g=new GameObject();
			g.transform.parent=GetFlowchart().gameObject.transform;
			
			Menu m=g.AddComponent<Menu>();

			if(m==null)
			{
				Debug.Log("null menu");
			}
			
			
			ParentBlock.CommandList.Add(m);
			ParentBlock.CommandList[ParentBlock.CommandList.Count-1].enabled=true;
			Debug.Log("exit");
			Continue();
		}
	}
}


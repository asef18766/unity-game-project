using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PriceInfo))]
public class PriceInfoEditor : Editor
{
	private class Item
	{
		public string key;
		public int val;

		public Item(string k, int v)
		{
			key = k;
			val = v;
		}
	}

	private PriceInfo info;
	private string newKey;
	private int newVal;

	void OnEnable()
	{
		info = target as PriceInfo;	
		this.newKey = "";
		this.newVal = 0;
	}

	private void modifyDict(List<string> ks, string k, List<int> vs, int v)
	{
		string bk = k; // backup for old key
		EditorGUILayout.BeginHorizontal();
		EditorGUI.BeginChangeCheck();
		k = EditorGUILayout.TextField(k, GUILayout.Width(80));
		// key was modified
		if(EditorGUI.EndChangeCheck())
		{
			v = EditorGUILayout.IntField(v, GUILayout.Width(80));
			if(ks.Contains(k))
			{
				k += "(duplicate key)";
			}
			else
			{
				int i = ks.IndexOf(bk);
				ks[i] = k;
				vs[i] = v;
			}
		}
		else
		{
			v = EditorGUILayout.IntField(v, GUILayout.Width(80));
			int i = ks.IndexOf(bk);
			ks[i] = k;
			vs[i] = v;
		}
		// delete product
		if(GUILayout.Button("-", GUILayout.Width(40)))
		{
			int i = ks.IndexOf(k);
			ks.RemoveAt(i);
			vs.RemoveAt(i);
		}
		EditorGUILayout.EndHorizontal();
	}

	public override void OnInspectorGUI()
	{
		// dump kv-pair to list
		List<Item> items = new List<Item>();
		for(int i=0, l=info.keys.Count ; i<l ; i++)
		{
			Item kv = new Item(info.keys[i], info.vals[i]);
			items.Add(kv);
		}

		// draw on inspector
		int len = items.Count;
		for(int i=0 ; i<len ; i++)
		{
			this.modifyDict(info.keys, items[i].key, info.vals, items[i].val);
		}

		GUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Add New Item");
		GUILayout.EndHorizontal();
		this.newKey = EditorGUILayout.TextField(this.newKey);
		this.newVal = EditorGUILayout.IntField(this.newVal);
		if(GUILayout.Button("+"))
		{
			if(info.keys.Contains(newKey))
			{
				GUI.color = Color.red;
				EditorGUILayout.LabelField("dunplicate key!");
				GUI.color = Color.black;
			}
			else
			{
				info.keys.Add(newKey);
				info.vals.Add(newVal);
			}
		}
	}
}
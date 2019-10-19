
using UnityEditorInternal;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
[CustomEditor(typeof(Mission),true)]
public class MissionEditor : Editor
{
    public void OnEnable(){
    }
    public override void OnInspectorGUI () {
		serializedObject.Update();
		EditorGUILayout.PropertyField(serializedObject.FindProperty("mission_name"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("cur_state"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("pre_request"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("requirements"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("reward_action"),true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rediraction"),true);
		serializedObject.ApplyModifiedProperties();
	}
}

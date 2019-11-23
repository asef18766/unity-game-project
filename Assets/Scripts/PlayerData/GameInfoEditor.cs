using UnityEditorInternal;
using UnityEngine;
using UnityEditor;
[CustomPropertyDrawer(typeof(GameInfo))]
public class GameInfoEditor : PropertyDrawer
{
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
		EditorGUI.PrefixLabel(position, label);
		EditorGUI.PropertyField(position, property.FindPropertyRelative("times"));
	}
}
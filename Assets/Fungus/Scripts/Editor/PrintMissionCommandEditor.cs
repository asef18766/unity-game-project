using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using Rotorz.ReorderableList;

namespace Fungus.EditorUtils
{
    [CustomEditor(typeof(PrintMissionCommand))]
    public class PrintMissionCommandEditor : CommandEditor
    {
        protected SerializedProperty text;
        protected SerializedProperty description;
        protected SerializedProperty dest_list;

        protected virtual void OnEnable()
        {
            if (NullTargetCheck()) // Check for an orphaned editor instance
                return;

            text = serializedObject.FindProperty("text");
            description = serializedObject.FindProperty("description");
            dest_list = serializedObject.FindProperty("dest");
        }
        public override void DrawCommandGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(text);
            EditorGUILayout.PropertyField(description);

            ReorderableListGUI.Title(new GUIContent("Target Objects", "Objects containing collider components (2D or 3D)"));
            ReorderableListGUI.ListField(dest_list);

            serializedObject.ApplyModifiedProperties();
        }
        
    }
}
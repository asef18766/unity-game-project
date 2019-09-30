
using UnityEditorInternal;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
[CustomEditor(typeof(Mission),true)]
public class MissionEditor : Editor
{
    Mission mission;
    void OnEnable(){
        mission=(Mission)target;
        
    }
}

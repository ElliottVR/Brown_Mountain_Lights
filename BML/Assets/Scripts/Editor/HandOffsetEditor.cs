using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(HandOffset))]
public class HandOffsetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        HandOffset item = (HandOffset)target;
        if (GUILayout.Button("Set Transforms"))
        {
            item.SetTransforms();
        }
    }
}
#endif
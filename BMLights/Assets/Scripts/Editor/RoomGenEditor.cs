using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(RoomGen))]
public class RoomGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        RoomGen room = (RoomGen)target;
        if (GUILayout.Button("Generate Room"))
        {
            foreach(GameObject obj in room.itemsInArea)
            {
                Destroy(obj);
            }
            room.itemsInArea.Clear();
            room.GenerateRoom();
        }
    }
}
#endif

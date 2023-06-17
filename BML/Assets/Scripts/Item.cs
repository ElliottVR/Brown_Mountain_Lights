using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string Name = "New Item";
    
    public enum Subtype
    {
        Nonperishable,
        Spoilable
    };
    public Subtype editorPopup;

    public enum Edible
    {
        Nonconsumable,
        Consumable
    };
    public Edible editorType;

#if UNITY_EDITOR
    public void OnInspectorGUI()
    {
        editorPopup = (Subtype)EditorGUILayout.EnumPopup("Display", editorPopup);
        editorType = (Edible)EditorGUILayout.EnumPopup("Display", editorType);
    }
#endif

    public float nutritionalValue;


    public int hours;
    public int minutes;
    public TimeSpan timeUntilSpoiled;
    

    public GameObject itemPrefab;
    public int randChance;
    //public GameObject slotPrefab;
    
}

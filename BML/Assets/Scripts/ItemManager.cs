using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Classify : Item
{
    public List<Item> items;
    
    public void newItem(Item tempItem)
    {
        switch (editorPopup)
        {
            case Subtype.Nonperishable:
                items.Add(tempItem);
                break;
            
            case Subtype.Spoilable:
                items.Add(tempItem);
                break;
        }
    }
}




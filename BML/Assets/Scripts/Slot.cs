using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Item storedItem;
    public Transform pos;
    private bool containsItem = false;
    //private GameObject tempObj;

    void Start()
    {
        
    }

    void Update()
    {
        //if (item != null && containsItem == false)
        //{
            //AddItem();
        //}
        //if (item == null && containsItem == true)
        //{
            //RemoveItem();
        //}/
        
    }

    // Adds the item to the slot.
    public void AddItem(Item item, GameObject obj)
    {
        //obj.GetComponent<HandOffset>().inSlot = true;
        //hasItem = true; // Contains an item.
        storedItem = item;
        foreach (Component comp in obj.GetComponents<Component>())
        {
            /*if (!(comp is Transform) && !(comp is MeshRenderer) && !(comp is MeshFilter) && !(comp is HandOffset))
            {
                Destroy(comp);
                
            }*/
            if ((comp is Rigidbody))
            {
                Destroy(comp);
            }
        }
        
        /*
        foreach (Behaviour comp in tempComponents)
        {
            Debug.Log("disabling");
            comp.enabled = false;
        }*/
        
        //storedItem = item; // Sets the item varaible.
        //itemObject = obj;
        //itemObject = Instantiate(storedItem.slotPrefab, itemTransform.position, itemTransform.rotation); // Instantiates item in the slot.
        obj.transform.parent = gameObject.transform; // Sets the instantiated item as a child of the slot.
        obj.transform.localPosition = new Vector3(0, 0, 0);
        obj.transform.localScale = obj.GetComponent<HandOffset>().slotScale;
        obj.transform.localPosition = new Vector3(0, 0, 0);
        obj.transform.localEulerAngles = obj.GetComponent<HandOffset>().slotRotationOffset;
        obj.layer = 3; // Sets to inSlot layer.
        Debug.Log("adding item");

    }

    void RemoveItem()
    {
        storedItem = null;
        containsItem = false;
        //Destroy(tempObj);
    }
}

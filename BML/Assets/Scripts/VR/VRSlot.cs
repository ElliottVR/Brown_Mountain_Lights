using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSlot : MonoBehaviour
{
    public Item storedItem;
    public Transform itemTransform;
    private GameObject itemObject;
    public bool hasItem;

    public Grab lHand;
    public Grab rHand;

    private List<Component> tempComponents;

    private void Awake()
    {
        lHand = GameObject.Find("LeftHand").GetComponent<Grab>();
        rHand = GameObject.Find("RightHand").GetComponent<Grab>();
    }

    // If item is being held in the slot trigger.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != null && other.gameObject.layer == LayerMask.NameToLayer("Slottable") && hasItem == false) // If the object held is an item that can be put in the inventory.
        {
            if (other.gameObject.transform.parent.GetComponent<HandOffset>().grabbed == false) // If object is not being held anymore.
            {
                Item tempItem = other.gameObject.transform.parent.GetComponent<HandOffset>().item; // Sets item.
                AddItem(tempItem, other.transform.parent.gameObject); // Calls add item function with the new item.
            }
        }

        if (other.gameObject != null && other.gameObject.layer == LayerMask.NameToLayer("RHand") && hasItem == true) // If right hand enters the trigger.
        {   
            if (other.gameObject.GetComponent<Grab>().grabbed == false && rHand.gripping == true) // If hand is not grabbing anything else.
            {
                RemoveItem(other.gameObject); // Calls the remove item function.
            }
        }
        if (other.gameObject != null && other.gameObject.layer == LayerMask.NameToLayer("LHand") && hasItem == true) // If right hand enters the trigger.
        {
            if (other.gameObject.GetComponent<Grab>().grabbed == false && lHand.gripping == true) // If hand is not grabbing anything else.
            {
                RemoveItem(other.gameObject); // Calls the remove item function.
            }
        }

    }
    
    // Adds the item to the slot.
    public void AddItem(Item item, GameObject obj)
    {
        if (obj.GetComponent<HandOffset>().grabbed == false)
        {
            obj.GetComponent<HandOffset>().inSlot = true;
            hasItem = true; // Contains an item.

            foreach (Component comp in obj.GetComponents<Component>())
            {
                if ((comp is Rigidbody))
                {
                    Destroy(comp);
                }
            }
            
            storedItem = item; // Sets the item varaible.
            itemObject = obj;
            //itemObject = Instantiate(storedItem.slotPrefab, itemTransform.position, itemTransform.rotation); // Instantiates item in the slot.
            obj.transform.parent = gameObject.transform; // Sets the instantiated item as a child of the slot.
            obj.transform.localPosition = new Vector3(0, 0, 0);
            obj.transform.localScale = obj.GetComponent<HandOffset>().slotScale;
            obj.transform.localPosition = new Vector3(0, 0, 0);
            obj.transform.localEulerAngles = obj.GetComponent<HandOffset>().slotRotationOffset;
            obj.layer = 3; // Sets to inSlot layer.
            Debug.Log("adding item");
        }
    }

    // Removes the item from the slot.
    void RemoveItem(GameObject controller)
    {
        Debug.Log("removing item");
        hasItem = false; // Does not have an item.
        itemObject.transform.parent = null;
        itemObject.transform.localScale = itemObject.GetComponent<HandOffset>().originSize;
        itemObject.layer = 6; // Sets to grabbable layer.
        controller.GetComponent<Grab>().GrabStart(itemObject.GetComponent<Collider>()); // Starts the grab from the controller script.
        
        storedItem = null; // Resets item.
        
        itemObject.GetComponent<HandOffset>().inSlot = false;
        itemObject = null;
    }
}

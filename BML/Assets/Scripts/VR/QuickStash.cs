using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickStash : MonoBehaviour
{
    public GameObject[] slots;
    public GameObject backPack;
    public GameObject geigerCounter;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable"))
        {
            if (other.gameObject.GetComponent<HandOffset>().grabbed == false && other.gameObject.GetComponent<HandOffset>().slottable == true)
            {
                Debug.Log("triggered");
                Item item = other.gameObject.GetComponent<HandOffset>().item;
                AddItem(item, other.gameObject);
                
            }
        }
    }

    public void AddItem(Item item, GameObject obj)
    {
        for (int i = 0; slots.Length > i; i++)
        {
            if (slots[i].GetComponent<VRSlot>().storedItem == null)
            {
                slots[i].GetComponent<VRSlot>().AddItem(item, obj);
                break;
            }
        }
    }
}

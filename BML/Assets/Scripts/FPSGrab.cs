using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSGrab : MonoBehaviour
{
    public GameObject objectSeen;
    public GameObject player;
    
    public float maxSight;
    private bool rayHitting;

    public GameObject[] slots;
    private int maxSlots;
    private int slotsUsed;

    void Start()
    {
        maxSlots = slots.Length;
    }

    void Update()
    {
        RaycastHit hit;
        rayHitting = Physics.Raycast(player.transform.position, player.transform.forward, out hit, maxSight);
        //Debug.Log(rayHitting);
        if (rayHitting)
        {
            objectSeen = hit.transform.gameObject;
            //Debug.Log("hit");
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Grabbable") && Input.GetButtonDown("Pickup") && hit.transform.gameObject != null && slotsUsed < maxSlots)
            {
                AddItem(hit.transform.gameObject.GetComponent<HandOffset>().item, hit.transform.gameObject);
                //Destroy(hit.transform.gameObject);
            }
        }
    }

    public void AddItem(Item item, GameObject obj)
    {
        for (int i = 0; slots.Length > i; i++)
        {
            if (slots[i].GetComponent<Slot>().storedItem == null)
            {
                slots[i].GetComponent<Slot>().AddItem(item, obj);
                break;
            }
        }
    }

}

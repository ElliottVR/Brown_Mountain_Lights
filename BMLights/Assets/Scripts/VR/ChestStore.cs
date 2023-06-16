using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestStore : MonoBehaviour
{
    public GameObject item;
    public Grab lHand;
    public Grab rHand;
    public string objectTag;
    private bool itemStored = false;
    private bool comingBack;
    public GameObject storeLoc;
    
    void Update()
    {   
        if (itemStored == false && item.GetComponent<HandOffset>().grabbed == false)
        {
            PutAwayItem();
        }

        if (item.GetComponent<HandOffset>().grabbed == true)
        {
            itemStored = false;
        }
        if (item.GetComponent<HandOffset>().grabbed == false && itemStored == false)
        {
            itemStored = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == item)
        {
            if (item.GetComponent<HandOffset>().grabbed == false)
            {
                PutAwayItem();
            }
        }
        /*
        if (other.gameObject == item)
        {
            if (item.GetComponent<HandOffset>().leftGrabbed == false)
            {
                if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) <= 0)
                {
                    PutAwayItem();
                }
            }
        }*/
    }
    
    void PutAwayItem()
    {
        
        itemStored = true;
        item.GetComponent<HandOffset>().stored = true;
        StopCoroutine("ComeBackToPlayer");
        comingBack = false;
        if (item.GetComponent<Rigidbody>() != null)
        {
            Destroy(item.GetComponent<Rigidbody>());
        }
        
        item.transform.parent = storeLoc.transform;
        item.transform.localPosition = new Vector3(0, 0, 0);
        item.transform.localEulerAngles = storeLoc.transform.localEulerAngles;
        
        Debug.Log("geiger counter stored");
        comingBack = false;
    }

}

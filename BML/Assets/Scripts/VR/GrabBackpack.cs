using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBackpack : MonoBehaviour
{
    
    public GameObject backPack;
    public bool holdingBackpack = false;
    public Grab lHand;
    public Grab rHand;
    private Grab handGrabbedBy;
    
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("LHand") && lHand.gripping == true) // If left hand enters the grab area collider.
        {
            if (holdingBackpack == false && other.gameObject.GetComponent<Grab>().grabbed == false) // Prevents "OpenBackpack" function from calling multiple times.
            {
                OpenBackpack(lHand); // Open backpack.
            } 
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("RHand") && rHand.gripping == true) // If left hand enters the grab area collider.
        {
            if (holdingBackpack == false && other.gameObject.GetComponent<Grab>().grabbed == false) // Prevents "OpenBackpack" function from calling multiple times.
            {
                OpenBackpack(rHand); // Open backpack.
            }
        }
        if (handGrabbedBy != null && holdingBackpack == true)
        {
            if (other.gameObject.tag == "BackPack" && handGrabbedBy.gripping == false) // If backpack enters the grab area collider.
            {
                CloseBackpack(); // Close the backpack.
            }
        }
    }

    // Open the backpack.
    void OpenBackpack(Grab hand)
    {
        handGrabbedBy = hand;
        holdingBackpack = true; // Is holding backpack.
        Debug.Log("Holding backpack");
        backPack.SetActive(true); // Sets the backpack to active.
        hand.GrabStart(backPack.GetComponent<Collider>()); // Communicates with the hand script to start a grab.
    }

    // Close the backpack.
    void CloseBackpack()
    {
        holdingBackpack = false; // Not holding backpack.
        backPack.SetActive(false); // Turns backpack model off.
    }
}

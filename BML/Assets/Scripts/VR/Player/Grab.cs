/// Script for virtual reality hands to pick up objects. Unlike the defualt Oculus grab script, it works with locomotion of the player. Grabbable objects need the "grabbable" layer as well as the "HandOffset" script attached.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Grab : MonoBehaviour
{
    public bool left;
    
    private bool lGripping = false;
    private bool rGripping = false;
    public bool gripping = false;

    public bool grabbed = false;

    public GameObject handParent;
    public SkinnedMeshRenderer handModel;

    private Collider tempCollision;
    public GameObject tempObj;
    private Rigidbody tempRb;

    public OVRInput.Controller controller;
    private Vector3 anchorPos;
    private Quaternion anchorRot;

    public TimeControl time;

    
    private void Awake()
    {
        anchorPos = transform.localPosition;
        anchorRot = transform.localRotation;
        
        time = GameObject.Find("TimeControl").GetComponent<TimeControl>();
    }

    void Update()
    {
        // Lets go of object once grab button is released.
        if (grabbed == true) // If already holding object.
        {
            if (left == true && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) == 0) // If left hand button released.
            {
                GrabEnd(); // Let go of object.
            }
            if (left == false && OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) == 0) // If right hand button released.
            {
                GrabEnd(); // Let go of object.
            }
        }
        if (tempObj == null)
        {
            //GrabEnd();
        }
        
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0)
        {
            lGripping = true;
        }
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) <= 0)
        {
            lGripping = false;
        }

        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0)
        {
            rGripping = true;
        }
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) <= 0)
        {
            rGripping = false;
        }
        if (left == true)
        {
            gripping = lGripping;
        }
        if (left == false)
        {
            gripping = rGripping;
        }

    }

    // If hand trigger is colliding with a grabbable object pick it up.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable")) // If layer of the object is "grabbable".
        {
            if (other.gameObject.GetComponent<HandOffset>() != null)
            {
                if (other.gameObject.GetComponent<HandOffset>().grabbed == false) // Prevents grabbing an object from other hand.
                {
                    Debug.Log("collide");
                    if (lGripping && grabbed == false && left == true) // If holding grab button, not holding anything already, and using left hand.
                    {
                        tempCollision = other; // Stores the colliders data.
                        GrabStart(tempCollision); // Grab object.
                    }
                    if (rGripping && grabbed == false && left == false/* && other.gameObject.tag != "BackPack"*/) // If holding grab button, not holding anything already, and using right hand.
                    {
                        tempCollision = other; // Stores the colliders data.
                        GrabStart(tempCollision); // Grab object.
                    }
                }
            }
        }
    }

    // Sets grabbable object's parent to the hand and removes the rigidbody.
    public void GrabStart(Collider colliding)
    {
        grabbed = true; // Holding something in hand.
        
        if (grabbed)
        {
            Debug.Log("start");
            
            tempObj = colliding.gameObject; // Sets gameobject variable.
            if (tempObj.GetComponent<HandOffset>().stored == true)
            {
                tempObj.GetComponent<HandOffset>().stored = false;
            }
            tempObj.GetComponent<HandOffset>().grabbed = true;
            if (left)
            {
                tempObj.GetComponent<HandOffset>().leftGrabbed = true;
            }
            else
            {
                tempObj.GetComponent<HandOffset>().leftGrabbed = false;
            }
            tempRb = tempObj.GetComponent<Rigidbody>(); // Sets rigidbody variable.
            handModel.enabled = false; // Hand is not visible anymore.
            tempObj.transform.parent = handParent.transform; // Sets the objects parent to the hand.
            if (!left && !tempObj.GetComponent<HandOffset>().hasScreen) // If grabbed by right hand.
            {
                tempObj.transform.localPosition = -tempObj.GetComponent<HandOffset>().positionOffset; // Sets the offsetted position of the object in the hand to the offset in a separate script.
                tempObj.transform.localEulerAngles = -tempObj.GetComponent<HandOffset>().rotationOffset; // Sets the offsetted rotation of the object in the hand to the offset in a separate script.
            }
            else if (!left && tempObj.GetComponent<HandOffset>().hasScreen) // If grabbed by right hand but object is one sided (EX. Geiger Counter)
            {
                tempObj.transform.localPosition = tempObj.GetComponent<HandOffset>().positionOffset; // Sets the offsetted position of the object in the hand to the offset in a separate script.
                Vector3 newOffset = new Vector3(tempObj.GetComponent<HandOffset>().rotationOffset.x, tempObj.GetComponent<HandOffset>().rotationOffset.y, -tempObj.GetComponent<HandOffset>().rotationOffset.z);
                tempObj.transform.localEulerAngles = newOffset;  
            }
            else // If grabbed by left hand.
            {
                tempObj.transform.localPosition = tempObj.GetComponent<HandOffset>().positionOffset; // Sets the offsetted position of the object in the hand to the offset in a separate script.
                tempObj.transform.localEulerAngles = tempObj.GetComponent<HandOffset>().rotationOffset; // Sets the offsetted rotation of the object in the hand to the offset in a separate script.
            }
            //tempObj.GetComponent<HandOffset>().handGrabbedBy = gameObject.GetComponent<Grab>();

            // For item spoiling system.
            if (tempObj.GetComponent<HandOffset>() != null)
            {
                if (tempObj.GetComponent<HandOffset>().item != null)
                {
                    Item storedItem = tempObj.GetComponent<HandOffset>().item;
                    if (storedItem.editorPopup == Item.Subtype.Spoilable && tempObj.GetComponent<HandOffset>().spoiling == false)
                    {
                        if (tempObj.GetComponent<HandOffset>().spoilingMeter != null)
                        {
                            tempObj.GetComponent<HandOffset>().spoilSlider.SetActive(true);
                        }
                        tempObj.GetComponent<HandOffset>().whenSpoiled = time.AddedTime(storedItem.timeUntilSpoiled);
                        
                        // Sets the values on the slider.
                        tempObj.GetComponent<HandOffset>().spoilingMeter.minValue = (int)GameVariables.Time.TotalMilliseconds;
                        tempObj.GetComponent<HandOffset>().spoilingMeter.maxValue = (int)tempObj.GetComponent<HandOffset>().whenSpoiled.TotalMilliseconds;

                        Debug.Log("Spoiling: " + tempObj.GetComponent<HandOffset>().whenSpoiled);
                        tempObj.GetComponent<HandOffset>().spoiling = true;
                    }
                }
            }
            
            Destroy(tempRb); // Rigidbody causes object to bug out while holding it in hand. Remove it.
        }
    }

    // Sets grabbable parent to null and adds a rigidbody.
    public void GrabEnd()
    {
        handModel.enabled = true; // Hand is visible now.

        if (tempObj != null)
        {
            tempObj.GetComponent<HandOffset>().grabbed = false;
            if (tempObj.GetComponent<HandOffset>().inSlot == false && tempObj.GetComponent<HandOffset>().stored == false)
            {
                tempRb = tempObj.AddComponent<Rigidbody>(); // Add rigidbody so object can fall when let go.
                
                tempObj.transform.parent = null; // Grabbed object becomes unparrented.

                // Gets the controller velocity.
                OVRPose localPose = new OVRPose { position = OVRInput.GetLocalControllerPosition(controller), orientation = OVRInput.GetLocalControllerRotation(controller) };
                OVRPose offsetPose = new OVRPose { position = anchorPos, orientation = anchorRot };
                localPose = localPose * offsetPose;
                OVRPose trackingSpace = transform.ToOVRPose() * localPose.Inverse();
                Vector3 linearVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerVelocity(controller);
                Vector3 angularVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerAngularVelocity(controller);
                
                if (tempObj.GetComponent<HandOffset>().spoilingMeter != null)
                {
                    //tempObj.GetComponent<HandOffset>().spoilSlider.SetActive(false);
                }

                // Add velocity to the object the hand is holding.
                if (tempRb != null && tempObj.tag != "BackPack")
                {
                    tempRb.velocity = linearVelocity;
                    tempRb.angularVelocity = angularVelocity;
                }
                if (tempRb != null && tempObj.tag == "BackPack")
                {
                    tempRb.isKinematic = true;
                }
                //tempObj.GetComponent<HandOffset>().spoiling = false;
            }
            
        }
        StartCoroutine("ResetTempValues"); // Starts timer coroutine.
    }

    // Timer to reset values. Fixes issue of values resetting before "GrabEnd" function finished.
    IEnumerator ResetTempValues()
    {
        yield return new WaitForSeconds(0f);
        grabbed = false; // Not holding something in hand.
        //if (tempObj.GetComponent<HandOffset>().handGrabbedBy != null && tempObj != null)
        //{
            //tempObj.GetComponent<HandOffset>().handGrabbedBy = null;
        //}
        // Resets all temporary values for next time.
        tempRb = null;
        tempCollision = null;
        tempObj = null;
    }
}

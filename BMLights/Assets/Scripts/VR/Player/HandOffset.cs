/// Position and rotation offset for the object that the player is holding with the grab script.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class HandOffset : MonoBehaviour
{
    public bool grabbed = false;
    public bool inSlot = false;
    public bool stored;
    public bool slottable;
    public bool leftGrabbed;
    public Item item;

    public GameObject spoilSlider;
    public Slider spoilingMeter;
    public bool spoiling = false;
    
    public TimeSpan whenSpoiled;

    [Header("Transform Variables")]
    public Vector3 positionOffset = new Vector3(0, 0, 0);
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public Vector3 slotPositionOffset = new Vector3(0, 0, 0);
    public Vector3 slotRotationOffset = new Vector3(0, 0, 0);
    public Vector3 originSize;
    public Vector3 slotScale = new Vector3(0, 0, 0);
    public bool hasScreen;
    

    public void SetTransforms() // Editor tool to set the location of the item in hand.
    {
        positionOffset = transform.localPosition;
        rotationOffset = transform.localEulerAngles;
    }

    private void Awake()
    {
        if (item != null)
        {
            item.timeUntilSpoiled = new TimeSpan(item.hours, item.minutes, 0);
            Debug.Log(item.ToString() + item.timeUntilSpoiled);
        }
        if (spoilingMeter != null)
        {
            spoilSlider.SetActive(false);
        }
    }
    private void Update()
    {
        if (spoiling == true)
        {
            spoilingMeter.value = (float)GameVariables.Time.TotalMilliseconds;
        }
    }


}

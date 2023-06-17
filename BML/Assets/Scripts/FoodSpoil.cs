using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FoodSpoil : MonoBehaviour
{
    public TimeControl timer;
    
    public void SpoilTime(GameObject item, TimeSpan spoilTime)
    {
        TimeSpan end = timer.AddedTime(spoilTime);
        item.GetComponent<HandOffset>().whenSpoiled = end;
        item.GetComponent<HandOffset>().spoilingMeter.minValue = (int)GameVariables.Time.TotalMinutes;
        item.GetComponent<HandOffset>().spoilingMeter.maxValue = (int)end.TotalMinutes;
        Debug.Log("set values");
    }
}

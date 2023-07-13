using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLights : MonoBehaviour
{
    public GameObject ObjectToEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ObjectToEnter.SetActive(true);
            Debug.Log("BML incoming. . .");
        }
    }


}

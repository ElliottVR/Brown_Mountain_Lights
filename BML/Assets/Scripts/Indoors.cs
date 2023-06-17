using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indoors : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.tag=="Player")
        {
            GameVariables.isIndoors = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameVariables.isIndoors = false;
        }
    }

}

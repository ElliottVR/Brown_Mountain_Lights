using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCertainCollisions : MonoBehaviour
{
    public string tagName;
    // Ignore backpack tagged collisions.
    private void OnCollisionStay(Collision collision)
    {
       if (collision.gameObject.layer == LayerMask.NameToLayer(tagName) && gameObject.GetComponent<Collider>() != null)
       {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), false);
       }
    }
     
}

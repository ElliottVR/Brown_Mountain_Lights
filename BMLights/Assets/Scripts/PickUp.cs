using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private bool InHand = false;
    private bool CanDrop = false;
    public GameObject Hand;
    public GameObject Self;
    public GameObject PlayerRaycast;
    public Rigidbody ObjectRigidbody;
    public Collider ObjectCollider;
    private bool LookingAtSurface = false;

    public Vector3 itemPlacementLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InHand == true)
        {
            //Self.transform.position = Hand.transform.position;
            ObjectRigidbody.useGravity = false;
            ObjectRigidbody.isKinematic = true;
            Self.transform.parent = Hand.transform;
            ObjectCollider.enabled = false;
            Self.transform.localPosition = new Vector3(0, -.25f, .5f);
            

            if (Input.GetButtonDown("Fire2") && CanDrop == true)
            {
                InHand = false;
                Drop();
                CanDrop = false;
                PlayerRaycast.GetComponent<Raycast>().SendMessage("ItemDrop");
            }

            if (Input.GetButtonDown("Fire1") && itemPlacementLocation != null)
            {
                //LookingAtSurface = 
                //Debug.Log(LookingAtSurface);

               

           
            }
        }
    }

    public void Grab()
    {
        if (InHand == false)
        {
            InHand = true;
            StartCoroutine(CanDropWait());
        }
    }

    public void Drop()
    {
        //Self.transform.position = Hand.transform.position;
        Self.transform.parent = null;
        ObjectRigidbody.useGravity = true;
        ObjectRigidbody.isKinematic = false;
        ObjectCollider.enabled = true;
    }

    public void PositionDrop(Vector3 itemPlacementLocation)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Self.transform.parent = null;
            Self.transform.localPosition = itemPlacementLocation;
            ObjectRigidbody.useGravity = true;
            ObjectRigidbody.isKinematic = false;
            ObjectCollider.enabled = true;

            
            //Self.transform.position = new Vector3(itemPlacementLocation.x, itemPlacementLocation.y, itemPlacementLocation.z);

            InHand = false;
            PlayerRaycast.GetComponent<Raycast>().SendMessage("ItemDrop");
        }

    }
    IEnumerator CanDropWait()
    {
        yield return new WaitForSeconds(.2f);
        CanDrop = true;
    }
}

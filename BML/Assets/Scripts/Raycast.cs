using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{

    public GameObject UiReticle;
    public GameObject ItemPlaceReticle;
    private Vector3 scaleChange;
    private Vector3 originalScale;
    private Vector3 ObjectPlacementLoc;
    public bool LookingAtSurface = false;
    private bool HandEmpty = true;

    private PickUp pickupScript;
    private Vector3 currentItemPlaceReticle;

    private bool radioOn = false;

    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 240;
        scaleChange = new Vector3(0.2f, 0.2f, 0.2f);
        originalScale = new Vector3(0.1f, 0.1f, 0.1f);

        pickupScript = gameObject.GetComponent<PickUp>();
    }

    // Update is called once per frame
    public void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, forward, Color.green);

        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, 3, 1 << LayerMask.NameToLayer("Selectable")))
        {
            UiReticle.transform.localScale = scaleChange;

            if (hit.collider.tag == "Door" && Input.GetButtonDown("Fire"))
            {
                if (hit.collider != null)
                {

                    hit.collider.transform.GetComponent<DoorOpen>().SendMessage("Open");
                    Debug.Log("Raycast hit " + hit.collider.gameObject.name);
                    Debug.DrawLine(ray.origin, ray.origin + ray.direction * 2, Color.green);



                }
            }

            if (hit.collider.tag == "PickUpAble" && Input.GetButtonDown("Fire") && HandEmpty == true)
            {
                if (hit.collider != null)
                {
                    hit.collider.transform.GetComponent<PickUp>().SendMessage("Grab");

                    Debug.Log("Raycast hit " + hit.collider.gameObject.name);
                    Debug.DrawLine(ray.origin, ray.origin + ray.direction * 2, Color.green);

                    //GameObject HeldObject = hit.gameObject;

                    // finds PickUp.cs on the held object
                    pickupScript = hit.transform.GetComponent<PickUp>();
                    StartCoroutine(Wait());
                }
            }

            if (hit.collider.name == "Radio" && Input.GetButtonDown("Fire"))
            {
                if (hit.collider != null)
                {
                    if (radioOn == false)
                    {
                        hit.collider.transform.GetComponent<AudioSource>().Play();
                        StartCoroutine(WaitRadio());
                    }

                    if (radioOn == true)
                    {
                        hit.collider.transform.GetComponent<AudioSource>().Stop();
                        StartCoroutine(WaitRadio());
                    }

                }
            }
        }

        else
        {
            UiReticle.transform.localScale = originalScale;
        }

        if (Physics.Raycast(ray, out hit, 3, 1 << LayerMask.NameToLayer("Surface")))
        {
            if (hit.collider.tag == "Surface" && HandEmpty == false)
            {
                if (hit.collider != null)
                {
                    ObjectPlacementLoc = hit.point;
                    LookingAtSurface = true;
                    UiReticle.SetActive(false);
                    ItemPlaceReticle.SetActive(true);
                    ItemPlaceReticle.transform.position = hit.point;

                    if (ItemPlaceReticle.transform.position != null && Input.GetButtonDown("Fire"))
                    {

                        // determines Vector3 of raycast on suface
                        currentItemPlaceReticle = ItemPlaceReticle.transform.position;


                        // transfers Vector3 of reticle position to PositionDrop() method in PickUp.cs and runs method
                        pickupScript.transform.GetComponent<PickUp>().SendMessage("PositionDrop", currentItemPlaceReticle);
                        Debug.Log(pickupScript.itemPlacementLocation);

                        // set HandEmpty to true
                        HandEmpty = true;
                        UiReticle.SetActive(true);
                    }
                }
            }
        }
        else
        {
            ItemPlaceReticle.SetActive(false);
            LookingAtSurface = false;
            UiReticle.SetActive(true);
        }
    }

    public void ItemDrop()
    {
        HandEmpty = true;
        ItemPlaceReticle.SetActive(false);
        UiReticle.SetActive(true);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.1f);
        HandEmpty = false;
    }

    IEnumerator WaitRadio()
    {
        yield return new WaitForSeconds(0.5f);
        radioOn = !radioOn;
    }
}

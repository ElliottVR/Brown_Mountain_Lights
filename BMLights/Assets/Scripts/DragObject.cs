using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public Camera mainCam;
    private float CameraDistance;

    void Start()
    {
        mainCam = Camera.main;
        CameraDistance = mainCam.WorldToScreenPoint(transform.localPosition).x;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Main"))
        {

        }
        if (Input.GetButton("Main"))
        {
            PointerHold();
        }
    }

    void PointerClick()
    {

    }

    void PointerHold()
    {
        Debug.Log("dragging");
        Vector3 ScreenPos = new Vector3(CameraDistance, Input.mousePosition.y, Input.mousePosition.z);
        transform.position = mainCam.ScreenToWorldPoint(ScreenPos);
    }
}

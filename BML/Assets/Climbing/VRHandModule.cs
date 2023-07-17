using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRHandModule : MonoBehaviour
{
    public bool inUse;
    public bool keyDown;
    private bool triggered;

    [Space(10)]

    [SerializeField] private HandPresence _myHand;
    [SerializeField] private GameObject _controllerAnchor;
    [SerializeField] private GameObject _myHandMesh;
    [SerializeField] private Rigidbody _myHandRb;

    [Space(10)]

    [SerializeField] private LayerMask _stationaryLayer;

    [Space(10)]

    public Vector3 handDefultPosition = new Vector3(-.001f, .001f, -.035f);
    public Vector3 handDefuaultRotation;
    
    [Space(10)]

    public GameObject grabbedObj;

    public Vector3 force;
    private Vector3 tempPos;

    private Vector3 lastPosition;

    private void FixedUpdate()
    {
        tempPos = _myHand.gameObject.transform.position; // Gets fixed position of VR hand.
    }
    private void LateUpdate()
    {
        force = tempPos - _myHand.gameObject.transform.position; // Subtracts late position from fixed position to calculate force.
    }

    private void Update()
    {
        CheckForInput();

        if (!keyDown)
        {
            grabbedObj = null;
        }
        if (triggered && keyDown) // If grabbing
        {
            inUse = true;
        }
        else // If not grabbing
        {
            inUse = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((_stationaryLayer.value & (1 << other.transform.gameObject.layer)) > 0) // Checks if the player is touching a stationary target interactable layer.
        {
            triggered = true;

            if (keyDown)
            {
                grabbedObj = other.gameObject;

                _myHandMesh.transform.parent = null; // Locks hand position at the initial grab point.
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((_stationaryLayer.value & (1 << other.transform.gameObject.layer)) > 0) // Checks if the player is exiting stationary target interactable layer.
        {
            grabbedObj = null;
        }
    }

    /// Checks for grip input from controller.
    private void CheckForInput()
    {
        if (_myHand.targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue)) // Checks if grip button is being pressed on specific hand that is touching object.
        {
            if (gripValue > 0.1) // Key down
            {
                keyDown = true;
            }    
            else if (gripValue < 0.1) // Key up
            {
                keyDown = false;
                Release(); // Release from grab point if input key up.
            }
        }
    }

    /// Releases hand from stationary position.
    private void Release()
    {
        triggered = false;

        _myHandMesh.transform.parent = _myHand.transform; // Resets the parent of the hand mesh.
        _myHandMesh.transform.localPosition = handDefultPosition; // Resets the hand mesh position to default.
        _myHandMesh.transform.localEulerAngles = handDefuaultRotation; // Resets the hand mesh rotation to default.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHandPhysics : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.localPosition = new Vector3(0, 0, 0);
    }
    private void FixedUpdate()
    {
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;
        
        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation); rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
        Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;
        rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}

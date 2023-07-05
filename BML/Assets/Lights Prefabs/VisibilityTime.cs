using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityTime : MonoBehaviour
{
    private float visibleTime; // Total time the object should remain visible
    private float elapsedTime; // Time elapsed since the object spawned

    // Set the visible time for the object
    public void SetVisibleTime(float time)
    {
        visibleTime = time;
        elapsedTime = 0f;
    }

    // Check if the object should be despawned
    public bool ShouldDespawn()
    {
        elapsedTime += Time.deltaTime;
        return elapsedTime >= visibleTime;
    }
}

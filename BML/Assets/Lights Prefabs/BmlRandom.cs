using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BmlRandom : MonoBehaviour
{
    public GameObject TimedIntroLight;
    public AudioSource EnterSound;
    public AudioSource SwitchSound;

    public GameObject[] prefabs;  // Array of prefab objects to spawn
    public Transform center;      // Center transform around which objects will spawn
    public float spawnRadius = 5f; // Maximum distance from the center to spawn objects
    public float minVisibleTime = 2f; // Minimum time an object should remain visible
    public float maxVisibleTime = 5f; // Maximum time an object should remain visible
    public float height = 0f;

    private List<GameObject> spawnedObjects = new List<GameObject>(); // Keep track of spawned objects

    // Start is called before the first frame update
    void Start()
    {
        // Start spawning objects
        StartCoroutine(IntroLight());
        //StartCoroutine(SpawnObjects());
    }

    // Update is called once per frame
    void Update()
    {
        // Check if any spawned objects should be despawned
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            GameObject spawnedObject = spawnedObjects[i];

            //Debug.Log("spawnedObject count: " + spawnedObjects.Count);

            // Check if the spawned object is null or destroyed
            if (spawnedObject == null)
            {
                spawnedObjects.RemoveAt(i);
                continue;
            }

            var visibilityTime = spawnedObject.GetComponent<VisibilityTime>();

            if (visibilityTime.ShouldDespawn())
            {
                spawnedObjects.RemoveAt(i);
                Destroy(spawnedObject);
            }
        }
    }

    // Coroutine to spawn objects
    IEnumerator SpawnObjects()
    {
        {
            while (true)
            {

                // Play the SwitchSound
                if (!SwitchSound.isPlaying)
                {
                    SwitchSound.Play();
                }

                // Randomly select a prefab from the array
                GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

                // Calculate a random position within the spawn radius around the center transform
                Vector3 spawnPosition = center.position + Random.insideUnitSphere * spawnRadius;
                spawnPosition.y = height; // Ensure objects are at ground level or desired height

                // Spawn the prefab at the random position
                GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
                spawnedObjects.Add(spawnedObject);

                // Calculate a random visible time for the spawned object
                float visibleTime = Random.Range(minVisibleTime, maxVisibleTime);

                // Set the visible time for the spawned object
                var visibilityTime = spawnedObject.GetComponent<VisibilityTime>();
                if (visibilityTime != null)
                {
                    visibilityTime.SetVisibleTime(visibleTime);
                }

                // Randomly set the rotation speed for the spawned object
                float rotationSpeed = Random.Range(10f, 50f); // Adjust the speed range as needed

                // Randomly set the rotation direction
                int rotationDirection = Random.value < 0.5f ? -1 : 1;

                // Randomly set the rotation axis
                Vector3 rotationAxis = Random.insideUnitSphere.normalized;

                // Store the rotation center position
                Vector3 rotationCenter = center.position;

                // Wait for the visible time to elapse
                while (visibleTime > 0f && spawnedObject != null)
                {
                    if (spawnedObject.activeSelf)
                    {
                        // Rotate the spawned object around the center transform
                        spawnedObject.transform.RotateAround(rotationCenter, rotationAxis, rotationDirection * rotationSpeed * Time.deltaTime);
                    }

                    visibleTime -= Time.deltaTime;
                    yield return null;
                }

                // Despawn the object
                spawnedObjects.Remove(spawnedObject);
                Destroy(spawnedObject);

            }
        }
    }

    IEnumerator IntroLight()
    {
        yield return new WaitForSeconds(7);
        EnterSound.Play();
        yield return new WaitForSeconds(5);
        Destroy(TimedIntroLight);
        StartCoroutine(SpawnObjects());
    }
}

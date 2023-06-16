using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotEffect : MonoBehaviour
{
    public AudioSource distantShot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("DistantShotLoop");
        }
    }

    IEnumerator DistantShotLoop()
    {
        if (!distantShot.isPlaying)
        {
            distantShot.Play();
        }
        yield return new WaitForSeconds(5.0f);
    }
}

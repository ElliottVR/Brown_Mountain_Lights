using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukesIntro : MonoBehaviour
{
    public GameObject[] mushroomClouds;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("NukesLoop");
    }

    void Update()
    {

    }

    IEnumerator NukesLoop()
    {
        yield return new WaitForSeconds(4);

        for (int i = mushroomClouds.Length - 1; i >= 0; i--)
        {
            mushroomClouds[i].SetActive(true);

            float rand = Random.Range(0.5f, 7.0f);
            yield return new WaitForSeconds(rand);

        }
    }
}

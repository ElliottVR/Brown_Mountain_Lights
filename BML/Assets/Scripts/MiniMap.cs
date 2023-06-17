using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private Transform player;
    private GameObject playerObject;
    public GameObject Map1;
    public GameObject Map2;
    public GameObject Map3;
    public GameObject RawImageMap;


    private bool canSwitchMap;
    private int mapNumber;


    void Start()
    {
        
        mapNumber = 1;
        canSwitchMap = true;
        RawImageMap.SetActive(true);
        StartCoroutine("FindPlayer");
        
    }

    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && canSwitchMap==true)
        {
            canSwitchMap = false;

            SwitchMap();
        }
    }

    void SwitchMap()
    {
        if (mapNumber == 1)
        {
            mapNumber = 2;
            RawImageMap.SetActive(true);
            Map1.SetActive(false);
            Map2.SetActive(true);
            Map3.SetActive(false);
        }

        else if (mapNumber == 2)
        {
            mapNumber = 3;
            RawImageMap.SetActive(true);
            Map1.SetActive(false);
            Map2.SetActive(false);
            Map3.SetActive(true);
        }

        else if (mapNumber == 3)
        {
            mapNumber = 4;
            RawImageMap.SetActive(true);
            Map1.SetActive(true);
            Map2.SetActive(false);
            Map3.SetActive(false);
        }

        else if (mapNumber == 4)
        {
            mapNumber = 1;
            RawImageMap.SetActive(false);
            Map1.SetActive(false);
            Map2.SetActive(false);
            Map3.SetActive(false);
        }

        StartCoroutine("SwitchWait");

        Debug.Log("Switched to Map " + mapNumber);
    }

    IEnumerator SwitchWait()
    {
        yield return new WaitForSeconds(0.2f);
        canSwitchMap = true;
    }


    IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(1f);
        playerObject = GameObject.FindGameObjectWithTag("Player");
        yield return new WaitForSeconds(0.5f);
        player = playerObject.transform;
    }
}

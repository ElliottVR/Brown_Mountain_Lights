using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    public GameObject PlayerPrefab;


    // Start is called before the first frame update
    void Start()
    {
        if (GameVariables.SpawnPlayer == true)
        {
            SpawnPlayerController();
        }
    }

    void SpawnPlayerController()
    {
        //  make GameVariable FALSE, so the player doesn't spawn again 
        GameVariables.SpawnPlayer = false;

        // instantiate an instance of the PREFAB
        GameObject playerInstance = Instantiate(PlayerPrefab, transform.position, transform.rotation);
    }

}

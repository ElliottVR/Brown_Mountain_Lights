using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocation : MonoBehaviour
{
    public Collider[] enemyLocations;

    //public Collider Loc1Collider;
    //public Collider Loc2Collider;

    private int hostileChance;

    private bool isHostile;

    // Start is called before the first frame update
    void Start()
    {
        LoopEnemyLocations();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("");
    }

    void LoopEnemyLocations()
    {
        // Repeat code for all objects in the array, one by one
        foreach (Collider Loc in enemyLocations)
        {
            hostileChance = Random.Range(0, 2);
            if (hostileChance == 1)
            {
                isHostile = true;
                Loc.enabled = true;
            }

            else
            {
                isHostile = false;
                Loc.enabled = false;
            }

            Debug.Log(Loc.name + " is Hostile " + isHostile);
            Debug.Log(Loc.name + " Hostile Chance = " + hostileChance);
        }
    }
}

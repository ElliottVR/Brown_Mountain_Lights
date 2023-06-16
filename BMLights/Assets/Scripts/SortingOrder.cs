using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrder : MonoBehaviour
{
    private GameObject Terrain;
    private GameObject[] Tree;
    private GameObject Moon;


    void Start()
    {
        Moon = GameObject.Find("Moon");
        Terrain = GameObject.Find("Terrain");
        Tree = GameObject.FindGameObjectsWithTag("Tree");

        SortLayers();
    }

    void SortLayers()
    {
        Moon.GetComponent<Renderer>().sortingOrder = 0;
        Terrain.GetComponent<Renderer>().sortingOrder = 1;

        foreach (GameObject tree in Tree)
        {
            tree.GetComponent<Renderer>().sortingOrder = 1;
        }
    }

}

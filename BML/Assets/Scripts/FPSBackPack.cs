using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSBackPack : MonoBehaviour
{
    public GameObject backPack;
    private bool opened = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (Input.GetButtonDown("Backpack")) // If pressing the key to open backpack.
        {
            Debug.Log("opened");
            opened = !opened; // Sets the opened state to opposite.
            OpenBackPack(); // Opens backpack.
        }
        
        

    }

    // Opens backpack.
    void OpenBackPack()
    {
        if (opened)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        backPack.SetActive(opened);
        
    }
}

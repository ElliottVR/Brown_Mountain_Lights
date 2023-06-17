using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public bool opensInward;
    public bool isOpen = false;
    public bool playerControl = true;
    //public Animator DoorAnim;
    public Animation doorOpen;
    public Collider doorCollider;
    public AudioSource audioOpen;
    public AudioSource audioClose;


    public void Close()
    {
        playerControl = false;
        StartCoroutine(ControlWait());
    }

    public void Open()
    {
        if (isOpen == false && playerControl == true)
        {
            doorCollider.enabled = false;
            if (!doorOpen.isPlaying && opensInward == false)
                doorOpen.Play("Door Open");
            if (!doorOpen.isPlaying && opensInward == true)
                doorOpen.Play("Door Open Inward");
            audioOpen.Play(0);
            StartCoroutine(WaitOpen());
        }

        else if (isOpen == true && playerControl == true)
        {
            doorCollider.enabled = false;
            if (!doorOpen.isPlaying && opensInward == false)
                doorOpen.Play("Door Close");
            if (!doorOpen.isPlaying && opensInward == true)
                doorOpen.Play("Door Close Inward");
            audioClose.Play(0);
            StartCoroutine(WaitClose());
        }
    }

    IEnumerator WaitOpen()
    {
        yield return new WaitForSeconds(1);
        isOpen = true;
        doorCollider.enabled = true;
    }

    IEnumerator WaitClose()
    {
        yield return new WaitForSeconds(1);
        isOpen = false;
        doorCollider.enabled = true;
    }

    IEnumerator ControlWait()
    {
        yield return new WaitForSeconds(1);
        if (isOpen == true)
        {
            isOpen = false;
            doorCollider.enabled = false;
            if (!doorOpen.isPlaying && opensInward == false)
                doorOpen.Play("Door Close");
            if (!doorOpen.isPlaying && opensInward == true)
                doorOpen.Play("Door Close Inward");
            audioClose.Play(0);
            StartCoroutine(WaitClose());
        }
        yield return new WaitForSeconds(1);
        playerControl = true;
    }
}
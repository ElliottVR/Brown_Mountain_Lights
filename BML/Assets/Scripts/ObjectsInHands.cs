using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsInHands : MonoBehaviour
{
    public GameObject Weapon;
    public Glock glockScript;
    public MeshRenderer GeigerCounter;
    public GameObject GeigerCounterDisplay;
    public AudioSource Alarm;
    public AudioSource High;
    public AudioSource Medium;
    public AudioSource Low;


    private bool weaponIsActive;
    private bool geigerCounterIsActive;


    // Start is called before the first frame update
    void Start()
    {
        GeigerCounter.enabled = true;
        GeigerCounterDisplay.SetActive(true);
        geigerCounterIsActive = true;
        Alarm.volume = 0.2f;
        High.volume = 0.2f;
        Medium.volume = 0.2f;
        Low.volume = 0.2f;

        Weapon.SetActive(false);
        weaponIsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (geigerCounterIsActive)
            {
                GeigerCounter.enabled = false;
                GeigerCounterDisplay.SetActive(false);
                geigerCounterIsActive = false;
                Alarm.volume = 0.075f;
                High.volume = 0.075f;
                Medium.volume = 0.075f;
                Low.volume = 0.075f;
            }
            else
            {
                GeigerCounter.enabled = true;
                GeigerCounterDisplay.SetActive(true);
                geigerCounterIsActive = true;
                Alarm.volume = 0.2f;
                High.volume = 0.2f;
                Medium.volume = 0.2f;
                Low.volume = 0.2f;
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (weaponIsActive)
            {
                Weapon.SetActive(false);
                weaponIsActive = false;
                glockScript.enabled = false;
            }
            else
            {
                Weapon.SetActive(true);
                weaponIsActive = true;
                glockScript.enabled = true;
            }
        }
    }
}

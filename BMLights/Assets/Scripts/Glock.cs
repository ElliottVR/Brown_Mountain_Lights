using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glock : MonoBehaviour
{
    [Header("Shot")]
    [Space(10)]
    public Animation Recoil;
    public AudioSource ShotSound;
    private bool canShoot;
    private bool fired;

    [Header("Reload")]
    [Space(10)]
    public Animation Slide;
    public AudioSource reloadSound;
    private bool canReload;

    private int Ammo;

    // Start is called before the first frame update
    void Start()
    {
        canReload = true;
        canShoot = true;
        Ammo = 15;

        Debug.Log("Ammo Count: " + Ammo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Reload") && canReload == true)
        {
            canReload = false;

            StartCoroutine(Reload(0f));

        }

        if (Input.GetButtonDown("Fire") && canShoot == true && Ammo > 0)
        {
            canShoot = false;

            StartCoroutine(Shoot());
        }

        if (Ammo == 0)
        {
            canReload = false;
            canShoot = false;
            StartCoroutine(Reload(0.5f));
        }

        if (Ammo < 0)
        {
            Ammo = 0;
        }



        if (GetComponent<HandOffset>().grabbed)
        {
            if (GetComponent<HandOffset>().leftGrabbed)
            {
                if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0 && !fired && canShoot == true && Ammo > 0)
                {
                    canShoot = false;

                    StartCoroutine(Shoot());
                }
                if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) <= 0)
                {
                    fired = false;
                }
                if (OVRInput.Get(OVRInput.Button.Three) && canReload == true)
                {
                    canReload = false;

                    StartCoroutine(Reload(0f));
                }
            }
            if (!GetComponent<HandOffset>().leftGrabbed)
            {
                if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0 && !fired && canShoot == true && Ammo > 0)
                {
                    canShoot = false;

                    StartCoroutine(Shoot());
                }
                if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) <= 0)
                {
                    fired = false;
                }
                if (OVRInput.Get(OVRInput.Button.One) && canReload == true)
                {
                    canReload = false;

                    StartCoroutine(Reload(0f));
                }
            }

        }

    }

    IEnumerator Reload(float wait)
    {
        
        yield return new WaitForSeconds(wait);
        canShoot = false;
        Ammo = 0;
        Slide.Play();
        reloadSound.Play();
        Ammo = 15;
        yield return new WaitForSeconds(3.2f);
        //Ammo = 15;
        canReload = true;
        canShoot = true;
        
        Debug.Log("Ammo Count: " + Ammo);
    }

    IEnumerator Shoot()
    {
        fired = true;
        Ammo--;
        //Recoil.Play();
        ShotSound.Play();
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
        
        Debug.Log("Ammo Count: " + Ammo);
    }

}

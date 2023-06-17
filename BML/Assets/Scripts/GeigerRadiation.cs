using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeigerRadiation : MonoBehaviour {

    [Header("Geiger Counter Parts")]
    public GameObject GeigerCounter;
    public Text GeigerCount;
    public Text Dosimeter;

    [Header("Geiger Sound Effects")]
    public AudioSource SoundGeigerLow;
    public AudioSource SoundGeigerMedium;
    public AudioSource SoundGeigerHigh;
    public AudioSource SoundGeigerAlarm;

    [Header("Radiation")]
    public GameObject RadiationSource1;
    public GameObject RadiationSource2;
    public GameObject RadiationSource3;
    public GameObject RadiationSource4;
    public GameObject RadiationSource5;
    public GameObject RadiationSource6;
    public GameObject RadiationSource7;
    public GameObject RadiationSource8;
    public GameObject RadiationSource9;
    public GameObject RadiationSource10;

    // private variables
    private float dist1 = 0;
    private float dist2 = 0;
    private float dist3 = 0;
    private float dist4 = 0;
    private float dist5 = 0;
    private float dist6 = 0;
    private float dist7 = 0;
    private float dist8 = 0;
    private float dist9 = 0;
    private float dist10 = 0;

    private float dosimeter;
    private float dosimeterAdd = 0;
    private float rads = 0;


    // Use this for initialization
    void Start () {

        dosimeter = GameVariables.DoseMillisieverts;

        //for (int i = 0; i < RadiationSources.Length; i++) ;
        //StartCoroutine("AddRadiation");

        Dosimeter = GameObject.Find("Dosimeter Count").GetComponent<Text>();

        RadiationSource1 = GameObject.Find("Radiation Source 1");
        RadiationSource2 = GameObject.Find("Radiation Source 2");
        RadiationSource3 = GameObject.Find("Radiation Source 3");
        RadiationSource4 = GameObject.Find("Radiation Source 4");
        RadiationSource5 = GameObject.Find("Radiation Source 5");
        RadiationSource6 = GameObject.Find("Radiation Source 6");
        RadiationSource7 = GameObject.Find("Radiation Source 7");
        RadiationSource8 = GameObject.Find("Radiation Source 8");
        RadiationSource9 = GameObject.Find("Radiation Source 9");
        RadiationSource10 = GameObject.Find("Radiation Source 10");

    }
	
	// Update is called once per frame
	void Update () {
        //for (int i = 0; i < RadiationSources.Length; i++)

        dist1 = Vector3.Distance(GeigerCounter.transform.position, RadiationSource1.transform.position);

        dist2 = Vector3.Distance(GeigerCounter.transform.position, RadiationSource2.transform.position);

        dist3 = Vector3.Distance(GeigerCounter.transform.position, RadiationSource3.transform.position);

        dist4 = Vector3.Distance(GeigerCounter.transform.position, RadiationSource4.transform.position);

        dist5 = Vector3.Distance(GeigerCounter.transform.position, RadiationSource5.transform.position);

        dist6 = Vector3.Distance(GeigerCounter.transform.position, RadiationSource6.transform.position);

        dist7 = Vector3.Distance(GeigerCounter.transform.position, RadiationSource7.transform.position);

        dist8 = Vector3.Distance(GeigerCounter.transform.position, RadiationSource8.transform.position);

        dist9 = Vector3.Distance(GeigerCounter.transform.position, RadiationSource9.transform.position);

        dist10 = Vector3.Distance(GeigerCounter.transform.position, RadiationSource10.transform.position);



        rads = ((0.2f / dist1) * 20.8f + (0.2f / dist2) * 20.8f + (0.2f / dist3) * 20.8f + (0.2f / dist4) * 20.8f +
                (0.2f / dist5) * 20.8f + (0.2f / dist6) * 20.8f + (0.2f / dist7) * 20.8f + (0.2f / dist8) * 20.8f +
                (0.2f / dist9) * 20.8f + (0.2f / dist10) * 20.8f);

        if (GameVariables.RadiationStorm == true && GameVariables.isIndoors == false)
        {
            rads = rads * 2.0f;
        }


        if (rads <= 0.1f)
        {
            rads = 0.0f;
        }


        GeigerCount.text = rads.ToString("0.00");

        GameVariables.DoseMillisieverts = dosimeter;
        Dosimeter.text = GameVariables.DoseMillisieverts.ToString("0.0");
      

        if (rads > 0.01f)
        {
            StartCoroutine("AddRadiation");
        }


        if (rads < 0.26f)
        {

            if (!SoundGeigerLow.isPlaying)
            {
                SoundGeigerLow.Play();
            }

            if (SoundGeigerMedium.isPlaying)
            {
                SoundGeigerMedium.Stop();
            }

            if (SoundGeigerHigh.isPlaying)
            {
                SoundGeigerHigh.Stop();
            }

        }


        if (rads >= 0.26f && rads < 0.35f)
        {

            if (!SoundGeigerMedium.isPlaying)
            {
                SoundGeigerMedium.Play();
            }

            if (SoundGeigerLow.isPlaying)
            {
                SoundGeigerLow.Stop();
            }

            if (SoundGeigerHigh.isPlaying)
            {
                SoundGeigerHigh.Stop();
            }

        }

        if (rads >= 0.35f)
        {

            if (!SoundGeigerHigh.isPlaying)
            {
                SoundGeigerHigh.Play();
            }

            if (SoundGeigerMedium.isPlaying)
            {
                SoundGeigerMedium.Stop();
            }

            if (SoundGeigerLow.isPlaying)
            {
                SoundGeigerLow.Stop();
            }
        }



        if (rads >= 0.6f)
        {

            if (!SoundGeigerAlarm.isPlaying)
            {
                SoundGeigerAlarm.Play();
            }
        }

        if (rads < 0.6f)
        {

            if (SoundGeigerAlarm.isPlaying)
            {
                SoundGeigerAlarm.Stop();
            }
        }

    }

    IEnumerator AddRadiation()
    {

        yield return new WaitForSeconds(0.01f);

        if (rads >= 0.35f && rads <= 0.59f)
        {
            dosimeter = dosimeter + (rads * 0.003f);
        }

        else if (rads >= 0.6f)
        {
            dosimeter = dosimeter + (rads * 0.006f);
        }

    }

} 
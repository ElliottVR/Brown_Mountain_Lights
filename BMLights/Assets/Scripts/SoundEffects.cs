using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [Header("Music")]
    [Space(10)]
    [SerializeField] private AudioClip[] clips;
    private int clipIndex;
    private AudioSource audio;
    //private bool audioPlaying = false;

    public float minWait = 120f;
    public float maxWait = 240f;

    [Header("Sound Effects")]
    [Space(10)]
    public AudioSource SunriseAmbience;
    public AudioSource DayAmbience;
    public AudioSource SunsetAmbience;
    public AudioSource NightAmbience;

    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {

        if (!audio.isPlaying)
        {

            clipIndex = Random.Range(0, clips.Length);
            audio.clip = clips[clipIndex];
            audio.PlayDelayed(Random.Range(minWait, maxWait));
            
        }

        if (GameVariables.isSunrise == true && !SunriseAmbience.isPlaying)
        {
            SunriseAmbience.Play();
            DayAmbience.Play();
            NightAmbience.Stop();
        }

        if (GameVariables.isDay == true && !DayAmbience.isPlaying)
        {
            DayAmbience.Play();
            SunriseAmbience.Stop();
        }

        if (GameVariables.isDay == true && SunriseAmbience.isPlaying)
        {
            SunriseAmbience.Stop();
        }

        if (GameVariables.isSunset == true && !SunsetAmbience.isPlaying)
        {
            SunsetAmbience.Play();
            DayAmbience.Play();
        }

        if (GameVariables.isNight == true && !NightAmbience.isPlaying)
        {
            NightAmbience.Play();
            SunsetAmbience.Stop();
            DayAmbience.Stop();
        }

    }
}

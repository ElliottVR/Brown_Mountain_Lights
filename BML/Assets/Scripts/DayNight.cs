using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayNight : MonoBehaviour
{
    public GameObject timeController;

    //[SerializeField]
    //private TextMeshProUGUI timeText;

    [SerializeField]
    private Light sunLight;

    public float sunriseHour;
    
    public float sunsetHour;

    [SerializeField]
    private Color dayAmbientLight;

    [SerializeField]
    private Color nightAmbientLight;

    [SerializeField]
    private AnimationCurve lightChangeCurve;

    [SerializeField]
    private float maxSunLightIntensity;

    public Renderer FogSettings;

    //[SerializeField]
    //private Light moonLight;

    //[SerializeField]
    //private float maxMoonLightIntensity;

    private DateTime currentTime;

    private TimeSpan sunriseTime;

    private TimeSpan sunsetTime;

    // Start is called before the first frame update
    void Start()
    {
        //currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);

        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
        RotateOrbits();
        UpdateLightSettings();
    }

    private void UpdateTimeOfDay()
    {
        //currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        //GameVariables.RealTime = currentTime;

        //if (timeText != null)
        //{
        //timeText.text = currentTime.ToString("HH:mm");
        //}
    }

    private void RotateOrbits()
    {
        /*
        float sunLightRotation;
        float moonLightRotation;
        
        if (GameVariables.Time > sunriseTime && GameVariables.Time < sunsetTime)
        {

            TimeSpan sunriseToSunsetDuration = timeController.GetComponent<TimeControl>().Difference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = timeController.GetComponent<TimeControl>().Difference(sunriseTime, GameVariables.Time);

            double percentage = (timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes) * Time.deltaTime;

            sunLightRotation = Mathf.Lerp(GameVariables.Hour, GameVariables.Hour + 1, (float)percentage);
            moonLightRotation = Mathf.Lerp(-180, 0, (float)percentage);
        }
        else
        {
            
            TimeSpan sunsetToSunriseDuration = timeController.GetComponent<TimeControl>().Difference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = timeController.GetComponent<TimeControl>().Difference(sunsetTime, GameVariables.Time);

            double percentage = (timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes) * Time.deltaTime;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
            moonLightRotation = Mathf.Lerp(-360, -180, (float)percentage);
        }
        */
        //sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
        sunLight.transform.rotation = Quaternion.Euler(new Vector3((GameVariables.ConstantTime - 28800) / 86400 * 360, 0, 0));
        
        //moonLight.transform.rotation = Quaternion.AngleAxis(moonLightRotation, Vector3.right);
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);

        // turned intensity scaling off so weather effects can dim sunlight
        //sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));

        //moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));

        FogSettings.material.color = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }


    /*
    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }
    */
}


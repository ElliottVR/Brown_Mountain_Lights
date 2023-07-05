using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light targetLight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float minRange = 5f;
    public float maxRange = 10f;
    public float flickerSpeed = 5f;
    public float intensityRandomnessFactor = 0.1f;
    public float rangeRandomnessFactor = 0.2f;
    public float rangeChangeInterval = 2f;

    private float baseIntensity;
    private float randomIntensity;
    private float baseRange;
    private float randomRange;
    private float rangeChangeTimer;

    private void Start()
    {
        baseIntensity = targetLight.intensity;
        baseRange = targetLight.range;
        rangeChangeTimer = rangeChangeInterval;
    }

    private void Update()
    {
        // Calculate the flickering effect
        float flicker = Mathf.PingPong(Time.time * flickerSpeed, 1f);

        // Apply randomness to the flicker
        flicker += Random.Range(-intensityRandomnessFactor, intensityRandomnessFactor);

        // Apply the flicker to the light intensity
        targetLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, flicker) * baseIntensity;

        // Update range change timer
        rangeChangeTimer -= Time.deltaTime;

        if (rangeChangeTimer <= 0f)
        {
            // Calculate the range flickering effect
            float rangeFlicker = Mathf.PingPong(Time.time * flickerSpeed * 0.5f, 1f);

            // Apply randomness to the range flicker
            rangeFlicker += Random.Range(-rangeRandomnessFactor, rangeRandomnessFactor);

            // Apply the range flicker to the light range
            targetLight.range = Mathf.Lerp(minRange, maxRange, rangeFlicker) * baseRange;

            // Reset range change timer
            rangeChangeTimer = rangeChangeInterval;
        }
    }
}

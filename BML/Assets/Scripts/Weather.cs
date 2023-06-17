using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    [Header("Fog")]
    [Space(10)]
    public int chanceOfFog = 80;
    public int FogDuration = 420;
    public GameObject FogObject;
    public Renderer FogRenderer;
    private float fogFadeSpeed = 0.05f;
    private bool isFoggy = false;
    private bool FogCheck = true;
    private bool canFadeFog = false;
    private float fogColor;
    private bool checkingFogColor = false;
    private bool canChangeFogColor = false;
    private float fogColorChangeSpeed = 0.01f;

    [Header("Radiation Storm")]
    [Space(10)]
    public int StormDuration = 30;

    [Range(0, 1)]
    public float MaxFadeVolume = 0.35f;

    [Header("Weather Domes")]
    [Space(10)]
    public Renderer FairWeather;
    public Renderer RadiationStorm;
    public Renderer RadiationStorm2;
    public Renderer RadiationStorm3;
    public GameObject Overcast;
    public Renderer OvercastRender;

    [Header("Weather Effects")]
    [Space(10)]
    public WindZone windZone;
    public Light Sun;
    public AudioSource Thunder;
    public AudioSource Wind;

    public ParticleSystem Dust;

    [Header("Texture Fading Speed")]
    [Space(10)]
    public float fadeSpeed = 0.05f;


    [Header("Texture Fading Strengths")]
    [Space(10)]
    [Range(0, 1)]
    public float desiredAlphaFair;
    [Range(0, 1)]
    public float currentAlphaFair;
    [Range(0, 1)]
    public float desiredAlphaStorm;
    [Range(0, 1)]
    public float currentAlphaStorm;
    [Range(0, 1)]
    public float desiredAlphaStorm2;
    [Range(0, 1)]
    public float currentAlphaStorm2;
    [Range(0, 1)]
    public float desiredAlphaStorm3;
    [Range(0, 1)]
    public float currentAlphaStorm3;
    [Range(0, 1)]
    public float desiredAlphaOvercast;
    [Range(0, 1)]
    public float currentAlphaOvercast;
    [Range(0, 1)]
    public float desiredSunIntensity;
    [Range(0, 1)]
    public float currentSunIntensity;
    [Range(0, 1)]
    public float desiredAlphaFog;
    [Range(0, 1)]
    public float currentAlphaFog;
    [Range(0, 1)]
    public float desiredColorFog;
    [Range(0, 1)]
    public float currentColorFog = 0.1226415f;
    

    public Color desiredColorCloud;
   
    public float currentColorCloud;



    [Header("Stars")]
    [Space(10)]
    public Renderer StarDome;
    public float starFadeSpeed = 0.05f;
    [Range(0, 1)]
    public float desiredAlphaStars;
    [Range(0, 1)]
    public float currentAlphaStars;

    private float alphaOut = 0.0f;

    private bool canSwitchWeather = true;

    private bool canFade = false;

    private bool canFadeStars = false;

    private bool canChangeCloudColor = true;

    private bool checkingCloudColor = false;

    private float timer;

    private bool BlendClouds = false;

    private int StoredHour;
    private bool canChangeHour = true;
    //private bool hourIncreased = false;

    // Start is called before the first frame update
    void Start()
    {
        Overcast.SetActive(false);
        FairWeather.material.EnableKeyword("_EMISSION");
        BlendClouds = false;

        StartCoroutine(waitToCheckHour());

        StartCoroutine(CheckCloudColor());
        checkingCloudColor = true;
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log("GameVariables hour = " + GameVariables.Hour);
        //Debug.Log("Stored Hour = " + StoredHour);
        //Debug.Log("canChangeHour = " + canChangeHour);

        if (GameVariables.Hour == StoredHour +1 && canChangeHour == true)
        {
            StoredHour++;
            //if (StoredHour >= 24)
            //{
            //    StoredHour = 0;
            //}
            canChangeHour = false;
            HourChange();
        }
        
        if (Input.GetKeyDown(KeyCode.Return) && canSwitchWeather == true)
        {
            canSwitchWeather = false;

            StartCoroutine(Storm(StormDuration));

        }

        if (canFade == true)
        {
            currentAlphaFair = Mathf.MoveTowards(currentAlphaFair, desiredAlphaFair, fadeSpeed * Time.deltaTime);
            FairWeather.material.color = new Color(0, 0, 0, currentAlphaFair);

            currentAlphaStorm = Mathf.MoveTowards(currentAlphaStorm, desiredAlphaStorm, fadeSpeed * Time.deltaTime);
            RadiationStorm.material.color = new Color(0, 0, 0, currentAlphaStorm);

            currentAlphaStorm2 = Mathf.MoveTowards(currentAlphaStorm2, desiredAlphaStorm2, fadeSpeed * Time.deltaTime);
            RadiationStorm2.material.color = new Color(0, 0, 0, currentAlphaStorm2);

            currentAlphaStorm3 = Mathf.MoveTowards(currentAlphaStorm3, desiredAlphaStorm3, fadeSpeed * Time.deltaTime);
            RadiationStorm3.material.color = new Color(0, 0, 0, currentAlphaStorm3);

            currentAlphaOvercast = Mathf.MoveTowards(currentAlphaOvercast, desiredAlphaOvercast, fadeSpeed * Time.deltaTime);
            OvercastRender.material.color = new Color(0, 0, 0, currentAlphaOvercast);

        }

        if (GameVariables.RadiationStorm == false && Sun.intensity < 0.5f && GameVariables.Hour > 6 && GameVariables.Hour < 21)
        {
            StartCoroutine(SunFadeIn());
        }

        if (currentAlphaStars >= 0.5f && GameVariables.Hour >= 8 && GameVariables.Hour < 20)
        {
            StartCoroutine(StarsFadeOut());
        }

        if (GameVariables.RadiationStorm == false && Sun.intensity >= 0.5f && GameVariables.Hour >= 21 ||
            GameVariables.RadiationStorm == false && Sun.intensity >= 0.5f && GameVariables.Hour <= 6)
        {
            StartCoroutine(SunFadeOut());
        }

        if (currentAlphaStars < 0.5f && GameVariables.Hour >= 20 || currentAlphaStars < 0.5f && GameVariables.Hour <= 7)
        {
            StartCoroutine(StarsFadeIn());
        }

        if (canFadeStars == true)
        {
            currentAlphaStars = Mathf.MoveTowards(currentAlphaStars, desiredAlphaStars, starFadeSpeed * Time.deltaTime);
            StarDome.material.color = new Color(0, 0, 0, currentAlphaStars);
        }



        if (FogCheck == true && GameVariables.Hour >= 1 && GameVariables.Hour <= 8)
        {
            FogCheck = false;
            CheckForFog();
        }

        if (canFadeFog == true)
        {
            currentAlphaFog = Mathf.MoveTowards(currentAlphaFog, desiredAlphaFog, fogFadeSpeed * Time.deltaTime);
            FogRenderer.material.color = new Color(fogColor, fogColor, fogColor, currentAlphaFog);
        }

        if (isFoggy && checkingFogColor == false && GameVariables.Hour >= 7)
        {
            StartCoroutine(CheckFogColor());
        }

        if (canChangeFogColor == true)
        {
            currentColorFog = Mathf.MoveTowards(currentColorFog, desiredColorFog, fogColorChangeSpeed * Time.deltaTime);
            FogRenderer.material.color = new Color(currentColorFog, currentColorFog, currentColorFog, 1);
        }

        
        if (canChangeCloudColor == true)
        {
            //currentColorCloud = Mathf.MoveTowards(currentColorCloud, desiredColorCloud, fogColorChangeSpeed * Time.deltaTime);
            //FairWeather.material.SetColor("_EmissionColor", Color.Lerp(FairWeather.material.color, desiredColorCloud, 0.5f * Time.deltaTime));

            //Color AlbedoColor = new Color(0.3301887f, 0.3301887f, 0.3301887f, 1);
            //FairWeather.material.SetColor("_Color", AlbedoColor);

            if (GameVariables.isNight == true)
            {
                Color oldColor = new Color(0.5f, 0.2915989f, 0.2051887f, 1); // orange emission color
                Color newColor = new Color(0.009433985f, 0.009433985f, 0.009433985f, 1); // night black emission color
                Color currentColor;
                //Color currentColor = FairWeather.material.color;
                //Color AlbedoColor = new Color(0.3301887f, 0.3301887f, 0.3301887f, 1);
                //FairWeather.material.SetColor("_Color", AlbedoColor); 

                if (BlendClouds == true)
                {
                    timer += Time.deltaTime / 40.0f;
                    currentColor = Color.Lerp(oldColor, newColor, timer);
                    FairWeather.material.SetColor("_EmissionColor", currentColor);
                }

                if (BlendClouds == false)
                {
                    Color AlbedoColor = new Color(0.3301887f, 0.3301887f, 0.3301887f, 1);
                    FairWeather.material.SetColor("_Color", AlbedoColor); 
                    FairWeather.material.SetColor("_EmissionColor", newColor);  
                }
            }

            if (GameVariables.isSunrise == true)
            {
                Color oldColor = new Color(0.009433985f, 0.009433985f, 0.009433985f, 1); // night black emission color
                Color newColor = new Color(0.5f, 0.2915989f, 0.2051887f, 1); // orange emission color
                Color currentColor;
                //Color currentColor = FairWeather.material.color;

                if (BlendClouds == true)
                {
                    timer += Time.deltaTime / 20.0f;
                    currentColor = Color.Lerp(oldColor, newColor, timer);
                    FairWeather.material.SetColor("_EmissionColor", currentColor);
                }

                if (BlendClouds == false)
                {
                    Color AlbedoColor = new Color(0.3301887f, 0.3301887f, 0.3301887f, 1);
                    FairWeather.material.SetColor("_Color", AlbedoColor);
                    FairWeather.material.SetColor("_EmissionColor", newColor);
                }
            }

            if (GameVariables.isDay == true)
            {
                Color oldColor = new Color(0.5f, 0.2915989f, 0.2051887f, 1); // orange emission color
                Color newColor = new Color(0.75f, 0.75f, 0.75f, 1); // day white emission color
                Color currentColor;
                //Color currentColor = FairWeather.material.color;

                if (BlendClouds == true)
                {
                    timer += Time.deltaTime / 40.0f;
                    currentColor = Color.Lerp(oldColor, newColor, timer);
                    FairWeather.material.SetColor("_EmissionColor", currentColor);
                }

                if (BlendClouds == false)
                {
                    Color AlbedoColor = new Color(0.3301887f, 0.3301887f, 0.3301887f, 1);
                    FairWeather.material.SetColor("_Color", AlbedoColor);
                    FairWeather.material.SetColor("_EmissionColor", newColor);
                }
            }

            if (GameVariables.isSunset == true)
            {
                Color oldColor = new Color(0.75f, 0.75f, 0.75f, 1); // day white emission color
                Color newColor = new Color(0.5f, 0.2915989f, 0.2051887f, 1); // orange emission color
                Color currentColor;
                //Color currentColor = FairWeather.material.color;

                if (BlendClouds == true)
                {
                    timer += Time.deltaTime / 20.0f;
                    currentColor = Color.Lerp(oldColor, newColor, timer);
                    FairWeather.material.SetColor("_EmissionColor", currentColor);
                }

                if (BlendClouds == false)
                {
                    Color AlbedoColor = new Color(0.3301887f, 0.3301887f, 0.3301887f, 1);
                    FairWeather.material.SetColor("_Color", AlbedoColor);
                    FairWeather.material.SetColor("_EmissionColor", newColor);
                }

            }

        }

        if (checkingCloudColor == false)
        {
            if (GameVariables.Hour == 6 || GameVariables.Hour == 7 || GameVariables.Hour == 8 || GameVariables.Hour == 20 || GameVariables.Hour == 21)
            {
                StartCoroutine(CheckCloudColor());
                checkingCloudColor = true;
            }
        }

    }

    void HourChange()
    {
        Debug.Log("Hour has increased");
        //hourIncreased = true;
        checkingCloudColor = false;
        canChangeHour = true;
        StartCoroutine(waitToCheckHour());
    }

    IEnumerator waitToCheckHour()
    {
        yield return new WaitForSeconds(1f);
        StoredHour = GameVariables.Hour;
        Debug.Log("Stored Hour after correction: " + StoredHour);
        //Debug.Log("Stored hour = " + StoredHour);
        //Debug.Log("GameVariables hour = " + GameVariables.Hour);
        //Debug.Log("CurrentTime = " + GameVariables.CurrentTime);
    }

    void CheckForFog()
    {
        int fogChance = Random.Range(0, 100);
        Debug.Log("Chance of Fog: " + fogChance);
        if (fogChance >= chanceOfFog)
        {
            if (GameVariables.Hour >= 7)
            {
                fogColor = 0.74f;
            }
            else
            {
                fogColor = 0.1226415f;
            }

            StartCoroutine(Fog(FogDuration));
        }
    }

    IEnumerator CheckFogColor ()
    {
        checkingFogColor = true;
        desiredColorFog = 0.74f;
        yield return new WaitForSeconds(0.5f);
        canChangeFogColor = true;
        yield return new WaitForSeconds(60f);
        canChangeFogColor = false;

    }


    IEnumerator Fog(int duration)
    {
        isFoggy = true;
        FogRenderer.material.color = new Color(0.1226415f, 0.1226415f, 0.1226415f, 0);
        FogObject.SetActive(true);
        desiredAlphaFog = 1;
        yield return new WaitForSeconds(0.5f);
        canFadeFog = true;
        yield return new WaitForSeconds(25f);
        canFadeFog = false;
        yield return new WaitForSeconds(duration);
        desiredAlphaFog = 0;
        fogColor = 0.74f;
        //yield return new WaitForSeconds(0.5f);
        canFadeFog = true;
        yield return new WaitForSeconds(25f);
        canFadeFog = false;
        FogObject.SetActive(false);
        isFoggy = false;
    }

    IEnumerator CheckCloudColor()
    {
        Debug.Log("Changing cloud color...");
        //checkingCloudColor = true;
        //desiredColorCloud = new Vector4(0, 0, 0); 
        yield return new WaitForSeconds(0.5f);
        

        // Night
        if (GameVariables.Hour <= 6 || GameVariables.Hour >= 21) 
        {
            Debug.Log("It is night.");

            GameVariables.isNight = true;
            GameVariables.isSunrise = false;
            GameVariables.isDay = false;
            GameVariables.isSunset = false;

            timer = 0;
            canChangeCloudColor = true;
            //FairWeather.material.SetColor("_EmissiveColor", Color.black * -10f);
            //FairWeather.material.SetFloat("_EmissiveIntensity", -10f);
            //FairWeather.material.SetColor("_Color", Color.black);

            //FairWeather.material.color = new Color(0.3301887f, 0.3301887f, 0.3301887f, 1);

            //Color color = new Color(0.009433985f, 0.009433985f, 0.009433985f, 1); // night black emission color
            //FairWeather.material.SetColor("_BaseColor", color);
            //FairWeather.material.SetColor("_EmissionColor", Color.black * Mathf.LinearToGammaSpace(-10f));
            //FairWeather.material.SetColor("_EmissionColor", color);
            yield return new WaitForSeconds(41f);
            BlendClouds = true;
            canChangeCloudColor = false;
        }

        // Sunrise
        if (GameVariables.Hour == 7)
        {
            GameVariables.isNight = false;
            GameVariables.isSunrise = true;
            GameVariables.isDay = false;
            GameVariables.isSunset = false;

            timer = 0;
            canChangeCloudColor = true;
            //FairWeather.material.color = new Color(0.7f, 0.7f, 0.7f, 1);
            //FairWeather.material.color = new Color(0.46f, 0.3663397f, 0.2900054f, 1);  // orange albedo color
            //Color color = new Color(0.5f, 0.2915989f, 0.2051887f, 1); // orange emission color
            //FairWeather.material.SetColor("_BaseColor", color);
            //FairWeather.material.SetColor("_EmissionColor", color * Mathf.LinearToGammaSpace(0f));
            //FairWeather.material.SetColor("_EmissionColor", color);
            yield return new WaitForSeconds(41f);
            BlendClouds = true;
            canChangeCloudColor = false;
        }

        // Day
        if (GameVariables.Hour >= 8 && GameVariables.Hour <= 19)
        {
            GameVariables.isNight = false;
            GameVariables.isSunrise = false;
            GameVariables.isDay = true;
            GameVariables.isSunset = false;
            Debug.Log("Becoming day...");
            timer = 0;
            canChangeCloudColor = true;
            //FairWeather.material.SetColor("_EmissiveColor", Color.black * -10f);
            //FairWeather.material.SetFloat("_EmissiveIntensity", -10f);
            //FairWeather.material.SetColor("_Color", Color.black);
            //FairWeather.material.color = new Color(0.3301887f, 0.3301887f, 0.3301887f, 1);
            //Color color = new Color(0.75f, 0.75f, 0.75f, 1); // day white emission color
            //FairWeather.material.SetColor("_BaseColor", color);
            //FairWeather.material.SetColor("_EmissionColor", Color.white * Mathf.LinearToGammaSpace(10f));
            //FairWeather.material.SetColor("_EmissionColor", color);
            yield return new WaitForSeconds(41f);
            BlendClouds = true;
            canChangeCloudColor = false;
        }

        // Sunset
        if (GameVariables.Hour == 20)
        {
            GameVariables.isNight = false;
            GameVariables.isSunrise = false;
            GameVariables.isDay = false;
            GameVariables.isSunset = true;

            timer = 0;
            canChangeCloudColor = true;
            //FairWeather.material.color = new Color(0.7f, 0.7f, 0.7f, 1);
            //FairWeather.material.color = new Color(0.46f, 0.3663397f, 0.2900054f, 1);  // orange albedo color
            //FairWeather.material.color = new Color(0.5f, 0.2915989f, 0.2051887f, 1);  // orange albedo color
            //Color color = new Color(0.5f, 0.2915989f, 0.2051887f, 1); // orange emission color
            //FairWeather.material.SetColor("_BaseColor", color);
            //FairWeather.material.SetColor("_EmissionColor", color * Mathf.LinearToGammaSpace(0f));
            //FairWeather.material.SetColor("_EmissionColor", color);
            yield return new WaitForSeconds(41f);
            BlendClouds = true;
            canChangeCloudColor = false;
        }

        //yield return new WaitForSeconds(73f);
        //canChangeFogColor = false;
        //checkingCloudColor = false;
    }

    IEnumerator Storm(int duration)
    {
        canFade = true;

        desiredAlphaFair = 0;
        desiredAlphaStorm = 1;


        yield return new WaitForSeconds(2.0f);

        Thunder.Play();

        yield return new WaitForSeconds(5.0f);

        GameVariables.RadiationStorm = true;

        StartCoroutine(SunFadeOut());

        windZone.windTurbulence = 1.0f;
        windZone.windPulseMagnitude = 3.0f;
        windZone.windPulseFrequency = 1.5f;

        Overcast.SetActive(true);

        Dust.Play();

        Wind.Play();

        desiredAlphaOvercast = 0.8f;
        //esiredSunIntensity = 0.0f;

        desiredAlphaStorm2 = 1;
        desiredAlphaStorm3 = 1;

        desiredAlphaStorm = 1.1f;

        yield return new WaitForSeconds(duration-10);

        desiredAlphaStorm2 = 0;
        desiredAlphaStorm3 = 0;

        yield return new WaitForSeconds(10);
        StartCoroutine(FadeThunder());

        desiredAlphaFair = 1;
        desiredAlphaStorm = 0;
        desiredAlphaOvercast = 0;

        //desiredSunIntensity = 0.9f;

        windZone.windTurbulence = 0.5f;
        windZone.windPulseMagnitude = 2f;
        windZone.windPulseFrequency = 1.1f;

        yield return new WaitForSeconds(5.0f);

        StartCoroutine(FadeWind());

        Dust.Stop();

        windZone.windTurbulence = 0.1f;
        windZone.windPulseMagnitude = 1f;
        windZone.windPulseFrequency = 0.8f;

        //StartCoroutine(SunFadeIn());

        GameVariables.RadiationStorm = false;

        yield return new WaitForSeconds(20.0f);

        Overcast.SetActive(false);

        canFade = false;

        fadeSpeed = 0.05f;

        canSwitchWeather = true;
    }

    IEnumerator FadeThunder()
    {
        float t = MaxFadeVolume;
        while (t > 0)
        {
            t -= Time.deltaTime / 50;
            Thunder.volume = t;
            yield return new WaitForSeconds(0);
        }

        Thunder.volume = 0.0f;
        Thunder.Stop();
        Thunder.volume = 1.0f;
    }

    IEnumerator FadeWind()
    {
        float t = MaxFadeVolume;
        while (t > 0)
        {
            t -= Time.deltaTime / 50;
            Wind.volume = t;
            yield return new WaitForSeconds(0);
        }

        Wind.volume = 0.0f;
        Wind.Stop();
        Wind.volume = 0.35f;

    }

    IEnumerator SunFadeOut()
    {
        float duration = 20.0f;//time you want it to run
        float interval = 0.1f;//interval time between iterations of while loop
        //Sun.intensity = 0.0f;
        while (duration >= 0.0f && Sun.intensity > 0.05f)
        {
            Sun.intensity -= 0.01f;

            duration -= interval;
            yield return new WaitForSeconds(interval);//the coroutine will wait for 0.1 secs
        }
    }

    IEnumerator SunFadeIn()
    {
        float duration = 20.0f;//time you want it to run
        float interval = 1.0f;//interval time between iterations of while loop
        //Sun.intensity = 0.0f;
        while (duration >= 0.0f && Sun.intensity < 0.5f)
        {
            Sun.intensity += 0.0005f;

            duration -= interval;
            yield return new WaitForSeconds(interval);//the coroutine will wait for 0.1 secs
        }
    }
    IEnumerator StarsFadeOut()
    {
        canFadeStars = true;
        desiredAlphaStars = 0;
        yield return new WaitForSeconds(30);
        canFadeStars = false;
    }

    IEnumerator StarsFadeIn()
    {
        canFadeStars = true;
        desiredAlphaStars = 0.5f;
        yield return new WaitForSeconds(30);
        canFadeStars = false;
    }

    IEnumerator DoAThingOverTime(float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            Color c = FairWeather.material.color;
            c.a = c.a - 0.0005f;
            FairWeather.material.color = c;
            yield return null;

        }
    }

}





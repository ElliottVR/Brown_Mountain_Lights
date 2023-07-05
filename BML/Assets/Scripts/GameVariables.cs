using UnityEngine;
using System.Collections;
using System;

[Serializable]
public static class GameVariables {

    public static bool EnterLights;
    
   
     // copied from AfterWar
     
    public static bool SpawnPlayer = true;

    public static float Health = 100f;
    public static float Food = 100f;
    public static float Water = 100f;

    public static float CurrentTime;
    public static float ConstantTime;
    public static DateTime RealTime;
    public static float timeScale;

    public static string AmPm;
    public static int Hour;
    public static int Minute;
    public static int Second;
    public static int Day = 0;

    public static TimeSpan Time = new TimeSpan(Day, Hour, Minute, Second);

    public static bool isNight;
    public static bool isSunrise;
    public static bool isDay;
    public static bool isSunset;

    public static bool isIndoors;

    public static bool RadiationStorm = false;

    public static float TotalRadiationDose = 0f;
    public static float DoseMillisieverts = 0f;
    public static bool Detector = true;

    public static bool FlashlightVisible = true;
    public static bool FlashlightOn = true;
    public static bool NightVisionOn = false;

    public static int BatteryAmount = 3;

    public static bool FirstStart = true;

    public static bool GameComplete = false;
    public static int DeathsTotal = 0;

    public static bool isTraveling = false;

    

}
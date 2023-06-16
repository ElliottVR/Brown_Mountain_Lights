using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    public float timeScale;
    [Space(10)]
    [Header("Starting Time")]
    public float currentTime;
    public float constantTimer;
    public int minute = 0, hour = 0;
    public int day;
    
    private bool pm = false;
    public bool timeStop = false;
    

    void Start()
    {
        //minute = 0;
        //hour = 8;
        GameVariables.timeScale = timeScale;
        constantTimer += 3600 * hour;
    }

    void Update()
    {
        if (timeStop == false)
        {
            //Debug.Log(GameVariables.Time);
            CalculateTime();
        }
        if (timeStop == true)
        {
            hour = GameVariables.Hour;
            minute = GameVariables.Minute;
        }
        GameVariables.ConstantTime = constantTimer;
    }

    void CalculateTime()
    {
        currentTime += Time.deltaTime * GameVariables.timeScale;
        constantTimer += Time.deltaTime * GameVariables.timeScale;

        if (currentTime >= 60)
        {
            minute++;
            currentTime = 0;
        }
        else if (minute >= 60)
        {
            minute = 0;
            hour++;
        }
        else if(hour < 1)
        {
            hour = 1;
        }
        else if(hour > 24)
        {
            hour = 1;
            GameVariables.Day++;
        }
        if (constantTimer > 86400)
        {
            constantTimer = 0;
        }

        /*
        if (hour > 11)
        {
            pm = true;
        }

        if (hour < 12)
        {
            pm = false;
        }
        */

        if (hour > 11 && hour < 24)
        {
            pm = true;
            GameVariables.AmPm = "PM";
        }
        else
        {
            pm = false;
            GameVariables.AmPm = "AM";
        }

        //Debug.Log(hour);

        GameVariables.Hour = Convert.ToInt32(hour);
        
        GameVariables.Minute = Convert.ToInt32(minute);
        
        GameVariables.Time = new TimeSpan(GameVariables.Hour, GameVariables.Minute, Convert.ToInt32(GameVariables.CurrentTime));
        
    }

    // Gets the time after a certain amount of time. (Ex. what is the time 2 hours from now)
    public TimeSpan AddedTime(TimeSpan length)
    {
        TimeSpan end;
        end = GameVariables.Time;
        end += length;
        return end;
    }

    // Finds the length of tiem between one time and another.
    public TimeSpan Difference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;
        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }
}

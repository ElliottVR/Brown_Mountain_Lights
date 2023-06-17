using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Watch : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay;

    public bool useRealTime = false;

    private int hour;
    private int minute;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GameVariables.Hour >= 13)
        {
            hour = GameVariables.Hour - 12;
        }
        else
        {
            hour = GameVariables.Hour;
        }

        minute = GameVariables.Minute;


        if (useRealTime == false)
        {
            timeDisplay.text = string.Format(hour.ToString("0") + ":" + minute.ToString("00 ") + GameVariables.AmPm);
            //timeDisplay.text = (GameVariables.RealTime.ToString("h"));
        }

        if (useRealTime == true)
        {
            timeDisplay.text = (GameVariables.RealTime.ToString("t"));
        }
    }
}

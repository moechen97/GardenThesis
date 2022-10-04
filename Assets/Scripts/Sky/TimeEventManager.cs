using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class TimeEventManager : MonoBehaviour
{
    [SerializeField] private float dayTimeStart;
    [SerializeField] private float duskTimeStart;
    [SerializeField] private float nightTimeStart;
    //state = 0 = night , state = 1 = day , state = 2 = dusk
    private int state = 0;
    private float currentTime;

    public delegate void TimePeriod();
    public static event TimePeriod DayStart;
    public static event TimePeriod DuskStart;
    public static event TimePeriod NightStart;

    void Start()
    {
        currentTime = ProceduralSky.TimeofDay;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = ProceduralSky.TimeofDay;
        
        if (currentTime > dayTimeStart && state == 0 && currentTime<=duskTimeStart)
        {
            if (DayStart != null)
            {
                
                DayStart();
            }
            Debug.Log("Day");
            state = 1;
        }
        else if (currentTime > duskTimeStart && state == 1 && currentTime<=nightTimeStart)
        {
            if (DuskStart != null)
            {
                DuskStart();
            }
            Debug.Log("Dusk");
            state = 2;
        }
        else if (currentTime > nightTimeStart && state == 2)
        {
            if (NightStart != null)
            {
                NightStart();
            }
            Debug.Log("Night");
            state = 0;
        }
    }
}

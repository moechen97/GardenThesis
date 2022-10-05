using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedManager : MonoBehaviour
{
    [SerializeField] public float speedDefault = 1.0F;
    [HideInInspector] public float speed;
    [SerializeField] ProceduralSky proceduralSky;
    [SerializeField] Scrollbar scrollbar;
    private float lastSpeed;

    private void Start()
    {
        speed = speedDefault;
        lastSpeed = speed;
        proceduralSky.SetTimePassSpeed(speed);
    }

    private void Update()
    {
        speed = (scrollbar.value * (speedDefault * 4F)) + speedDefault;
        if(lastSpeed != speed)
        {
            proceduralSky.SetTimePassSpeed(speed);
        }
        lastSpeed = speed;
    }
}

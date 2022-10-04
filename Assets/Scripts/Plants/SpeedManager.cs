using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedManager : MonoBehaviour
{
    [SerializeField] public float speed = 1.0F;
    [SerializeField] ProceduralSky proceduralSky;
    [SerializeField] Scrollbar scrollbar;
    private float lastSpeed;

    private void Start()
    {
        lastSpeed = speed;
        proceduralSky.SetTimePassSpeed(speed);
    }

    private void Update()
    {
        speed = (scrollbar.value * 4F) + 1F;
        if(lastSpeed != speed)
        {
            proceduralSky.SetTimePassSpeed(speed);
        }
        lastSpeed = speed;
    }
}

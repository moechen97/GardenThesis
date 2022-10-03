using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthManager : MonoBehaviour
{
    [SerializeField] ProceduralSky proceduralSky;
    public float growthFactor = 1F;

    private void Start()
    {
        proceduralSky.SetTimePassSpeed(growthFactor);
    }
}

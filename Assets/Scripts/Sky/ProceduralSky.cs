using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ProceduralSky : MonoBehaviour
{
    //References
    [SerializeField] private LightingPreset _lightingPreset;
    //Variables
    [SerializeField,Range(0,24)] private float TimeOfDay;
    [SerializeField] private float timePassSpeed;
    

    void UpdateLighting(float timePercent)
    {
        Color topColor = _lightingPreset.TopColor.Evaluate(timePercent);
        Color bottomColor = _lightingPreset.BottomColor.Evaluate(timePercent);
        RenderSettings.skybox.SetColor("_Top_Color",topColor);
        RenderSettings.skybox.SetColor("_Bottom_Color",bottomColor);
    }

    void Update()
    {
        if (_lightingPreset == null)
            return;

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime * timePassSpeed;
            TimeOfDay %= 24;//clamp between 0-24
            UpdateLighting(TimeOfDay/24f);
        }
        else
        {
            UpdateLighting(TimeOfDay/24f);
        }
        
    }
    private void OnValidate()
    {
        
    }
}

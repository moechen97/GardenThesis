using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ProceduralSky : MonoBehaviour
{
    //References
    [SerializeField] private LightingPreset _lightingPreset;
    [SerializeField] private Light DirectionLight;
    [SerializeField] private GameObject Ground;
    
    //Variables
    [SerializeField,Range(0,24)] private float TimeOfDay;
    [SerializeField] private float timePassSpeed;
    

    void UpdateLighting(float timePercent)
    {
        Color topColor = _lightingPreset.TopColor.Evaluate(timePercent);
        Color bottomColor = _lightingPreset.BottomColor.Evaluate(timePercent);
        RenderSettings.skybox.SetColor("_Top_Color",topColor);
        RenderSettings.skybox.SetColor("_Bottom_Color",bottomColor);
        if (DirectionLight != null)
        {
            DirectionLight.color = _lightingPreset.LightColor.Evaluate(timePercent);
            DirectionLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, -45f, 0f));
        }

        if (Ground != null)
        {
            Ground.GetComponent<Renderer>().material.SetColor("_GradientBottomColor",_lightingPreset.GroundBottomColor.Evaluate(timePercent));
        }
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
        if (DirectionLight != null)
            return;
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionLight = light;
                    return;
                }
            }
        }
    }

    public void SetTimePassSpeed(float speed)
    {
        timePassSpeed = speed;
    }
}

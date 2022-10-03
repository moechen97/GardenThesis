using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Lighting Preset")]
public class LightingPreset : ScriptableObject
{
    public Gradient TopColor;
    public Gradient BottomColor;
    public Gradient LightColor;
    public Gradient GroundBottomColor;
}

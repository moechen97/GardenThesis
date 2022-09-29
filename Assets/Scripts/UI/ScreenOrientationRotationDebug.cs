using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOrientationRotationDebug : MonoBehaviour
{
    public void ChangeOrientation()
    {
        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        
        Debug.Log(Screen.orientation);
    }

    private void Update()
    {
        Debug.Log(Input.deviceOrientation);
    }
}

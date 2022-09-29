using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapePortrait : MonoBehaviour
{
    [SerializeField] private GameObject landscapeCanvas;

    [SerializeField] private GameObject portraitCanvas;
    private bool isPortrat = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.deviceOrientation == DeviceOrientation.Portrait ||
            Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {
            switch (isPortrat)
            {
                case true:
                    return;
                case false:
                    landscapeCanvas.SetActive(false);
                    portraitCanvas.SetActive(true);
                    isPortrat = true;
                    break;
            }
        }
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeRight ||
            Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            switch (isPortrat)
            {
                case false:
                    return;
                case true:
                    landscapeCanvas.SetActive(true);
                    portraitCanvas.SetActive(false);
                    isPortrat = false;
                    break;
            }
        }
    }
}

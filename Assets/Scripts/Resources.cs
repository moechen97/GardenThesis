using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Planting {
    public class Resources : MonoBehaviour
    {
        public float FillSpeed = 0.125F;
        private static Slider slider;
        public static float resourcesUsed = 0;
        private static bool incrementProgress = true;
        private GameObject fillBar;
        private static float sliderValue;
        // Start is called before the first frame update
        void Start()
        {
            slider = GameObject.FindGameObjectWithTag("ResourceBar").GetComponent<Slider>();
            slider.value = 0.0F;
            sliderValue = 0.0F;
            fillBar = slider.fillRect.transform.gameObject;
            fillBar.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (sliderValue < resourcesUsed)
            {
                sliderValue += FillSpeed * Time.deltaTime;
            }
            else if (sliderValue > resourcesUsed)
            {
                sliderValue -= FillSpeed * Time.deltaTime;
            }
            if (Mathf.Abs(sliderValue - resourcesUsed) < 0.0125F)//Mathf.Approximately(sliderValue, resourcesUsed) || )
            {
                sliderValue = resourcesUsed;
            }

            slider.value = sliderValue;

            if (!slider.gameObject.activeSelf)
            {
                return;
            }
            
            if(resourcesUsed == 0.0F)
            {
                fillBar.SetActive(false);
            }
            else
            {
                fillBar.SetActive(true);
            }
        }

        public static void IncrementProgress(float newProgress)
        {
            resourcesUsed += newProgress;
            resourcesUsed = float.Parse(resourcesUsed.ToString("F2"));
            incrementProgress = true;
        }

        public static void DecrementProgress(float newProgress)
        {
            resourcesUsed -= newProgress;
            resourcesUsed = float.Parse(resourcesUsed.ToString("F2"));
            incrementProgress = false;
        }
        public static float GetResourcesUsed()
        {
            return resourcesUsed;
        }
    }
}
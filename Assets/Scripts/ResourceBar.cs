using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Planting {
    public class ResourceBar : MonoBehaviour
    {
        public float FillSpeed = 0.25F;
        private static Slider slider;
        private static float resourcesUsed = 0;
        private static bool incrementProgress = true;
        private GameObject fillBar;
        // Start is called before the first frame update
        void Start()
        {
            slider = GetComponent<Slider>();
            slider.value = 0.0F;
            fillBar = slider.fillRect.transform.gameObject;
            fillBar.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (incrementProgress && slider.value < resourcesUsed)
            {
                slider.value += FillSpeed * Time.deltaTime;
            }
            else if (!incrementProgress && slider.value > resourcesUsed)
            {
                slider.value -= FillSpeed * Time.deltaTime;
            }
            if (Mathf.Approximately(slider.value, resourcesUsed))
            {
                slider.value = resourcesUsed;
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
            resourcesUsed = Mathf.Round((slider.value + newProgress) * 100.0F) * 0.01F;
            incrementProgress = true;
        }

        public static void DecrementProgress(float newProgress)
        {
            resourcesUsed = Mathf.Round((slider.value - newProgress) * 100.0F) * 0.01F;
            incrementProgress = false;
        }

        public static float GetResourcesUsed()
        {
            return resourcesUsed;
        }
    }
}
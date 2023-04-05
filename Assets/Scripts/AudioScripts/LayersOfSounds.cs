using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Planting
{
    public class LayersOfSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource layer1;
        [SerializeField] private AudioSource layer2;
        [SerializeField] private AudioSource layer_red;
        [SerializeField] private AudioSource layer_blue;

        private float hueValue = 0;
        private int plantCount = 0;
        private bool layer1active = false;
        private bool layer2active = false;
        private bool layerBlueActive = false;
        private bool layerRedActive = false;

        private void Update()
        {
            hueValue = Resources.GetHueValue();
            plantCount = PlantManager.allPlants.Count;

            if (plantCount > 6)
            {
                layer1active = true;
                StopCoroutine(VolumeDown(layer1));
                StartCoroutine(VolumeUp(layer1));
            }
            else if (plantCount < 3 && layer1active)
            {
                layer1active = false;
                StopCoroutine(VolumeUp(layer1));
                StartCoroutine(VolumeDown(layer1));
            }
            
            if (plantCount > 13)
            {
                layer2active = true;
                StopCoroutine(VolumeDown(layer2));
                StartCoroutine(VolumeUp(layer2));
            }
            else if (plantCount < 10 && layer2active)
            {
                layer2active = false;
                StopCoroutine(VolumeUp(layer2));
                StartCoroutine(VolumeDown(layer2));
            }
            
            if (hueValue > 0.5f)
            {
                layerBlueActive = true;
                StopCoroutine(VolumeDown(layer_blue));
                StartCoroutine(VolumeUp(layer_blue));
            }
            else if (hueValue < 0.3f && layerBlueActive)
            {
                layerBlueActive = false;
                StopCoroutine(VolumeUp(layer_blue));
                StartCoroutine(VolumeDown(layer_blue));
            }
            
            if (hueValue < -0.5f)
            {
                layerRedActive = true;
                StopCoroutine(VolumeDown(layer_red));
                StartCoroutine(VolumeUp(layer_red));
            }
            else if (hueValue > -0.3f && layerRedActive)
            {
                layerRedActive = false;
                StopCoroutine(VolumeUp(layer_red));
                StartCoroutine(VolumeDown(layer_red));
            }
            
        }

        IEnumerator VolumeUp(AudioSource _audio)
        {
            
            _audio.DOFade(0.45f, 1.5f);
            yield return null;
        }
        
        IEnumerator VolumeDown(AudioSource _audio)
        {
            
            _audio.DOFade(0, 1.5f);
            yield return null;
        }
        
        
    }

}

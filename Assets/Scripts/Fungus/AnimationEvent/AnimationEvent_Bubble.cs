using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Planting
{
    public class AnimationEvent_Bubble : AnimationEvent_Plants
    {
        public override void InteractSound()
        {
            int ran = Random.Range(0, interactAudios.Length);
            _audioSource.PlayOneShot(interactAudios[ran]);
        }
    }
}

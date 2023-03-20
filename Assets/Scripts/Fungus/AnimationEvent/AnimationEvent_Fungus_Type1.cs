using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Planting
{
    public class AnimationEvent_Fungus_Type1 : AnimationEvent_Plants
    {

        [SerializeField] GameObject BreathParticle;
        [SerializeField] Transform particleInstantiatePosition;
        [SerializeField] Fungus_MaterialChange MaterialChange;


        public void Breath1()
        {
            _audioSource.pitch = 1;
            int num = Random.Range(0, breatheAudios.Length);
            _audioSource.PlayOneShot(breatheAudios[num]);
            Instantiate(BreathParticle, particleInstantiatePosition.position, quaternion.identity);
            MaterialChange.BreathIn();
        }

        public void Breath2()
        {

            Instantiate(BreathParticle, particleInstantiatePosition.position, quaternion.identity);
            MaterialChange.BreathIn();
        }
        
        public override void InteractSound()
        { 
            _audioSource.Stop();
            float aliveTime = _plant.ReturnAliveTime();
            float currentLifeTime = _plant.ReturnCurrentLife();
            float extent = currentLifeTime / (aliveTime*2);
            // if (extent is > 0f and < 0.34f)
            // {
            //    soundPitch = randomPitch;
            // }
            // else if (extent is >= 0.34f and < 0.68f)
            // {
            //     soundPitch = randomPitch - 0.03f ;
            // }
            // else
            // {
            //     soundPitch = randomPitch - 0.06f ;
            // }
            if (extent > 1f)
            {
                extent = 1f;
            }
            soundPitch = randomPitch - extent * 0.2f;
            // Debug.Log("randomPitch" + randomPitch + "soundPitch" + soundPitch);
            //int num = Random.Range(0, interactAudios.Length);
            _audioSource.pitch = soundPitch;
            Debug.Log("RP"+extent);
            _audioSource.PlayOneShot(interactAudios[randomSound]);
        }

        public void BreathOut()
        {
            MaterialChange.BreathOut();
        }


    }
}

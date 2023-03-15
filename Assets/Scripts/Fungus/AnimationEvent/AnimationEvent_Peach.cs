using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

namespace Planting
{
    public class AnimationEvent_Peach : AnimationEvent_Plants
    {


        [SerializeField] private GameObject emitParticle;
        [SerializeField] private Transform emitPosition;


        private int interactnum;
        private float soundpitch;

        override public void Start()
        {
            base.Start();
            interactnum = Random.Range(0, interactAudios.Length);
        }


        public override void GrowSound()
        {
            //_audioSource.pitch = Random.Range(0.9f, 1.1f);
            //int num = Random.Range(0, growthAudios.Length);
            //_audioSource.PlayOneShot(growthAudios[num]);
        }
        
        public override void InteractSound()
        {
            float aliveTime = _plant.ReturnAliveTime();
            float currentLifeTime = _plant.ReturnCurrentLife();
            float extent = currentLifeTime / (aliveTime*2);
            if (extent is > 0f and < 0.35f)
            {
                soundpitch = 1f;
            }
            else if (extent is >= 0.35f and < 0.65f)
            {
                soundpitch = 0.8f ;
            }
            else if (extent is >= 0.65f and < 0.95f)
            {
                soundpitch = 0.6f ;
            }
            else
            {
                soundpitch = 0.5f ;
            }
            
           
            // Debug.Log("randomPitch" + randomPitch + "soundPitch" + soundPitch);
            //int num = Random.Range(0, interactAudios.Length);
            _audioSource.pitch = soundpitch;
            Debug.Log("RP"+extent);
            _audioSource.PlayOneShot(interactAudios[randomSound]);
        }

        public void EmitParticle()
        {
            Instantiate(emitParticle, emitPosition.position, Quaternion.identity, emitPosition);
        }



        
    }
}

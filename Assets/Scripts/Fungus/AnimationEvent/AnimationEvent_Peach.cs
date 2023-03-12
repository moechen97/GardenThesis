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

        public void EmitParticle()
        {
            Instantiate(emitParticle, emitPosition.position, Quaternion.identity, emitPosition);
        }



        
    }
}

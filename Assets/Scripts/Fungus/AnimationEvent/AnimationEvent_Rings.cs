using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Planting
{
    public class AnimationEvent_Rings : AnimationEvent_Plants
    {

        [SerializeField] private GameObject emitParticle;
        [SerializeField] private Transform emitPosition;

        [SerializeField] private Animator ringsAnimator;
        private int interactNum;


        override public void Start()
        {
            base.Start();
            int interactNum = Random.Range(0, interactAudios.Length);
        }

        public void DoRandomBreath()
        {
            float i = Random.Range(0f, 1f);
            if (i > 0.5f)
            {
                ringsAnimator.SetBool("CanBreathe", true);
                StartCoroutine(DisableCanBreathe());
            }
        }

        IEnumerator DisableCanBreathe()
        {
            yield return new WaitForSeconds(2f);
            ringsAnimator.SetBool("CanBreathe", false);
        }

        public override void InteractSound()
        {
            _audioSource.PlayOneShot(interactAudios[randomSound]);
        }

        public void EmitParticle()
        {
            Instantiate(emitParticle, emitPosition.position, Quaternion.identity);
        }


    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Planting
{
    public abstract class AnimationEvent_Plants : MonoBehaviour
    {
        [SerializeField] private Fungus_MaterialChange[] _materials;
        [SerializeField] protected Plant _plant;
        [SerializeField] protected AudioSource _audioSource;
        [SerializeField] AudioClip[] bloomSounds;
        [SerializeField] protected AudioClip[] growthAudios;
        [SerializeField] protected AudioClip[] breatheAudios;
        [SerializeField] protected AudioClip[] interactAudios;
        [SerializeField] private AudioClip[] witheredAudios;

        [SerializeField] private GameObject dieParticle;
        [SerializeField] private Transform dieParticlePosition;
        [SerializeField] private Plant_StateControl _stateControl;

        private float randomPitch;
        private float soundPitch;
        protected int randomSound;
        
        public virtual void Start()
        {
            randomPitch = Random.Range(1.0f, 1.1f);
            randomSound = Random.Range(0, interactAudios.Length);
            //Debug.Log(randomPitch);
            
        }

        public virtual void GrowSound()
        {
            int num = Random.Range(0, growthAudios.Length);
            _audioSource.PlayOneShot(growthAudios[num]);
        }

        public void BreathSound()
        {
            int num = Random.Range(0, breatheAudios.Length);
            _audioSource.PlayOneShot(breatheAudios[num]);
        }

        public void WitheredSound()
        {
            int num = Random.Range(0, witheredAudios.Length);
            _audioSource.PlayOneShot(witheredAudios[num]);
        }

        public virtual void InteractSound()
        { 
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

        public void Withered()
        {
            foreach (var material in _materials)
            {
                material.MaterialWithered();
            }
        }

        public void BloomSound()
        {
            int num = Random.Range(0, bloomSounds.Length);
            _audioSource.PlayOneShot(bloomSounds[num]);
        }

        public void Die()
        {
            foreach (var material in _materials)
            {
                material.Die();
            }
        }

        public void Dim()
        {
            foreach (var material in _materials)
            {
                material.Dim();
            }
        }

        public void Glow()
        {
            foreach (var material in _materials)
            {
                material.Glow();
            }
        }

        public void EmitDieParticle()
        {
            Instantiate(dieParticle, dieParticlePosition.position, Quaternion.identity);
        }

        public void CanInteract()
        {
            _stateControl.CanbeInteract();
        }

        public void CannotBeInteract()
        {
            _stateControl.CannotbeInteract();
        }

        public void InteractLightUp()
        {
            foreach (var material in _materials)
            {
                material.BreathOut();
            }
        }

        public void InteractDim()
        {
            foreach (var material in _materials)
            {
                material.BreathIn();
            }
        }

        public void InteractAnimationStart()
        {
            _stateControl.IsInteracting();
        }

        public void InteractAnimationEnd()
        {
            _stateControl.DoneInteracting();
        }
    }
}

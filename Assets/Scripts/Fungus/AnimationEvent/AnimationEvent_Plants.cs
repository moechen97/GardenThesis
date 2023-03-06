using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationEvent_Plants : MonoBehaviour
{
    [SerializeField] private Fungus_MaterialChange[] _materials;
    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] AudioClip[] bloomSounds;
    [SerializeField] protected AudioClip[] growthAudios;
    [SerializeField] protected AudioClip[] breatheAudios;
    [SerializeField] protected AudioClip[] interactAudios;
    [SerializeField] private AudioClip[] witheredAudios;
    
    [SerializeField] private GameObject dieParticle;
    [SerializeField] private Transform dieParticlePosition;
    [SerializeField] private Plant_StateControl _stateControl;
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
        int num = Random.Range(0, interactAudios.Length);
        _audioSource.PlayOneShot(interactAudios[num]);
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

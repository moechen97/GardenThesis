using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimationEvent_Bubble : MonoBehaviour
{
    [SerializeField] private Fungus_MaterialChange[] _materials;

    [SerializeField] private AudioSource Bubble_AudioSource;

    [SerializeField] private AudioClip[] bloomAudios;
    [SerializeField] private GameObject dieParticle;
    [SerializeField] private Transform dieParticlePosition;
    [SerializeField] private Plant_StateControl _stateControl;

    public void Bloom()
    {
        int num = Random.Range(0, bloomAudios.Length);
        Bubble_AudioSource.PlayOneShot(bloomAudios[num]);
    }

    void Breath()
    {
        int num = Random.Range(0, bloomAudios.Length);
        Bubble_AudioSource.PlayOneShot(bloomAudios[num]);
    }
    
    public void Withered()
    {
        foreach (var material in _materials)
        {
            material.MaterialWithered();
        }
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

    public void CannotbeInteract()
    {
        _stateControl.CannotbeInteract();
    }

    public void InteractLightup()
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
}

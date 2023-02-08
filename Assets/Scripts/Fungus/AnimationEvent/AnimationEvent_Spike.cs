using System.Collections;
using System.Collections.Generic;
using Planting;
using UnityEngine;

public class AnimationEvent_Spike : MonoBehaviour
{
    [SerializeField] private Fungus_MaterialChange[] _materialChange;
    [SerializeField] private AudioClip growSound;
    [SerializeField] private AudioClip breatheSound;
    [SerializeField] private AudioClip witheredSound;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Spike _spike;

    private GameObject spikeFlower;
    
    
    public void GrowSound()
    {
        if (_spike.generation_seed == 0)
        {
            float generation = _spike.generation;
            _audioSource.pitch += generation * 0.1f;
            _audioSource.PlayOneShot(growSound);
        }
        
    }
    
    public void BreathSound()
    {
        if (_spike.generation_seed == 0)
        {
            _audioSource.PlayOneShot(breatheSound);
        }
        
    }
    
    public void WitheredSound()
    {
        if (_spike.generation_seed == 0)
        {
            _audioSource.PlayOneShot(witheredSound);
        }
        
    }
    
    public void Withered()
    {

        foreach (var material in _materialChange)
        {
            material.MaterialWithered();   
        }
       
    }

    public void Dead()
    {
        foreach (var material in _materialChange)
        {
            material.Die();
        }
    }

    public void Glow()
    {
        foreach (var material in _materialChange)
        {
            material.Glow();
        }
    }
    
    public void Dim()
    {
        foreach (var material in _materialChange)
        {
            material.Dim();
        }
    }
}

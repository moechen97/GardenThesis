using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Lotus : MonoBehaviour
{
    [SerializeField] Fungus_MaterialChange[] materials;
    [SerializeField] private AudioSource lotus_audiosource;
    [SerializeField] private AudioClip[] bloomSound;
    [SerializeField] private AudioClip[] growSound;
    [SerializeField] private AudioClip[] breathSound;
    [SerializeField] private AudioClip[] interactSound;
    [SerializeField] private AudioClip[] witheredSound;
    [SerializeField] private Plant_StateControl _stateControl;

    public void BloomSound()
    {
        int num = Random.Range(0, bloomSound.Length);
        lotus_audiosource.PlayOneShot(bloomSound[num]);
    }
    
    public void GrowSound()
    {
        int num = Random.Range(0, growSound.Length);
        lotus_audiosource.PlayOneShot(growSound[num]);
    }
    
    public void BreathSound()
    {
        int num = Random.Range(0, breathSound.Length);
        lotus_audiosource.PlayOneShot(breathSound[num]);
    }
    
    public void InteractSound()
    {
        int num = Random.Range(0, interactSound.Length);
        lotus_audiosource.PlayOneShot(interactSound[num]);
    }
    
    public void WitheredSound()
    {
        int num = Random.Range(0, witheredSound.Length);
        lotus_audiosource.PlayOneShot(witheredSound[num]);
    }
    public void Withered()
    {
        foreach (var material in materials)
        {
            material.MaterialWithered();
        }
    }
    
    public void Die()
    {
        foreach (var material in materials)
        {
            material.Die();
        }
    }
    
    public void Glow()
    {
        foreach (var material in materials)
        {
            material.Glow();
        }
    }
    
    public void Dim()
    {
        foreach (var material in materials)
        {
            material.Dim();
        }
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
        foreach (var material in materials)
        {
            material.BreathOut();
        }
    }
    
    public void InteractDim()
    {
        foreach (var material in materials)
        {
            material.BreathIn();
        }
    }
    
    
}

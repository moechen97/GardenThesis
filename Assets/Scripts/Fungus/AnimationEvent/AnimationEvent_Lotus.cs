using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Lotus : MonoBehaviour
{
    [SerializeField] Fungus_MaterialChange[] materials;
    [SerializeField] private AudioSource lotus_audiosource;
    [SerializeField] private AudioClip bloom;
    [SerializeField] private Plant_StateControl _stateControl;

    public void Bloom()
    {
        lotus_audiosource.PlayOneShot(bloom);
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

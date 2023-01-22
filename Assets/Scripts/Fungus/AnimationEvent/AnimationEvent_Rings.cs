using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Rings : MonoBehaviour
{
    [SerializeField] Fungus_MaterialChange[] materials;
    [SerializeField] private AudioSource ring_audiosource;
    [SerializeField] private AudioClip grow;
    [SerializeField] private AudioClip bloom;
    [SerializeField] private AudioClip breath;
    [SerializeField] private AudioClip withered;
    [SerializeField] private GameObject emitParticle;
    [SerializeField] private Transform emitPosition;
    [SerializeField] private Plant_StateControl _stateControl;

    public void Grow()
    {
        ring_audiosource.PlayOneShot(grow);
    }

    public void Bloom()
    {
        ring_audiosource.PlayOneShot(bloom);
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

    public void Breath()
    {
        ring_audiosource.PlayOneShot(breath);
    }
    public void Withered()
    {
        foreach (var material in materials)
        {
            material.MaterialWithered();
        }
        ring_audiosource.PlayOneShot(withered);
    }
    
    public void Die()
    {
        foreach (var material in materials)
        {
            material.Die();
        }
    }

    public void EmitParticle()
    {
        Instantiate(emitParticle, emitPosition.position, Quaternion.identity);
    }
    
    public void CanInteract()
    {
        _stateControl.CanbeInteract();
    }

    public void CannotbeInteract()
    {
        _stateControl.CannotbeInteract();
    }
}

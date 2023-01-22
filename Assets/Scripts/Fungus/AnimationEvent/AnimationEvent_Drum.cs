using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Drum : MonoBehaviour
{
    [SerializeField] Fungus_MaterialChange[] materials;
    [SerializeField] private AudioSource drum_audiosource;
    [SerializeField] private AudioClip bloom;
    [SerializeField] private AudioClip breath;
    [SerializeField] private GameObject emitParticle;
    [SerializeField] private Transform emitPosition;
    [SerializeField] private Plant_StateControl _stateControl;

    public void Bloom()
    {
        drum_audiosource.PlayOneShot(bloom);
    }

    public void Breath()
    {
        drum_audiosource.PlayOneShot(breath);
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

    public void Emit()
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

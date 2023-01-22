using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationEvent_Peach : MonoBehaviour
{
    [SerializeField] Fungus_MaterialChange[] MaterialChanges;
    [SerializeField] private AudioSource peach_audiosource;
    [SerializeField] private AudioClip grow;
    [SerializeField] private AudioClip fullygrow;
    [SerializeField] private AudioClip bloom;
    [SerializeField] private AudioClip withered;
    [SerializeField] private AudioClip killed;
    
    [SerializeField] private GameObject emitParticle;
    [SerializeField] private Transform emitPosition;

    [SerializeField] private Plant_StateControl _stateControl;
    
    public void GrowColorChange()
    {
        MaterialPropertyBlock _shadowPropertyBlock = new MaterialPropertyBlock();
        
        
    }

    public void Glow()
    {
        foreach (var materialChange in MaterialChanges)
        {
            materialChange.Glow();
        }
    }

    public void Dim()
    {
        foreach (var materialChange in MaterialChanges)
        {
            materialChange.Dim();
        }
    }
    
    public void Grow()
    {
        peach_audiosource.PlayOneShot(grow);
    }

    public void EmitParticle()
    {
        Instantiate(emitParticle, emitPosition.position,Quaternion.identity,emitPosition);
    }

    public void Bloom()
    {
        peach_audiosource.PlayOneShot(bloom);
    }

    public void Breath()
    {
        peach_audiosource.PlayOneShot(fullygrow);
    }
    
    public void Withered()
    {
        foreach (var MaterialChange in MaterialChanges)
        {
            MaterialChange.MaterialWithered();
        }
        peach_audiosource.PlayOneShot(withered);
    }
    
    public void Die()
    {
        foreach (var MaterialChange in MaterialChanges)
        {
            MaterialChange.Die();
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
    
}

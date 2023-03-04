using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class AnimationEvent_Peach : MonoBehaviour
{
    [SerializeField] Fungus_MaterialChange[] MaterialChanges;
    [SerializeField] private AudioSource peach_audiosource;
    [SerializeField] private AudioClip[] grow;
    [SerializeField] private AudioClip[] fullygrow;
    [SerializeField] private AudioClip[] bloom;
    [SerializeField] private AudioClip withered;
    [SerializeField] private AudioClip killed;
    [SerializeField] private AudioClip[] interact;
    
    [SerializeField] private GameObject emitParticle;
    [SerializeField] private Transform emitPosition;

    [SerializeField] private Plant_StateControl _stateControl;
    private int interactnum;
    
    private void Start()
    {
         interactnum = Random.Range(0, interact.Length);
    }


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
        peach_audiosource.pitch = Random.Range(0.9f, 1.1f);
        int num = Random.Range(0, grow.Length);
        peach_audiosource.PlayOneShot(grow[num]);
    }

    public void EmitParticle()
    {
        Instantiate(emitParticle, emitPosition.position,Quaternion.identity,emitPosition);
    }

    public void Bloom()
    {
        peach_audiosource.pitch = Random.Range(0.9f, 1.1f);
        int num = Random.Range(0, bloom.Length);
        peach_audiosource.PlayOneShot(bloom[num]);
    }

    public void Breath()
    {
        peach_audiosource.pitch = Random.Range(0.9f, 1.1f);
        int num = Random.Range(0, fullygrow.Length);
        peach_audiosource.PlayOneShot(fullygrow[num]);
    }

    public void InteractSoundPlay()
    {
        peach_audiosource.pitch = Random.Range(0.9f, 1.1f);
        peach_audiosource.PlayOneShot(interact[interactnum]);
    }
    
    public void Withered()
    {
        foreach (var MaterialChange in MaterialChanges)
        {
            MaterialChange.MaterialWithered();
        }
        //peach_audiosource.PlayOneShot(withered);
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

    public void InteractLightUp()
    {
        foreach (var MaterialChange in MaterialChanges)
        {
            MaterialChange.BreathOut();
        }
    }
    
    public void InteractLightDim()
    {
        foreach (var MaterialChange in MaterialChanges)
        {
            MaterialChange.BreathIn();
        }
    }
}

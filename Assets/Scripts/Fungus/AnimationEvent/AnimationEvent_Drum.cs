using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Drum : MonoBehaviour
{
    [SerializeField] Fungus_MaterialChange[] materials;
    [SerializeField] private AudioSource drum_audiosource;
    [SerializeField] private AudioClip[] growAndBloomSound;
    [SerializeField] private AudioClip[] interactSound;
    [SerializeField] private AudioClip[] breathSound;
    [SerializeField] private AudioClip[] witheredSound;
    [SerializeField] private AudioClip[] particleSound;
    [SerializeField] private GameObject emitParticle;
    [SerializeField] private Transform emitPosition;
    [SerializeField] private Plant_StateControl _stateControl;

    //Drum Play Sounds
    public void GrowandBloomSound()
    {
        int num = Random.Range(0, growAndBloomSound.Length);
        drum_audiosource.PlayOneShot(growAndBloomSound[num]);
    }

    public void BreathSound()
    {
        int num = Random.Range(0, growAndBloomSound.Length);
        drum_audiosource.PlayOneShot(breathSound[num]);
    }
    
    public void InteractSound()
    {
        int num = Random.Range(0, interactSound.Length);
        drum_audiosource.PlayOneShot(interactSound[num]);
    }
    
    public void ParticleSound()
    {
        int num = Random.Range(0, particleSound.Length);
        drum_audiosource.PlayOneShot(particleSound[num]);
    }
    
    public void WitheredSound()
    {
        int num = Random.Range(0, witheredSound.Length);
        drum_audiosource.PlayOneShot(witheredSound[num]);
    }
    
    //Different plant state
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

    public void InteractLightUp()
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

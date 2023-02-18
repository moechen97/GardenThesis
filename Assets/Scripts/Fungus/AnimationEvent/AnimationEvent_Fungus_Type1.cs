using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimationEvent_Fungus_Type1 : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] AudioClip[] bloomSounds;
    [SerializeField] AudioClip[] breathSound;
    [SerializeField] AudioClip[] growthSounds;
    [SerializeField] AudioClip[] witheredSounds;
    [SerializeField] GameObject BreathParticle;
    [SerializeField] Transform particleInstantiatePosition;
    [SerializeField] Fungus_MaterialChange MaterialChange;
    

    public void Breath1()
    {
        int num = Random.Range(0, breathSound.Length);
        _audioSource.PlayOneShot(breathSound[num]);
        Instantiate(BreathParticle, particleInstantiatePosition.position, quaternion.identity);
        MaterialChange.BreathIn();
    }
    public void Breath2()
    {
        
        Instantiate(BreathParticle, particleInstantiatePosition.position, quaternion.identity);
        MaterialChange.BreathIn();
    }

    public void Growth()
    {
        int x = Random.Range(0, growthSounds.Length);
        _audioSource.PlayOneShot(growthSounds[x]);
    }
    
    public void Bloom()
    {
        int x = Random.Range(0, bloomSounds.Length);
        _audioSource.PlayOneShot(bloomSounds[x]);
    }
    
    public void Withered()
    {
        //int x = Random.Range(0, witheredSounds.Length);
        //SoundEffectManager.Instance.PlayOneClip(witheredSounds[x]);
        MaterialChange.MaterialWithered();
    }

    public void Explode()
    {
        MaterialChange.Exploded();
    }

    public void Die()
    {
        MaterialChange.Die();
    }

    public void BreathOut()
    {
        MaterialChange.BreathOut();
    }
    
}

using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimationEvent_Fungus_Type1 : MonoBehaviour
{
    public AudioClip[] bloomSounds;
    public AudioClip[] breathSound;
    public AudioClip[] growthSounds;
    public AudioClip[] witheredSounds;
    public GameObject BreathParticle;
    public Transform particleInstantiatePosition;
    public Fungus_MaterialChange MaterialChange;

    public void Breath1()
    {
        int num = Random.Range(0, breathSound.Length);
        SoundEffectManager.Instance.PlayOneClip(breathSound[num]);
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
        SoundEffectManager.Instance.PlayOneClip(growthSounds[x]);
    }
    
    public void Bloom()
    {
        int x = Random.Range(0, bloomSounds.Length);
        SoundEffectManager.Instance.PlayOneClip(bloomSounds[x]);
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimationEvent_Bubble : MonoBehaviour
{
    [SerializeField] private Fungus_MaterialChange[] _materials;

    [SerializeField] private AudioSource Bubble_AudioSource;

    [SerializeField] private AudioClip[] bloomAudios;

    public void Bloom()
    {
        int num = Random.Range(0, bloomAudios.Length);
        Bubble_AudioSource.PlayOneShot(bloomAudios[num]);
    }

    void Breath()
    {
        int num = Random.Range(0, bloomAudios.Length);
        Bubble_AudioSource.PlayOneShot(bloomAudios[num]);
    }
    
    public void Withered()
    {
        foreach (var material in _materials)
        {
            material.MaterialWithered();
        }
    }
    
    public void Die()
    {
        foreach (var material in _materials)
        {
            material.Die();
        }
    }
}

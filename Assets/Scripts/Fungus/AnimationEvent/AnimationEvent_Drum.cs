using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Drum : MonoBehaviour
{
    [SerializeField] Fungus_MaterialChange[] materials;
    [SerializeField] private AudioSource drum_audiosource;
    [SerializeField] private AudioClip bloom;
    [SerializeField] private AudioClip breath;

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
}

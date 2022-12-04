using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Lotus : MonoBehaviour
{
    [SerializeField] Fungus_MaterialChange[] materials;
    [SerializeField] private AudioSource lotus_audiosource;
    [SerializeField] private AudioClip bloom;

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
}

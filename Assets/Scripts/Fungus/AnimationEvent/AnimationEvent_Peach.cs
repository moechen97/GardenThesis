using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Peach : MonoBehaviour
{
    [SerializeField] Fungus_MaterialChange MaterialChange;
    [SerializeField] private AudioSource peach_audiosource;
    [SerializeField] private AudioClip grow;
    [SerializeField] private AudioClip fullygrow;
    [SerializeField] private AudioClip bloom;
    [SerializeField] private AudioClip withered;
    [SerializeField] private AudioClip killed;

    public void Grow()
    {
        peach_audiosource.PlayOneShot(grow);
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
        MaterialChange.MaterialWithered();
        peach_audiosource.PlayOneShot(withered);
    }
    
    public void Die()
    {
        MaterialChange.Die();
    }
    
}

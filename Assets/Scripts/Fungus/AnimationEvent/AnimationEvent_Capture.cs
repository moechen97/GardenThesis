using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Capture : MonoBehaviour
{
    [SerializeField] Fungus_MaterialChange MaterialChange;
    [SerializeField] private AudioSource capture_Audiosource;
    [SerializeField] private AudioClip grow;
    [SerializeField] private AudioClip bloom;
    [SerializeField] private AudioClip breath;
    [SerializeField] private AudioClip withered;

    public void Grow()
    {
        capture_Audiosource.PlayOneShot(grow);
    }

    public void Bloom()
    {
        capture_Audiosource.PlayOneShot(bloom);
    }

    public void Breath()
    {
        capture_Audiosource.PlayOneShot(breath);
    }
    
    public void Withered()
    {
        MaterialChange.MaterialWithered();
        capture_Audiosource.PlayOneShot(withered);
    }
    
    public void Die()
    {
        MaterialChange.Die();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance { get; private set; }

    private AudioSource[] audioPlayers;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        audioPlayers = GetComponentsInChildren<AudioSource>();
    }

    public void PlayOneClip(AudioClip preparedClip)
    {
        foreach (var audioPlayer in audioPlayers)
        {
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.PlayOneShot(preparedClip);
                return;
            }
        }
    }
   
}

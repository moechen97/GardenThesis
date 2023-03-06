using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationEvent_Plants : MonoBehaviour
{
    [SerializeField] private Fungus_MaterialChange[] _materials;
    [SerializeField] AudioClip[] bloomSounds;
    [SerializeField] private AudioClip[] growthAudios;
    [SerializeField] private AudioClip[] breatheAudios;
    [SerializeField] private AudioClip[] interactAudios;
    [SerializeField] private AudioClip[] witheredAudios;
    public abstract void InteractSound();
}

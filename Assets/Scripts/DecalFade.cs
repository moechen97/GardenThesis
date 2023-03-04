using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class DecalFade : MonoBehaviour
{
    [SerializeField]private DecalProjector _decalProjector;
    [SerializeField] private float showSpeed;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _clip;
    

    public void  DecalShowUp()
    {
        DOTween.To(() => _decalProjector.fadeFactor, x => _decalProjector.fadeFactor = x, .6f, showSpeed);
        
    }

    public void PlaySound()
    {
        _audio.pitch = Random.Range(0.8f, 1.2f);
        _audio.PlayOneShot(_clip);
    }
    
    public void  DecalFadeAway()
    {
        DOTween.To(() => _decalProjector.fadeFactor, x => _decalProjector.fadeFactor = x, 0, fadeSpeed);
        
    }
}

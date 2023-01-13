using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.Rendering;

public class DecalFade : MonoBehaviour
{
    [SerializeField]private DecalProjector _decalProjector;
    [SerializeField] private float showSpeed;
    [SerializeField] private float fadeSpeed;
    

    public void  DecalShowUp()
    {
        DOTween.To(() => _decalProjector.fadeFactor, x => _decalProjector.fadeFactor = x, .6f, showSpeed);
        
    }
    
    public void  DecalFadeAway()
    {
        DOTween.To(() => _decalProjector.fadeFactor, x => _decalProjector.fadeFactor = x, 0, fadeSpeed);
        
    }
}

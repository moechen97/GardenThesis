using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private Renderer skyRenderer;
    [SerializeField] private float changingSpeed;
    
    private Material m_Material;
    private MaterialPropertyBlock _propertyBlock;
    
    //state = 0 = night , state = 1 = day , state = 2 = dusk
    private float currentTime;

    private float resourceUsed=0f;
    private float resourceValue = 0f;
    
    private void OnEnable()
    {
        TimeEventManager.NightStart += StarNight;
        TimeEventManager.DayStart += StarDay;
    }

    private void OnDisable()
    {
        TimeEventManager.NightStart -= StarNight;
        TimeEventManager.DayStart -= StarDay;
    }

    private void Start()
    {
        _propertyBlock = new MaterialPropertyBlock();
        m_Material = skyRenderer.material;
        currentTime = TimeEventManager.state;
        if (currentTime == 0)
        {
            _propertyBlock.SetFloat("_AlphaClipTreshold", 0.1f);
            skyRenderer.SetPropertyBlock(_propertyBlock);
        }
        else
        {
            _propertyBlock.SetFloat("_AlphaClipTreshold", 1f);
            skyRenderer.SetPropertyBlock(_propertyBlock);
        }
    }

    void StarNight()
    {
        DOVirtual.Float(1f, 0.3f, changingSpeed,
            (float value) =>
            {
                _propertyBlock.SetFloat("_AlphaClipTreshold", value);
                skyRenderer.SetPropertyBlock(_propertyBlock);
            });
    }

    void StarDay()
    {
        DOVirtual.Float(0.3f, 1f, changingSpeed,
            (float value) =>
            {
                _propertyBlock.SetFloat("_AlphaClipTreshold", value);
                skyRenderer.SetPropertyBlock(_propertyBlock);
            });
    }
}

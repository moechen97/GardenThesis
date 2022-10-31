using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Planting
{
    public class Moss_Adjustment : MonoBehaviour
{
    [SerializeField] private Renderer mossRenderer;
    [SerializeField] private Color BottomDayColor;
    [SerializeField] private Color BottomNightColor;
    [SerializeField] private Color ShadowDayColor;
    [SerializeField] private Color ShadowNightColor;
    [SerializeField] private Color TopDayColor;
    [SerializeField] private Color TopNightColor;
    [SerializeField] private float colorChangingSpeed;
    [SerializeField] private float expandExtent;
    [SerializeField] private float expandSpeed;

    private Material m_Material;
    private MaterialPropertyBlock _propertyBlock;
    
    //state = 0 = night , state = 1 = day , state = 2 = dusk
    private float currentTime;

    private float resourceUsed=0f;
    private float resourceValue = 0f;
    
    private void OnEnable()
    {
        TimeEventManager.NightStart += MossNight;
        TimeEventManager.DayStart += MossDay;
    }

    private void OnDisable()
    {
        TimeEventManager.NightStart -= MossNight;
        TimeEventManager.DayStart -= MossDay;
    }

    private void Start()
    {
        _propertyBlock = new MaterialPropertyBlock();
        m_Material = mossRenderer.material;
        currentTime = TimeEventManager.state;
        _propertyBlock.SetFloat("_MaskDistance",0);
        mossRenderer.SetPropertyBlock(_propertyBlock);
        if (currentTime == 0)
        {
            _propertyBlock.SetColor("_HeightColor",BottomNightColor);
            mossRenderer.SetPropertyBlock(_propertyBlock);
            _propertyBlock.SetColor("_Shadow_Color",ShadowNightColor);
            mossRenderer.SetPropertyBlock(_propertyBlock);
            _propertyBlock.SetColor("_TopColor",TopNightColor);
            mossRenderer.SetPropertyBlock(_propertyBlock);
        }
        else
        {
            _propertyBlock.SetColor("_HeightColor",BottomDayColor);
            mossRenderer.SetPropertyBlock(_propertyBlock);
            _propertyBlock.SetColor("_Shadow_Color",ShadowDayColor);
            mossRenderer.SetPropertyBlock(_propertyBlock);
            _propertyBlock.SetColor("_TopColor",TopDayColor);
            mossRenderer.SetPropertyBlock(_propertyBlock);
        }
    }

    private void Update()
    {
        resourceUsed = Resources.GetResourcesUsed();
       
        if (resourceValue < resourceUsed)
        {
            resourceValue += expandSpeed * Time.deltaTime;
        }
        else if (resourceValue > resourceUsed)
        {
            resourceValue -= expandSpeed * Time.deltaTime;
        }
        if (Mathf.Abs(resourceValue - resourceUsed) < 0.00125F)//Mathf.Approximately(sliderValue, resourcesUsed) || )
        {
            resourceValue = resourceUsed;
        }

        _propertyBlock.SetFloat("_MaskDistance",resourceValue*4);
        mossRenderer.SetPropertyBlock(_propertyBlock);
        
        
    }

    void MossNight()
    {
        Color bottomColor = _propertyBlock.GetColor("_HeightColor");
        DOVirtual.Color(bottomColor, BottomNightColor, colorChangingSpeed, (Color value) => {
            _propertyBlock.SetColor("_HeightColor",value);
            mossRenderer.SetPropertyBlock(_propertyBlock);
        });
        
        Color shadowColor = _propertyBlock.GetColor("_Shadow_Color");
        DOVirtual.Color(shadowColor, ShadowNightColor, colorChangingSpeed, (Color value) => {
            _propertyBlock.SetColor("_Shadow_Color",value);
            mossRenderer.SetPropertyBlock(_propertyBlock);
        });
        
        Color topColor = _propertyBlock.GetColor("_TopColor");
        DOVirtual.Color(topColor, TopNightColor, colorChangingSpeed, (Color value) => {
            _propertyBlock.SetColor("_TopColor",value);
            mossRenderer.SetPropertyBlock(_propertyBlock);
        });
    }

    void MossDay()
    {
        Color bottomColor = _propertyBlock.GetColor("_HeightColor");
        DOVirtual.Color(bottomColor, BottomDayColor, colorChangingSpeed, (Color value) => {
            _propertyBlock.SetColor("_HeightColor",value);
            mossRenderer.SetPropertyBlock(_propertyBlock);
        });
        
        Color shadowColor = _propertyBlock.GetColor("_Shadow_Color");
        DOVirtual.Color(shadowColor, ShadowDayColor, colorChangingSpeed, (Color value) => {
            _propertyBlock.SetColor("_Shadow_Color",value);
            mossRenderer.SetPropertyBlock(_propertyBlock);
        });
        
        Color topColor = _propertyBlock.GetColor("_TopColor");
        DOVirtual.Color(topColor, TopDayColor, colorChangingSpeed, (Color value) => {
            _propertyBlock.SetColor("_TopColor",value);
            mossRenderer.SetPropertyBlock(_propertyBlock);
        });
    }

    
}
}


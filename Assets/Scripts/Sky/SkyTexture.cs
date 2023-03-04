using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkyTexture : MonoBehaviour
{
    [SerializeField] private Renderer skyRenderer;
    [SerializeField] private float changingSpeed;
    
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
        currentTime = TimeEventManager.state;
        if (currentTime == 0)
        {
            _propertyBlock.SetFloat("_AlphaMultipler", 0f);
            skyRenderer.SetPropertyBlock(_propertyBlock);
        }
        else
        {
            _propertyBlock.SetFloat("_AlphaMultipler", 1f);
            skyRenderer.SetPropertyBlock(_propertyBlock);
        }
    }

    void StarNight()
    {
        DOVirtual.Float(1f, 0f, changingSpeed,
            (float value) =>
            {
                _propertyBlock.SetFloat("_AlphaMultipler", value);
                skyRenderer.SetPropertyBlock(_propertyBlock);
            });
    }

    void StarDay()
    {
        DOVirtual.Float(0f, 1f, changingSpeed,
            (float value) =>
            {
                _propertyBlock.SetFloat("_AlphaMultipler", value);
                skyRenderer.SetPropertyBlock(_propertyBlock);
            });
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Fungus_MaterialChange : MonoBehaviour
{
    [SerializeField] private Renderer fungusRenderer;
    [SerializeField] private float changeSpeed = 1f;
    [SerializeField] private float glowExtentMax;
    [SerializeField] private float witheredExtent;
    [SerializeField] private Animator fungusAnimator;

    private void OnEnable()
    {
        TimeEventManager.NightStart += GotoNight;
        TimeEventManager.DayStart += GotoDay;
    }

    private void OnDisable()
    {
        TimeEventManager.NightStart -= GotoNight;
        TimeEventManager.DayStart -= GotoDay;
    }

    void GotoNight()
    {
        fungusRenderer.material.DOFloat(glowExtentMax,"_GlowExtent",changeSpeed);
    }

    void GotoDay()
    {
        fungusRenderer.material.DOFloat(0,"_GlowExtent",changeSpeed);
    }

    void Withered()
    {
        fungusAnimator.SetBool("isWithered",true);
    }
}

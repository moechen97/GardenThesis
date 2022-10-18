using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class Fungus_MaterialChange : MonoBehaviour
{
    [SerializeField] private Renderer fungusRenderer;
    [SerializeField] private float changeSpeed = 1f;
    [SerializeField] private float glowExtentMax;
    [SerializeField] private float witheredSpeed;
    [SerializeField] private float witheredExtent;
    [SerializeField] private float explodeSpeed;
    [SerializeField] private float explodeExtent;
    [SerializeField] private float hightLightExtent;
    [SerializeField] private float hightLightBreathInSpeed;
    [SerializeField] private float hightLightBreathOutSpeed;
    [SerializeField] private Animator fungusAnimator;
    [SerializeField] private GameObject explodeParticle;
    [SerializeField] private Transform particleGeneratePosition;

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

    public void Withered()
    {
        fungusAnimator.SetBool("isWithered",true);
    }

    public void MaterialWithered()
    {
        fungusRenderer.material.DOFloat(witheredExtent,"_WitheredExtent",witheredSpeed);
        fungusRenderer.material.DOFloat(0, "_DeformExtent", witheredSpeed);
    }

    public void Exploded()
    {
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        fungusRenderer.material.DOFloat(explodeExtent, "_DissolveExtent", explodeSpeed);
        yield return new WaitForSeconds(explodeExtent * 0.2f);
        Instantiate(explodeParticle, particleGeneratePosition.position,quaternion.identity);
        yield return null;
    }

    public void BreathIn()
    {
        fungusRenderer.material.DOFloat(0f,"_HighlightExtent",hightLightBreathInSpeed);
    }

    public void BreathOut()
    {
        fungusRenderer.material.DOFloat(hightLightExtent,"_HighlightExtent",hightLightBreathOutSpeed);
    }
}

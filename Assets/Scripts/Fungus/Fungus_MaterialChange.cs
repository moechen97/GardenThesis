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
    [SerializeField] private Transform Shadow;

    private Material m_Material;
    private MaterialPropertyBlock _propertyBlock;
    
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

    private void Start()
    {
        _propertyBlock = new MaterialPropertyBlock();
        m_Material = fungusRenderer.material;

    }

    void GotoNight()
    {
        fungusRenderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetFloat("_GlowExtent",glowExtentMax);
        fungusRenderer.SetPropertyBlock(_propertyBlock);
        //m_Material.DOFloat(glowExtentMax,"_GlowExtent",changeSpeed);
    }

    void GotoDay()
    {
        m_Material.DOFloat(0,"_GlowExtent",changeSpeed);
    }

    public void Withered()
    {
        fungusAnimator.SetBool("isWithered",true);
    }

    public void MaterialWithered()
    {
        m_Material.DOFloat(witheredExtent,"_WitheredExtent",witheredSpeed);
        m_Material.DOFloat(0, "_DeformExtent", witheredSpeed);
    }

    public void Exploded()
    {
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        m_Material.DOFloat(explodeExtent, "_DissolveExtent", explodeSpeed);
        Shadow.gameObject.SetActive(false);
        yield return new WaitForSeconds(explodeExtent * 0.2f);
        Instantiate(explodeParticle, particleGeneratePosition.position,quaternion.identity);
        Destroy(this.gameObject,0.1f);
        yield return null;
        
    }

    public void BreathIn()
    {
        m_Material.DOFloat(0f,"_HighlightExtent",hightLightBreathInSpeed);
    }

    public void BreathOut()
    {
        m_Material.DOFloat(hightLightExtent,"_HighlightExtent",hightLightBreathOutSpeed);
    }

    private void OnDestroy()
    {
        //Destroy(m_Material);
    }
}

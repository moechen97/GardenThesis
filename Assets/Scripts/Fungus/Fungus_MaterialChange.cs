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
    [SerializeField] private GameObject DieParticle;
    [SerializeField] private float DieSpeed;
    [SerializeField] private Transform particleGeneratePosition;
    [SerializeField] private Transform Shadow;
    [SerializeField] private float beenKilledSpeed;
    [SerializeField] private float killedExtent;
    [SerializeField] private Renderer shadowRenderer;
    [SerializeField] private bool canChangeInitialColor;
    [SerializeField] private Color initialColor;
    [SerializeField] private Color matureColor;
    [SerializeField] private float growTime;
    [SerializeField] private bool canGlow;
    [ColorUsage(true, true)]
    [SerializeField] private Color glowColor;
    [ColorUsage(true, true)]
    [SerializeField] private Color dimColor;
    [SerializeField] private float glowSpeed;
    [SerializeField] private Transform BigParent;

    private Material m_Material;
    private MaterialPropertyBlock _propertyBlock;
    
    //state = 0 = night , state = 1 = day , state = 2 = dusk
    private float currentTime;
    private bool iskilled = false;
    
    
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
        currentTime = TimeEventManager.state;
        //if it's night now
        if (currentTime == 0)
        {
            _propertyBlock.SetFloat("_GlowExtent",glowExtentMax);
            fungusRenderer.SetPropertyBlock(_propertyBlock);
        }

        if (shadowRenderer)
        {
            Debug.Log("ShadowChange");
            MaterialPropertyBlock _shadowPropertyBlock = new MaterialPropertyBlock();
            Color originalColor = shadowRenderer.material.color;
            Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
            _shadowPropertyBlock.SetColor("_BaseColor", transparentColor);
            shadowRenderer.SetPropertyBlock(_shadowPropertyBlock);
            DOVirtual.Color(transparentColor, originalColor, 1f, (Color value) =>
            {
                _shadowPropertyBlock.SetColor("_BaseColor", value);
                shadowRenderer.SetPropertyBlock(_shadowPropertyBlock);
            });
            
            if(!canChangeInitialColor)
                return;
            
            _propertyBlock.SetColor("_MainColor", initialColor);
            fungusRenderer.SetPropertyBlock(_propertyBlock);
        
            DOVirtual.Color(initialColor, matureColor, growTime, (Color value) =>
            {
                _propertyBlock.SetColor("_MainColor", value);
                fungusRenderer.SetPropertyBlock(_propertyBlock);
            });

        }
    }

    void GotoNight()
    {
        DOVirtual.Float(0, glowExtentMax, changeSpeed, (float value) => {
            _propertyBlock.SetFloat("_GlowExtent", value);
            fungusRenderer.SetPropertyBlock(_propertyBlock);
        });
        //m_Material.DOFloat(glowExtentMax,"_GlowExtent",changeSpeed);
    }

    void GotoDay()
    {
        float currentExtent = _propertyBlock.GetFloat("_GlowExtent");
        DOVirtual.Float(currentExtent, 0, changeSpeed, (float value) => {
            _propertyBlock.SetFloat("_GlowExtent", value);
            fungusRenderer.SetPropertyBlock(_propertyBlock);
        });
        //m_Material.DOFloat(0,"_GlowExtent",changeSpeed);
    }

    public void Withered()
    {
        if (!fungusAnimator)
            return;
        fungusAnimator.SetBool("isWithered",true);
    }

    public void MaterialWithered()
    {
        float currentExtent = _propertyBlock.GetFloat("_WitheredExtent");
        DOVirtual.Float(currentExtent, witheredExtent, witheredSpeed, (float value) => {
            _propertyBlock.SetFloat("_WitheredExtent", value);
            fungusRenderer.SetPropertyBlock(_propertyBlock);
        });
        
        float currentDeformExtent = _propertyBlock.GetFloat("_DeformExtent");
        DOVirtual.Float(currentDeformExtent, 0, witheredSpeed, (float value) => {
            _propertyBlock.SetFloat("_DeformExtent", value);
            fungusRenderer.SetPropertyBlock(_propertyBlock);
        });
        /*m_Material.DOFloat(witheredExtent,"_WitheredExtent",witheredSpeed);
        m_Material.DOFloat(0, "_DeformExtent", witheredSpeed);*/
    }

    public void Exploded()
    {
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        float currentDissolveExtent = _propertyBlock.GetFloat("_DissolveExtent");
        DOVirtual.Float(currentDissolveExtent, explodeExtent, explodeSpeed, (float value) => {
            _propertyBlock.SetFloat("_DissolveExtent", value);
            fungusRenderer.SetPropertyBlock(_propertyBlock);
        });
        
        //m_Material.DOFloat(explodeExtent, "_DissolveExtent", explodeSpeed);
        Shadow.gameObject.SetActive(false);
        yield return new WaitForSeconds(explodeSpeed * 0.2f);
        if (explodeParticle)
        {
            Instantiate(explodeParticle, particleGeneratePosition.position,quaternion.identity);
        }

        if (BigParent)
        {
            Destroy(BigParent.gameObject,explodeSpeed*0.8f+0.1f);
        }
        
        Destroy(this.gameObject,explodeSpeed*0.8f+0.1f);
        yield return null;
        
    }

    public void Die()
    {
        StartCoroutine(Dead());
    }

    IEnumerator Dead()
    {
        float currentDissolveExtent = _propertyBlock.GetFloat("_DissolveExtent");
        DOVirtual.Float(currentDissolveExtent, explodeExtent, DieSpeed, (float value) => {
            _propertyBlock.SetFloat("_DissolveExtent", value);
            fungusRenderer.SetPropertyBlock(_propertyBlock);
        });
        
        //m_Material.DOFloat(explodeExtent, "_DissolveExtent", explodeSpeed);
        if(Shadow)
            Shadow.gameObject.SetActive(false);
        yield return new WaitForSeconds(DieSpeed * 0.2f);
        
        if(DieParticle)
            Instantiate(DieParticle, particleGeneratePosition.position,quaternion.identity);
       
        if (BigParent)
        {
            Destroy(BigParent.gameObject,DieSpeed*0.8f+0.1f);
        }
        
        Destroy(this.gameObject,DieSpeed*0.8f+0.1f);
        yield return null;
    }

    public void BreathIn()
    {
        float currentHighLightExtent = _propertyBlock.GetFloat("_HighlightExtent");
        DOVirtual.Float(currentHighLightExtent, 0, hightLightBreathInSpeed, (float value) => {
            _propertyBlock.SetFloat("_HighlightExtent", value);
            fungusRenderer.SetPropertyBlock(_propertyBlock);
        });
        //m_Material.DOFloat(0f,"_HighlightExtent",hightLightBreathInSpeed);
    }

    public void BreathOut()
    {
        float currentHighLightExtent = _propertyBlock.GetFloat("_HighlightExtent");
        DOVirtual.Float(currentHighLightExtent, hightLightExtent, hightLightBreathOutSpeed, (float value) => {
            _propertyBlock.SetFloat("_HighlightExtent", value);
            fungusRenderer.SetPropertyBlock(_propertyBlock);
        });
        //m_Material.DOFloat(hightLightExtent,"_HighlightExtent",hightLightBreathOutSpeed);
    }

    public void Killed()
    {
        if (iskilled)
            return;
        StartCoroutine(Death());
        iskilled = true;
    }

    IEnumerator Death()
    {
        float currentkillExtent = _propertyBlock.GetFloat("_Killed_Extent");
        DOVirtual.Float(currentkillExtent, killedExtent, beenKilledSpeed, (float value) => {
            _propertyBlock.SetFloat("_Killed_Extent", value);
            fungusRenderer.SetPropertyBlock(_propertyBlock);
        });
        yield return new WaitForSeconds(beenKilledSpeed + 1f);
       
        float currentDissolveExtent = _propertyBlock.GetFloat("_DissolveExtent");
        DOVirtual.Float(currentDissolveExtent, explodeExtent, DieSpeed, (float value) => {
            _propertyBlock.SetFloat("_DissolveExtent", value);
            fungusRenderer.SetPropertyBlock(_propertyBlock);
        });
        
        //m_Material.DOFloat(explodeExtent, "_DissolveExtent", explodeSpeed);
        if(Shadow)
            Shadow.gameObject.SetActive(false);
        yield return new WaitForSeconds(DieSpeed * 0.2f);
        
        if(DieParticle)
            Instantiate(DieParticle, particleGeneratePosition.position,quaternion.identity);
        
        if (BigParent)
        {
            Destroy(BigParent.gameObject,DieSpeed*0.8f+0.1f);
        }
        
        Destroy(this.gameObject,DieSpeed*0.8f+0.1f);
        yield return null;

    }

    public void Glow()
    {
        if (!canGlow)
            return;

        DOVirtual.Color(dimColor, glowColor, glowSpeed, (Color value) =>
        {
            _propertyBlock.SetColor("_GlowColor", value);
            fungusRenderer.SetPropertyBlock(_propertyBlock);
        });
    }

    public void Dim()
    {
        if (!canGlow)
            return;
        DOVirtual.Color(glowColor, dimColor, glowSpeed, (Color value) =>
        {
            _propertyBlock.SetColor("_GlowColor", value);
            fungusRenderer.SetPropertyBlock(_propertyBlock);
        });
    }
    

    private void OnDestroy()
    {
        //Destroy(m_Material);
    }
}

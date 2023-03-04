using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimationEvent_Rings : MonoBehaviour
{
    [SerializeField] Fungus_MaterialChange[] materials;
    [SerializeField] private AudioSource ring_audiosource;
    [SerializeField] private AudioClip[] grow;
    [SerializeField] private AudioClip[] bloom;
    [SerializeField] private AudioClip[] breath;
    [SerializeField] private AudioClip withered;
    [SerializeField] private AudioClip[] interact;
    [SerializeField] private GameObject emitParticle;
    [SerializeField] private Transform emitPosition;
    [SerializeField] private Plant_StateControl _stateControl;
    [SerializeField] private Animator ringsAnimator;
    private int interactNum;
    
    private void Start()
    {
        int interactNum = Random.Range(0, interact.Length);
    }

    public void Grow()
    {
        int num = Random.Range(0, grow.Length);
        ring_audiosource.PlayOneShot(grow[num]);
    }

    public void Bloom()
    {
        int num = Random.Range(0, bloom.Length);
        ring_audiosource.PlayOneShot(bloom[num]);
    }
    
    public void Breathe()
    {
        int num = Random.Range(0, breath.Length);
        ring_audiosource.PlayOneShot(breath[num]);
    }

    public void Interact()
    {
        ring_audiosource.PlayOneShot(interact[interactNum]);
    }

    public void WitheredSound()
    {
        ring_audiosource.PlayOneShot(withered);
    }

    public void DoRandomBreath()
    {
        float i = Random.Range(0f, 1f);
        if (i > 0.5f)
        {
            ringsAnimator.SetBool("CanBreathe",true);
            StartCoroutine(DisableCanBreathe());
        }
    }

    IEnumerator DisableCanBreathe()
    {
        yield return new WaitForSeconds(2f);
        ringsAnimator.SetBool("CanBreathe",false);
    }

    public void Glow()
    {
        foreach (var material in materials)
        {
            material.Glow();
        }
    }

    public void Dim()
    {
        foreach (var material in materials)
        {
            material.Dim();
        }
    }

    
    public void Withered()
    {
        foreach (var material in materials)
        {
            material.MaterialWithered();
        }
    }
    
    public void Die()
    {
        foreach (var material in materials)
        {
            material.Die();
        }
    }

    public void EmitParticle()
    {
        Instantiate(emitParticle, emitPosition.position, Quaternion.identity);
    }
    
    public void CanInteract()
    {
        _stateControl.CanbeInteract();
    }

    public void CannotbeInteract()
    {
        _stateControl.CannotbeInteract();
    }

    public void InteractLightUp()
    {
        foreach (var material in materials)
        {
            material.BreathOut();
        }
    }

    public void InteractDim()
    {
        foreach (var material in materials)
        {
            material.BreathIn();
        }
    }
}

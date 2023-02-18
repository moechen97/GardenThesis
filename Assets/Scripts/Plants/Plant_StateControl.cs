using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DG.Tweening;
using UnityEngine;

public class Plant_StateControl : MonoBehaviour
{
    [SerializeField] private bool canBeInteract;
    [SerializeField] private Animator fungusAnimator;
    [SerializeField] private Fungus_MaterialChange[] MaterialControls;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float beingKilledDuration;
    private bool iskilled = false;

    public void Withered()
    {
        if (!fungusAnimator)
            return;
        fungusAnimator.SetBool("isWithered",true);
    }

    public void Interact()
    {
        if (!fungusAnimator || !canBeInteract)
            return;
        fungusAnimator.SetBool("isInteract",true);
    }

    public void BeingKilled()
    {
        if (!iskilled)
        {
            _audioSource.DOPitch(_audioSource.pitch - 0.02f, beingKilledDuration);
            _audioSource.DOFade(0.5f, beingKilledDuration);
            foreach (var materialChange in MaterialControls)
            {
                materialChange.Killed();
            }
        }
        iskilled = true;
    }
    
    public bool returnKilledState()
    {
        return iskilled;
    }

    public void CanbeInteract()
    {
        canBeInteract = true;
    }
    
    public void CannotbeInteract()
    {
        canBeInteract = false;
        fungusAnimator.SetBool("isInteract",false);
    }

    public void Wiggle()
    {
        foreach (var materialChange in MaterialControls)
        {
            materialChange.PlantTouchedWiggle();
        }
    }
    
}

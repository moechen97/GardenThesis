using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Plant_StateControl : MonoBehaviour
{
    [SerializeField] private bool canBeInteract;
    [SerializeField] private Animator fungusAnimator;
    [SerializeField] private Fungus_MaterialChange[] MaterialControls;
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
        iskilled = true;
        foreach (var materialChange in MaterialControls)
        {
            materialChange.Killed();
        }
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
    
}

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Plant_StateControl : MonoBehaviour
{
    [SerializeField] private Animator fungusAnimator;
    [SerializeField] private Fungus_MaterialChange[] MaterialControls;
    private bool iskilled = false;

    public void Withered()
    {
        if (!fungusAnimator)
            return;
        fungusAnimator.SetBool("isWithered",true);
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
    
}

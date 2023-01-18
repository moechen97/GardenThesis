using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Spike : MonoBehaviour
{
    [SerializeField] private Fungus_MaterialChange[] _materialChange;
    
    private GameObject spikeFlower;
    
    
    public void Bloom()
    {
        
        
    }
    
    public void Withered()
    {

        foreach (var material in _materialChange)
        {
            material.MaterialWithered();   
        }
       
    }

    public void Dead()
    {
        foreach (var material in _materialChange)
        {
            material.Die();
        }
    }

    public void Glow()
    {
        foreach (var material in _materialChange)
        {
            material.Glow();
        }
    }
    
    public void Dim()
    {
        foreach (var material in _materialChange)
        {
            material.Dim();
        }
    }
}

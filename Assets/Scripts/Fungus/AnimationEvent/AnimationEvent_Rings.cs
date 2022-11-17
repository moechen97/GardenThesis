using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Rings : MonoBehaviour
{
    [SerializeField] Fungus_MaterialChange[] materials;
    
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Bubble : MonoBehaviour
{
    [SerializeField] private Fungus_MaterialChange[] _materials;
    // Start is called before the first frame update
    public void Withered()
    {
        foreach (var material in _materials)
        {
            material.MaterialWithered();
        }
    }
    
    public void Die()
    {
        foreach (var material in _materials)
        {
            material.Die();
        }
    }
}

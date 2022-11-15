using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Peach : MonoBehaviour
{
    [SerializeField] Fungus_MaterialChange MaterialChange;
    
    public void Withered()
    {
        MaterialChange.MaterialWithered();
    }
    
    public void Die()
    {
        MaterialChange.Die();
    }
    
}

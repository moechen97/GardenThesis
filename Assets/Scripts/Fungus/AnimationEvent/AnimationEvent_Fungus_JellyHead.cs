using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Fungus_JellyHead : MonoBehaviour
{
    public Fungus_MaterialChange MaterialChange;
   
    public void BreathIn()
    {
        MaterialChange.BreathIn();
    }
    
    public void BreathOut()
    {
        MaterialChange.BreathOut();
    }

    public void Withered()
    {
        MaterialChange.MaterialWithered();
    }

    public void Dead()
    {
        MaterialChange.Die();
    }
}

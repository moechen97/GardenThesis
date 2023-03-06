using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Capture : AnimationEvent_Plants
{
   
    [SerializeField] private GameObject emitParticle;
    [SerializeField] private Transform emitPosition;
    
  
    public void EmitParticle()
    {
        Instantiate(emitParticle, emitPosition.position, emitPosition.rotation,emitPosition);
    }
    
    
}

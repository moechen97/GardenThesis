using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimationEvent_Fungus_Type1 : AnimationEvent_Plants
{
  
    [SerializeField] GameObject BreathParticle;
    [SerializeField] Transform particleInstantiatePosition;
    [SerializeField] Fungus_MaterialChange MaterialChange;
    

    public void Breath1()
    {
        int num = Random.Range(0, breatheAudios.Length);
        _audioSource.PlayOneShot(breatheAudios[num]);
        Instantiate(BreathParticle, particleInstantiatePosition.position, quaternion.identity);
        MaterialChange.BreathIn();
    }
    public void Breath2()
    {
        
        Instantiate(BreathParticle, particleInstantiatePosition.position, quaternion.identity);
        MaterialChange.BreathIn();
    }

    public void BreathOut()
    {
        MaterialChange.BreathOut();
    }
 
    
}

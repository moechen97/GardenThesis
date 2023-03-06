using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Drum : AnimationEvent_Plants
{
  
    [SerializeField] private AudioClip[] particleSound;
    [SerializeField] private GameObject emitParticle;
    [SerializeField] private Transform emitPosition;
    

    //Drum Play Sounds
   
    public void ParticleSound()
    {
        int num = Random.Range(0, particleSound.Length);
        _audioSource.PlayOneShot(particleSound[num]);
    }
    
    
    //Different plant state
    

    public void Emit()
    {
        Instantiate(emitParticle, emitPosition.position, Quaternion.identity);
    }
    
    
}

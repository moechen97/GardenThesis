using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Capture : MonoBehaviour
{
    [SerializeField] Fungus_MaterialChange MaterialChange;
    [SerializeField] private AudioSource capture_Audiosource;
    [SerializeField] private AudioClip grow;
    [SerializeField] private AudioClip bloom;
    [SerializeField] private AudioClip breath;
    [SerializeField] private AudioClip withered;
    [SerializeField] private AudioClip[] interactSound;
    [SerializeField] private GameObject emitParticle;
    [SerializeField] private Transform emitPosition;
    [SerializeField] private Plant_StateControl _stateControl;

    public void Grow()
    {
        capture_Audiosource.PlayOneShot(grow);
    }

    public void Bloom()
    {
        capture_Audiosource.PlayOneShot(bloom);
    }

    public void Breath()
    {
        capture_Audiosource.PlayOneShot(breath);
    }
    
    public void InteractSound()
    {
        int num = Random.Range(0, interactSound.Length);
        capture_Audiosource.PlayOneShot(interactSound[num]);
    }
    
    public void Withered()
    {
        MaterialChange.MaterialWithered();
        capture_Audiosource.PlayOneShot(withered);
    }
    
    public void Die()
    {
        MaterialChange.Die();
    }

    public void Glow()
    {
        MaterialChange.Glow();
    }

    public void Dim()
    {
        MaterialChange.Dim();
    }

    public void EmitParticle()
    {
        Instantiate(emitParticle, emitPosition.position, emitPosition.rotation,emitPosition);
    }
    
    public void CanInteract()
    {
        _stateControl.CanbeInteract();
    }

    public void CannotbeInteract()
    {
        _stateControl.CannotbeInteract();
    }

    public void InteractLightUp()
    {
        MaterialChange.BreathOut();
    }

    public void InteractDim()
    {
        MaterialChange.BreathIn();
    }
}

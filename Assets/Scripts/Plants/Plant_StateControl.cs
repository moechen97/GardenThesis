using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DG.Tweening;
using UnityEngine;

public class Plant_StateControl : MonoBehaviour
{
    [SerializeField] private bool canBeInteract;
    [SerializeField] private Animator fungusAnimator;
    [SerializeField] private Fungus_MaterialChange[] MaterialControls;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float beingKilledDuration;
    private bool iskilled = false;
    [HideInInspector] public bool interacting = false;
    [SerializeField] private float minimumPlantVolume = 0.05f;
    [SerializeField] private float volumeUpTime = 6.895f;
    [SerializeField] private float volumeDownTime = 0.675f;
    private bool volumeAdjustment = false;
    private bool volumeDown = false;
    private void Awake()
    {
        Plant_State_Control_Manager.Instance.AddPlant(this);
        _audioSource.volume = Plant_State_Control_Manager.Instance.uniformVolume;
    }
    private void OnDestroy()
    {
        Plant_State_Control_Manager.Instance.RemovePlant(this);
    }
    public void Withered()
    {
        if (!fungusAnimator)
            return;
        fungusAnimator.SetBool("isWithered",true);
    }
    public void Interact()
    {
        if (!fungusAnimator || !canBeInteract)
            return;
        fungusAnimator.SetBool("isInteract",true);
    }
    public void BeingKilled()
    {
        if (!iskilled)
        {
            _audioSource.DOPitch(_audioSource.pitch - 0.02f, beingKilledDuration);
            _audioSource.DOFade(0.5f, beingKilledDuration);
            foreach (var materialChange in MaterialControls)
            {
                materialChange.Killed();
            }
        }
        iskilled = true;
    }    
    public bool returnKilledState()
    {
        return iskilled;
    }
    public void CanbeInteract()
    {
        canBeInteract = true;
    }   
    public void CannotbeInteract()
    {
        canBeInteract = false;
        fungusAnimator.SetBool("isInteract",false);
    }
    public void Wiggle()
    {
        foreach (var materialChange in MaterialControls)
        {
            materialChange.PlantTouchedWiggle();
        }
    }
    public void IsInteracting()
    {
        interacting = true;
        Plant_State_Control_Manager.Instance.AddInteractingPlant(this);
    }
    public void DoneInteracting()
    {
        interacting = false;
        Plant_State_Control_Manager.Instance.RemoveInteractingPlant(this);
    }
    public float GetVolume()
    {
        return _audioSource.volume;
    }
    public void SetVolume(float v)
    {
        _audioSource.volume = v;
    }
}

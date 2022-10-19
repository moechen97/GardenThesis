using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Fungus_Jelly : MonoBehaviour
{
    [SerializeField] private GameObject JellyHead;
    [SerializeField] private Transform JellyHeadPostion;
    [SerializeField] private Transform JellyHeadRotation;
    [SerializeField] private Fungus_MaterialChange _materialChange;

    private GameObject jellyHead;
    
    private void Start()
    {
        jellyHead =Instantiate(JellyHead, JellyHeadPostion.position, JellyHeadRotation.rotation, 
            this.transform.parent);
        jellyHead.GetComponent<Fungus_JellyHead>().GetPosition(JellyHeadPostion,JellyHeadRotation);
    }

    public void Bloom()
    {
        jellyHead.GetComponent<Fungus_JellyHead>().JellyBloom();
    }

    public void Withered()
    {
        jellyHead.GetComponent<Fungus_JellyHead>().jellyWithered();
        _materialChange.MaterialWithered();
    }

    public void Dead()
    {
        _materialChange.Die();
        jellyHead.GetComponent<Fungus_JellyHead>().JellyDead();
    }
}

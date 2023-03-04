using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Planting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HUDAnimation : MonoBehaviour
{
    public static HUDAnimation instance;
    [SerializeField] private RectTransform SettingUI;
    [SerializeField] private Vector2 settingStartAnchorPose;
    [SerializeField] private Vector2 settingEndAnchorPose;
    [SerializeField] private RectTransform SeedBarUI;
    [SerializeField] private Vector2 barStartAnchorPose;
    [SerializeField] private Vector2 barEndAnchorPose;
    [SerializeField] private RectTransform SeedToggleUI;
    [SerializeField] private Vector2 seedStartAnchorPose;
    [SerializeField] private Vector2 seedEndAnchorPose;
    [SerializeField] private float fadeInSpeed;
    [SerializeField] private float fadeOutSpeed;
    private bool isIdle = false;

    private void Awake()
    {
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
    }

    private void Update()
    {
        if (GardenInput.isIdle&&!isIdle)
        {
            IdleFadeOut();
            isIdle = true;
        }
        else if (!GardenInput.isIdle && isIdle)
        {
            IdleFadeIn();
            isIdle = false;
        }
    }

    public void IdleFadeIn()
    {
        SettingUI.DOKill();
        SeedBarUI.DOKill();
        SeedToggleUI.DOKill();
        SettingUI.DOAnchorPos(settingEndAnchorPose, fadeInSpeed);
        SeedBarUI.DOAnchorPos(barEndAnchorPose, fadeInSpeed);
        SeedToggleUI.DOAnchorPos(seedEndAnchorPose, fadeInSpeed);
    }
    
    public void IdleFadeOut()
    {
        SettingUI.DOKill();
        SeedBarUI.DOKill();
        SeedToggleUI.DOKill();
        SettingUI.DOAnchorPos(settingStartAnchorPose, fadeOutSpeed);
        SeedBarUI.DOAnchorPos(barStartAnchorPose, fadeOutSpeed);
        SeedToggleUI.DOAnchorPos(seedStartAnchorPose, fadeOutSpeed);
    }

    public void SeedFadeIn()
    {
        SettingUI.DOKill();
        SeedBarUI.DOKill();
        SeedToggleUI.DOKill();
        SeedBarUI.DOAnchorPos(barEndAnchorPose, fadeInSpeed);
        SeedToggleUI.DOAnchorPos(seedEndAnchorPose, fadeInSpeed);
    }
    

    public void SettingFadeIn()
    {
        SettingUI.DOKill();
        SeedBarUI.DOKill();
        SeedToggleUI.DOKill();
        SettingUI.DOAnchorPos(settingEndAnchorPose, fadeInSpeed);
    }

    public void SetUpTutorialFormat()
    {
        SeedBarUI.anchoredPosition = barStartAnchorPose;
        SettingUI.anchoredPosition = settingStartAnchorPose;
        SeedToggleUI.anchoredPosition = seedStartAnchorPose;
    }
}

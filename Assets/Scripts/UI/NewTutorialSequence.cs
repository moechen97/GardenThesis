using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Planting;

namespace Planting
{
    public class NewTutorialSequence : MonoBehaviour
{
    public static bool finishedTutorial = false;
    public static NewTutorialSequence instance { get; private set; }
    [SerializeField] private GameObject TutorialCamera;
    [SerializeField] private CanvasGroup startCanvas;
    [SerializeField] private CanvasGroup gameCanvas;
    //[SerializeField] private CanvasGroup tutorialsCanvas;
    [SerializeField] private CanvasGroup RotationCanvas;
    [SerializeField] private CanvasGroup ZoomCanvas;
    [SerializeField] private CanvasGroup PanCanvas;
    [SerializeField] private CanvasGroup SeedtoolCanvas;
    [SerializeField] private CanvasGroup SeedbarCanvas;
    [SerializeField] private CanvasGroup SettingCanvas;
    [SerializeField] private CanvasGroup CongratulationCanvas;
    [SerializeField] private CanvasRenderer _renderer;
    [SerializeField] private GameObject tutorialGroup;
    private bool rotationFinishd = false;
    private bool zoomFinished = false;
    private bool panFinished = false;
    private bool barOpened = false;
    private bool seedPlanted = false;
    private bool settingshowup = false;
    

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
    
    void Start()
    {
        TutorialCamera.SetActive(true);
        startCanvas.alpha = 1;
        gameCanvas.alpha = 0;
        StartCoroutine(ChangeTitleColor());
        
        if (!finishedTutorial)
        {
            HUDAnimation.instance.SetUpTutorialFormat();
            gameCanvas.alpha = 1;
        }
    }
    
    IEnumerator ChangeTitleColor()
    {
        yield return new WaitForSeconds(0.5f);
        _renderer.GetMaterial().DOFloat(0f, "_StepEdge", 0f);
    }
    
    void Update()
    {
        if (finishedTutorial)
        {
            return;
        }

        //rotation Camera tutorial
        if (!rotationFinishd)
        {
            if (GardenInput.isRotatingCamera)
            {
                StartCoroutine(RotateCameraFinished());
                rotationFinishd = true;
            }
        }
        
        //zoom Camera tutorial;
        if (!zoomFinished && rotationFinishd)
        {
            if (GardenInput.isZoomingCamera)
            {
                StartCoroutine(ZoomCameraFinished());
                zoomFinished = true;
            }
        }
        
        //pan CameraTutorial
        if (!panFinished && zoomFinished)
        {
            if (GardenInput.isPanningCamera)
            {
                StartCoroutine(PanCameraFinished());
                panFinished = true;
            }
        }

        //finished planting first seed;
        if (!seedPlanted && barOpened)
        {
            float resourceUsed = Resources.GetResourcesUsed();
            if (resourceUsed > 0)
            {
                StartCoroutine(PlantedSeed());
                seedPlanted = true;
            }
        }
        
        
    }

    IEnumerator RotateCameraFinished()
    {
        yield return new WaitForSeconds(3f);
        RotationCanvas.DOFade(0, 1.5f);
        yield return new WaitForSeconds(3f);
        ZoomCanvas.DOFade(1, 2f);
    }

    IEnumerator ZoomCameraFinished()
    {
        yield return new WaitForSeconds(3f);
        ZoomCanvas.DOFade(0, 1.5f);
        yield return new WaitForSeconds(3f);
        PanCanvas.DOFade(1, 2f);
        
    }

    IEnumerator PanCameraFinished()
    {
        yield return new WaitForSeconds(3f);
        PanCanvas.DOFade(0, 1.5f);
        yield return new WaitForSeconds(3f);
        HUDAnimation.instance.SeedFadeIn();
        SeedtoolCanvas.DOFade(1, 1.5f);
    }
    
    public void StartGame()
    {
        StartCoroutine(TapStart());
    }

    IEnumerator TapStart()
    {
        TutorialCamera.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        _renderer.GetMaterial().DOFloat(1.03f, "_StepEdge", 1.8f);
        startCanvas.DOFade(0f, 2f);
        yield return new WaitForSeconds(2f);
        _renderer.GetMaterial().SetFloat("_StepEdge", 0f);
        startCanvas.transform.gameObject.SetActive(false);
        RotationCanvas.DOFade(1f, 2f);
        yield return new WaitForSeconds(1.5f);
        GardenInput.instance.EnableControl();
        
    }

    public void SkipTutorial()
    {
        if (!finishedTutorial)
        {
            StartCoroutine(SkipT());
        }
        finishedTutorial = true;
    }

    IEnumerator SkipT()
    {
        TutorialCamera.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        _renderer.GetMaterial().DOFloat(1.03f, "_StepEdge", 1.8f);
        startCanvas.DOFade(0f, 1.5f);
        yield return new WaitForSeconds(1.5f);
        HUDAnimation.instance.IdleFadeIn();
        _renderer.GetMaterial().SetFloat("_StepEdge", 0f);
        startCanvas.transform.gameObject.SetActive(false);
        GardenInput.instance.EnableControl();
    }

    public void ToggleSeedButton()
    {
        if (!finishedTutorial && !barOpened)
        {
            StartCoroutine(TappedSeedButton());
        }
    }
    
    IEnumerator TappedSeedButton()
    {
        yield return null;
        SeedtoolCanvas.DOFade(0f, 0.5f);
        SeedbarCanvas.DOFade(1, 2f);
        barOpened = true;
    }

    IEnumerator PlantedSeed()
    {
        SeedbarCanvas.DOFade(0, 1f);
        yield return new WaitForSeconds(2f);
        HUDAnimation.instance.SettingFadeIn();
        SettingCanvas.DOFade(1, 2f);
        seedPlanted = true;
        yield return new WaitForSeconds(3.5f);
        SettingCanvas.DOFade(0, 1.5f);
        yield return new WaitForSeconds(2f);
        CongratulationCanvas.DOFade(1, 2f);
        yield return new WaitForSeconds(3f);
        CongratulationCanvas.DOFade(0, 1.5f);
        yield return new WaitForSeconds(1.5f);
        finishedTutorial = true;
        tutorialGroup.SetActive(false);
        
    }
}

}

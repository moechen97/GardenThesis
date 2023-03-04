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
    [SerializeField] private GameObject creditButton;
    private bool rotationFinishd = false;
    private bool zoomFinished = false;
    private bool panFinished = false;
    private bool barOpened = false;
    private bool seedPlanted = false;
    private bool settingshowup = false;
    private bool isRotating;
    private bool isZooming;
    private bool isPanning;
    private bool isCamera1Toggle = false;
    private bool isCamera2Toggle = false;
    private bool iscameraResetFinished = false;
    private bool cameraReset = false;


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
            creditButton.SetActive(false);
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
        if (!rotationFinishd&&!isRotating)
        {
            if (GardenInput.isRotatingCamera)
            {
                StartCoroutine(RotateCameraFinished());
                isRotating = true;
            }
        }
        
        //zoom Camera tutorial;
        if (!zoomFinished && rotationFinishd && !isZooming)
        {
            if (GardenInput.isZoomingCamera)
            {
                StartCoroutine(ZoomCameraFinished());
                isZooming = true;
            }
        }
        
        //pan CameraTutorial
        if (!panFinished && zoomFinished &&!isPanning)
        {
            if (GardenInput.isPanningCamera)
            {
                StartCoroutine(PanCameraFinished());
                isPanning = true;
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
        
        //camera reset tutorial
        if (isCamera1Toggle && isCamera2Toggle && !cameraReset)
        {
            StartCoroutine(CameraResetFinished());
            cameraReset = true;
        }
        
        
    }

    IEnumerator RotateCameraFinished()
    {
        yield return new WaitForSeconds(3f);
        RotationCanvas.DOFade(0, 1.5f);
        yield return new WaitForSeconds(3f);
        ZoomCanvas.DOFade(1, 2f);
        rotationFinishd = true;
    }

    IEnumerator ZoomCameraFinished()
    {
        yield return new WaitForSeconds(3f);
        ZoomCanvas.DOFade(0, 1.5f);
        yield return new WaitForSeconds(3f);
        PanCanvas.DOFade(1, 2f);
        zoomFinished = true;
    }

    IEnumerator PanCameraFinished()
    {
        yield return new WaitForSeconds(3f);
        PanCanvas.DOFade(0, 1.5f);
        yield return new WaitForSeconds(3f);
        HUDAnimation.instance.SettingFadeIn();
        SettingCanvas.DOFade(1, 1.5f);
        panFinished = true;
    }

    IEnumerator CameraResetFinished()
    {
        yield return new WaitForSeconds(3f);
        SettingCanvas.DOFade(0, 1.2f);
        yield return new WaitForSeconds(2f);
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
        creditButton.SetActive(true);
        TutorialCamera.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        _renderer.GetMaterial().DOFloat(1.03f, "_StepEdge", 1.8f);
        startCanvas.DOFade(0f, 1.5f);
        yield return new WaitForSeconds(2f);
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
        seedPlanted = true;
        creditButton.SetActive(true);
        CongratulationCanvas.DOFade(1, 2f);
        yield return new WaitForSeconds(3f);
        CongratulationCanvas.DOFade(0, 1.5f);
        yield return new WaitForSeconds(1.5f);
        finishedTutorial = true;
        tutorialGroup.SetActive(false);
        
    }

    public void TapCamera1Botton()
    {
        isCamera1Toggle = true;
    }
    
    public void TapCamera2Botton()
    {
        isCamera2Toggle = true;
    }
}

}
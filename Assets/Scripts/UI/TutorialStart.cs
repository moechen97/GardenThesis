using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Planting;
using UnityEngine;

public class TutorialStart : MonoBehaviour
{
    [SerializeField] private GameObject TutorialCamera;
    [SerializeField] private CanvasGroup gameCanvas;
    [SerializeField] private CanvasGroup startCanvas;
    [SerializeField] private CanvasGroup CameraInstruction;
    [SerializeField] private GardenInput _input;
    [SerializeField] private CanvasRenderer _renderer;

    private void Start()
    {
        TutorialCamera.SetActive(true);
        gameCanvas.alpha = 0;
        startCanvas.alpha = 1;
        StartCoroutine(ChangeTitleColor());
    }

    IEnumerator ChangeTitleColor()
    {
        yield return new WaitForSeconds(0.5f);
        _renderer.GetMaterial().DOFloat(0f, "_StepEdge", 0.1f);
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
        startCanvas.DOFade(0, 2f);
        yield return new WaitForSeconds(2f);
        gameCanvas.DOFade(1, 2f);
        yield return new WaitForSeconds(1.8f);
        startCanvas.gameObject.SetActive(false);
        CameraInstruction.DOFade(1, 1.5f);
        
    }

    public void EnableCameraControl()
    {
        _input.EnableControl();
    }
}

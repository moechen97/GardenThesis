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
    [SerializeField] private GardenInput _input;

    private void Start()
    {
        TutorialCamera.SetActive(true);
        gameCanvas.alpha = 0;
        startCanvas.alpha = 1;
    }

    public void StartGame()
    {
        StartCoroutine(TapStart());
    }

    IEnumerator TapStart()
    {
        TutorialCamera.SetActive(false);
        startCanvas.DOFade(0, 2f);
        yield return new WaitForSeconds(2f);
        gameCanvas.DOFade(1, 2f);
        yield return new WaitForSeconds(2f);
        _input.EnableControl();
    }
}

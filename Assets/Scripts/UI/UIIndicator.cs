using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIIndicator : MonoBehaviour
{
    [SerializeField] private Color canPlantColor;
    [SerializeField] private Color cannotPlantColor;
    [SerializeField] private Image _image;
    
    private Animator _animator;

    private void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void CanPlant()
    {
        _animator.SetBool("expanded",true);
        _image.DOColor(canPlantColor, 1f);
    }

    public void CannotPlant()
    {
        _animator.SetBool("expanded",false);
        _image.DOColor(cannotPlantColor, 1f);
    }
}

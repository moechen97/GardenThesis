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
    private MaterialPropertyBlock _propertyBlock;
    private bool canPlant = false;

    private void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();


    }

    public void CanPlant()
    {
        if (canPlant)
            return;
        _image.DOKill();
        _animator.SetBool("expanded",true);

        _image.DOColor(canPlantColor, 0.5f);
        canPlant = true;
    }

    public void CannotPlant()
    {
        if (!canPlant)
            return;
        _image.DOKill();
        _image.DOColor(cannotPlantColor, 0.5f);
        canPlant = false;
    }

    public void ResetIndicator()
    {
        _animator.SetBool("expanded",false);
        _image.DOKill();
        _image.color = cannotPlantColor;
    }
}

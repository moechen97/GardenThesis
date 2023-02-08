using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILifeButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    public void ActiveButton()
    {
        _image.color = activeColor;
    }

    public void InactiveButton()
    {
        _image.color = inactiveColor;
    }
}

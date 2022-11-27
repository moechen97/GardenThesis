using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISizeAdjustment : MonoBehaviour
{
    [SerializeField] private float offset;
    private float lengthX;
    private Vector3 rectPosition;
    private Vector3 PanelPosition;
    private float scaleMultiplyer = 1f;
    private CanvasGroup _canvas;
    
    void Start()
    {
        _canvas = GetComponent<CanvasGroup>();
        lengthX = transform.parent.parent.GetComponent<RectTransform>().rect.width;
        lengthX -= 100f;
    }

   
    void Update()
    {
        if (Input.deviceOrientation == DeviceOrientation.Portrait ||
            Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {
            lengthX = transform.parent.parent.GetComponent<RectTransform>().rect.width;
            lengthX -= 100f;
        }
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeRight ||
                 Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            lengthX = transform.parent.parent.GetComponent<RectTransform>().rect.width;
            lengthX -= 100f;
        }
        UIPositionAdjust();
        
    }


    void UIPositionAdjust()
    {
        rectPosition = transform.GetComponent<RectTransform>().localPosition;
        PanelPosition = transform.parent.GetComponent<RectTransform>().anchoredPosition;
        
        
        //on the left
        if (PanelPosition.x+rectPosition.x <= offset)
        {
            float ratio = (PanelPosition.x + rectPosition.x) / (offset-20f);
            float multiple = Mathf.Lerp(0f, 1f, ratio);
            transform.localScale = Vector3.one*multiple;
            _canvas.alpha = multiple;

        }
        
        // middle
        else if (PanelPosition.x + rectPosition.x >= offset && PanelPosition.x + rectPosition.x <= lengthX - offset)
        {
            transform.localScale = Vector3.one;
            _canvas.alpha = 1;
        }
        
        //on the right
        else if (PanelPosition.x + rectPosition.x > lengthX - offset)
        {
            float ratio = (PanelPosition.x + rectPosition.x - lengthX) / offset;
            float multiple = Mathf.Lerp(1f, 0f, ratio);
            transform.localScale = Vector3.one * multiple;
            _canvas.alpha = multiple;
        }

        
       
    }
    
}

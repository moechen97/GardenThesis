using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonCooldown : MonoBehaviour
{
    [SerializeField] private float cooldownTime;
    [SerializeField] private Image fillImage;
    [SerializeField] private CanvasRenderer _renderer;

    private float countdownTime;
    private float currentTime;
    private bool isCountingdown =false;
    private MaterialPropertyBlock block;

   

    // Update is called once per frame
    void Update()
    {
        Debug.Log("isCountingdown"+isCountingdown);
        if (isCountingdown)
        {
            currentTime = Time.time - countdownTime;
            //fillImage.fillAmount = Mathf.Clamp(1f-(currentTime / cooldownTime), 0f, 1f);
            if (currentTime / cooldownTime > 1f / 2f)
            {
                float amount = Mathf.Lerp(0f, 1.02f, (currentTime-(cooldownTime/2f)) / (cooldownTime/2f));
                _renderer.GetMaterial().SetFloat("_StepEdge",amount);
            }
            
            if (currentTime >= cooldownTime)
            {
                isCountingdown = false;
            }
        }
    }

    public void StartCountDown()
    {
        isCountingdown = true;
        countdownTime = Time.time;
        //fillImage.fillAmount = 1f;
        _renderer.GetMaterial().SetFloat("_StepEdge",0f);
    }

    public bool GetCountDownState()
    {
        return isCountingdown;
    }
}

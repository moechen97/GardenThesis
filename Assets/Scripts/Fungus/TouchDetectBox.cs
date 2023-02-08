using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetectBox : MonoBehaviour
{
    [SerializeField] private Plant_StateControl _stateControl;

    public void IsTouched()
    {
        //Debug.Log("isTouched!");
        _stateControl.Interact();
        _stateControl.Wiggle();
    }
    
    
}

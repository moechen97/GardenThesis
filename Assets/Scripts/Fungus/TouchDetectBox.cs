using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetectBox : MonoBehaviour
{
    [SerializeField] private Plant_StateControl _stateControl;

    public void IsTouched()
    {
        _stateControl.Interact();
    }
    
    
}

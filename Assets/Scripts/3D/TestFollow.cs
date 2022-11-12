using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestFollow : MonoBehaviour
{
    private void Update()
    {
        var touch = Touchscreen.current;
        if (touch == null)
        {
            return;
        }

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.ReadValue().x,
            touch.position.ReadValue().y, 20f));
    }
}

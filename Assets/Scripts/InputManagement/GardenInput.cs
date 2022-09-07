using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class GardenInput : MonoBehaviour
{
    private GardenControl gardenControl;
    private Camera cameraMain;
    [SerializeField] GameObject draggableMushroom;
    [SerializeField] GameObject mushroomSprite;
    [SerializeField] DragAndDrop mushroomUI;
    void Awake()
    {
        gardenControl = new GardenControl();
        cameraMain = Camera.main;
    }

    private void Start()
    {
        gardenControl.Plant.Hold.started += ctx => StartDrag(ctx);
        gardenControl.Plant.Hold.canceled += ctx => EndDrag(ctx);
        //gardenControl.Plant.Tap.started += ctx => StartTap(ctx);
        //gardenControl.Plant.Tap.canceled += ctx => EndTap(ctx);
        //gardenControl.Plant.Drag.started += ctx => StartDrag(ctx);
        //UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
    }
    private void StartDrag(InputAction.CallbackContext context)
    {
        Debug.Log("START Drag " + context.ReadValue<float>());
    }
    private void EndDrag(InputAction.CallbackContext context)
    {
        Debug.Log("END DRAG!! " + gardenControl.Plant.FirstFingerPosition.ReadValue<Vector2>());
        if (mushroomUI.isDragging)
        {
            Vector2 finger = gardenControl.Plant.FirstFingerPosition.ReadValue<Vector2>();
            Vector3 screenCoordinates = new Vector3(finger.x, finger.y, cameraMain.nearClipPlane);
            screenCoordinates.z = 0.0F;
            RaycastHit hit;
            Ray ray = cameraMain.ScreenPointToRay(screenCoordinates);
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                Debug.Log("Hit transform: " + hit.point);
                Debug.Log("Hit name: " + hit.transform);
                GameObject plantedMushroom = GameObject.Instantiate(draggableMushroom);
                plantedMushroom.transform.position = hit.point;
            }
            mushroomUI.ResetPosition();
        }
    }
    private void OnEnable()
    {
        Debug.Log("Enabled garden input control");
        gardenControl.Enable();
    }

    private void OnDisable()
    {
        Debug.Log("Disabled garden input control");
        gardenControl.Disable();
    }

    private void FingerDown(Finger finger)
    {
        Debug.Log("Finger down");
    }
}

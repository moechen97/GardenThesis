using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SimplePlant : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [SerializeField] private float maxRotationSpeed;
    [SerializeField] private float maxFingerChangeDistance;
    [SerializeField] private float dampTime;
    
    private float lerpDuration;
    private float timeElapsed;
   
    private float rotationSpeed;
    private bool isfirstFingerTapping = false;

    private Vector2 pointerDelta;
    private Vector2 rotateDirection;

    private bool isDragging = false;

    private Vector2 firstFingerPositionDelta;
    private Vector2 secondFingerPositionDelta;
    
    private Vector2 firstFingerCurrentPosition;
    private Vector2 secondFingerCurrentPosition;
    
    private Vector2 firstFingerPreviousPosition;
    private Vector2 secondFingerPreviousPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isfirstFingerTapping = pointerDelta != Vector2.zero;
        if (isfirstFingerTapping)
        {
            RotateCamera();
        }
        else
        {
            RotationSpeedSlowDown();
        }
    }
    

    public void OnTap()
    {
        Debug.Log("Tap");
    }
    

    public void OnDrag(InputValue value)
    {
        Debug.Log("Drag");

        Vector2 moveVal = value.Get<Vector2>();
        Vector3 forwardDirection = transform.position - Camera.main.transform.position;
        transform.Translate(new Vector3(moveVal.x,0,0)*moveSpeed*Time.deltaTime,Space.Self);
        transform.Translate(new Vector3(forwardDirection.x,0,forwardDirection.z).normalized*moveVal.y*moveSpeed*Time.deltaTime,Space.World);
    }

    public void OnRotation(InputValue value)
    {
        pointerDelta = value.Get<Vector2>();
    }

    public void OnFirstFingerPosition(InputValue value)
    {
        firstFingerPositionDelta = value.Get<Vector2>();
        //Debug.Log("firstFingerPositionDelta: "+firstFingerPositionDelta);
    }
    
    public void OnSecondaryFingerPosition(InputValue value)
    {
        secondFingerPositionDelta = value.Get<Vector2>();
        //Debug.Log("secondFingerPositionDelta: "+secondFingerPositionDelta);
        
        if (firstFingerPositionDelta.x * secondFingerPositionDelta.x > 0 &&
            firstFingerPositionDelta.y * secondFingerPositionDelta.y > 0)
        {
            Vector3 forwardDirection = transform.position - Camera.main.transform.position;
            transform.Translate(new Vector3(secondFingerPositionDelta.x,0,0)*moveSpeed*Time.deltaTime,Space.Self);
            transform.Translate(new Vector3(forwardDirection.x,0,forwardDirection.z).normalized*secondFingerPositionDelta.y*moveSpeed*Time.deltaTime,Space.World);
            Debug.Log("move");
        }
        else
        {
            
        }
    }
    
    
    void RotationSpeedSlowDown()
    {
        if (isDragging)
        {
            float fingerPositionDifference = pointerDelta.magnitude;
            if (fingerPositionDifference < maxFingerChangeDistance)
            {
                lerpDuration = (fingerPositionDifference / maxFingerChangeDistance) * dampTime;
            }
            else
            {
                lerpDuration = dampTime;
            }
        
            timeElapsed = 0;
            isDragging = false;
        }

        if (timeElapsed < lerpDuration)
        {
            rotationSpeed = Mathf.Lerp(maxRotationSpeed, 0f, timeElapsed / lerpDuration);
         
            Vector3 rot = transform.localRotation.eulerAngles + new Vector3(
                -rotateDirection.y * rotationSpeed * Time.deltaTime, -rotateDirection.x * rotationSpeed * Time.deltaTime, 0f);
            rot.x = ClampAngle(rot.x, 0f, 88f);
            rot.z = 0;
         
            transform.eulerAngles = rot;

            timeElapsed += Time.deltaTime;
        }
      
    }
    
    void RotateCamera()
    {
        if (!isDragging)
        {
            isDragging = true;
            rotationSpeed = maxRotationSpeed;
        }
      
        rotateDirection = pointerDelta;

        Vector3 rot = transform.localRotation.eulerAngles + new Vector3(
            -rotateDirection.y * rotationSpeed * Time.deltaTime, -rotateDirection.x * rotationSpeed * Time.deltaTime, 0f);
        rot.x = ClampAngle(rot.x, 0f, 85f);
        rot.z = 0;
         
        transform.eulerAngles = rot;

        //transform.Rotate(new Vector3(1, 0, 0), rotateDirection.y * rotationSpeed * Time.deltaTime );
        //transform.Rotate(new Vector3(0, 1, 0), -rotateDirection.x * rotationSpeed * Time.deltaTime , Space.World);
        
    }
    
    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360+from);
        return Mathf.Min(angle, to);
    }
    
}

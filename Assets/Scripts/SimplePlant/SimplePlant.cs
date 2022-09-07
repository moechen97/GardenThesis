using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class SimplePlant : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [SerializeField] private float maxRotationSpeed;
    [SerializeField] private float maxFingerChangeDistance;
    [SerializeField] private float dampTime;
    [SerializeField] private float fingerZoomSpeed = 4f;
    [SerializeField] private float cameraZoomMin = -20f;
    [SerializeField] private float cameraZoomMax = -1f;
    [SerializeField] private float scrollZoomSpeed = 8f;
    
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

    private float previousDistance;

    private CinemachineVirtualCamera _virtualCamera;

    private bool cannotRotatie = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        isfirstFingerTapping = pointerDelta != Vector2.zero;
        
        cannotRotatie = Input.touchCount > 1;

        if (isfirstFingerTapping)
        {
            RotateCamera();
            Debug.Log("NotSlowDown");
        }
        else
        {
            RotationSpeedSlowDown();
            Debug.Log("SlowDown");
        }
    }
    

    public void OnTap()
    {
        Debug.Log("Tap");
    }
    

    public void OnMouseMiddlePress(InputValue value)
    {
        //Pan
       
        Vector2 moveVal = value.Get<Vector2>();
        Vector3 forwardDirection = transform.position - Camera.main.transform.position;
        transform.Translate(new Vector3(-moveVal.x,0,0)*moveSpeed*Time.deltaTime,Space.Self);
        transform.Translate(new Vector3(forwardDirection.x,0,forwardDirection.z).normalized*-moveVal.y*moveSpeed*Time.deltaTime,Space.World);
    }
    
    public void OnFirstTouchInformation(InputValue value)
    {
        if (value.Get<TouchState>().phase == TouchPhase.Began)
        {
            firstFingerPreviousPosition = value.Get<TouchState>().startPosition;
        }
        
        firstFingerCurrentPosition = value.Get<TouchState>().position;
        firstFingerPositionDelta = firstFingerCurrentPosition-firstFingerPreviousPosition;
        firstFingerPreviousPosition = firstFingerCurrentPosition;

        
    }
    
    public void OnSecondTouchInformation(InputValue value)
    {
        if (value.Get<TouchState>().phase == TouchPhase.Began)
        {
            secondFingerPreviousPosition = value.Get<TouchState>().startPosition;
            previousDistance = Vector3.Distance(firstFingerPreviousPosition, secondFingerPreviousPosition);
        }
        
        
        secondFingerCurrentPosition = value.Get<TouchState>().position;
        secondFingerPositionDelta = secondFingerCurrentPosition-secondFingerPreviousPosition;
        secondFingerPreviousPosition = secondFingerCurrentPosition;


        //camera move follow finger
        if (firstFingerPositionDelta.x * secondFingerPositionDelta.x > 0 ||
            firstFingerPositionDelta.y * secondFingerPositionDelta.y > 0)
        {
            Vector3 forwardDirection = transform.position - Camera.main.transform.position;
            transform.Translate(new Vector3(-secondFingerPositionDelta.x,0,0)*moveSpeed*Time.deltaTime,Space.Self);
            transform.Translate(new Vector3(forwardDirection.x,0,forwardDirection.z).normalized*-secondFingerPositionDelta.y*moveSpeed*Time.deltaTime,Space.World);
           
        }
        else if(Vector2.Dot(firstFingerPositionDelta,secondFingerPositionDelta)<-0.1f)
        {
            //camera zoom
           
            float distance = Vector3.Distance(firstFingerCurrentPosition, secondFingerCurrentPosition);
            
            float offset = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z;
            float target = Mathf.Clamp(offset + (distance - previousDistance)/100f,cameraZoomMin,cameraZoomMax);
            float newZValue = Mathf.Lerp(offset, target, fingerZoomSpeed * Time.deltaTime);
            _virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
                new Vector3(0, 0, newZValue);
            
            previousDistance = distance;
        }
    }
    
    public void OnScrollZoom(InputValue value)
    {
        float offset = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z;
        float offsetChange = offset + value.Get<Vector2>().y;
        float target = Mathf.Clamp(offsetChange,cameraZoomMin,cameraZoomMax);
        float newZValue = Mathf.Lerp(offset, target, scrollZoomSpeed * Time.deltaTime);
        _virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            new Vector3(0, 0, newZValue);
        
    }
    
    public void OnFingerAndMouseDrag(InputValue value)
    {
        pointerDelta = value.Get<Vector2>();
        Debug.Log("Rotate");
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
        if (cannotRotatie)
            return;
        
        if (!isDragging)
        {
            isDragging = true;
            rotationSpeed = maxRotationSpeed;
        }
      
        rotateDirection = pointerDelta;

        Vector3 rot = transform.localRotation.eulerAngles + new Vector3(
            -rotateDirection.y * rotationSpeed * Time.deltaTime, rotateDirection.x * rotationSpeed * Time.deltaTime, 0f);
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

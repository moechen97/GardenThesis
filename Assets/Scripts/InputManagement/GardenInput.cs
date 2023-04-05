using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;

namespace Planting
{
    public class GardenInput : MonoBehaviour
    {
        private Dictionary<PlantType, GameObject> seeds;
        private GardenControl gardenControl;
        public static GardenInput instance;
        Camera cameraMain;
        [SerializeField] GameObject cam;
        [SerializeField] Image indicator;
        [SerializeField] private UIIndicator _uiIndicator;
        [SerializeField] GameObject plantMenu;
        [SerializeField] GameObject plantMenu_Canvas;
        [SerializeField] EventSystem eventSystem;
        private GraphicRaycaster graphicRaycaster;
        private bool isOnPlantMenu = false;
        private bool rotatingScreen = false;
        private PlantMenu menu;
        [HideInInspector] public PlantType currSeed;
        [HideInInspector] public bool isDraggingSeed = false;
        private bool isDragging_InScrollbar = false;
        [SerializeField] GraphicRaycaster UIgraphicRaycaster;
        private Vector3 previousRotatePosition;
        private Vector3 currentRotatePosition;
        [SerializeField] GameObject camFocusPoint;
        [SerializeField] GameObject ground;
        private Vector3 rotateDirection;
        private Vector3 startDragPosition;
        private Vector3 endDragPosition;
        private float rotateStep;
        [SerializeField] float rotateSpeed = 1F;
        private Coroutine afterRotate = null;
        private Coroutine zoomCoroutine = null;
        [SerializeField] Transform camTransform;
        [SerializeField] float panZoomSpeed = 10F;
        [SerializeField] float cameraZoomSpeed = 4F;
        [SerializeField] private float rotationSlowDownSpeed = 1 / 2f;
        [SerializeField] private GameObject PrepareSeed;
        private Coroutine zoomEndDelay = null;
        private CinemachineVirtualCamera _virtualCamera;
        private bool twoFingers = false;
        private bool touchPlantDrag = false;
        private Vector2 prevFirstFingerPos = Vector2.zero;
        private Vector2 prevSecondFingerPos = Vector2.zero;
        private bool isZooming = false;
        private bool isPanning = false;
        private Coroutine zoomToPanEndDelay = null;
        private Coroutine panToZoomEndDelay = null;
        private Vector3 startCamPosition;
        private Vector3 startCamRotation;
        private bool enableControl = false;
        private Vector3 PreviousDragPosition;
        private Vector2 fingerPositionOffset = new Vector2(0f, 20f);
        private UIButtonCooldown presseButtonCooldown;
        [SerializeField] ScrollRect scrollbar;
        private bool isScrolling = false;
        [SerializeField] private float idleTime;
        private float stoptouchTime;
        private float currentTime;
        private bool stoptouch = true;
        public static bool isIdle=false;
        public static bool isRotatingCamera = false;
        public static bool isZoomingCamera = false;
        public static bool isPanningCamera = false;
        private bool isdragging = false;
        private bool stopDragging = false;

        void Awake()
        {
            if (instance != null && instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                instance = this; 
            } 
            startCamPosition = camFocusPoint.transform.position;
            startCamRotation = camFocusPoint.transform.eulerAngles;
            graphicRaycaster = plantMenu_Canvas.GetComponent<GraphicRaycaster>();
            gardenControl = new GardenControl();
            cameraMain = Camera.main;
            fingerPositionOffset = new Vector2(0f,Screen.height / 42f);
        }

        private void Start()
        {
            seeds = PlantManager.instance.GetComponent<SeedDictionaryScript>().DeserializeDictionary();
            gardenControl.Plant.Hold.started += ctx => StartDrag(ctx);
            gardenControl.Plant.Hold.canceled += ctx => EndDrag(ctx);
            gardenControl.Plant.Tap.started += ctx => StartTap(ctx);
            gardenControl.Plant.Tap.canceled += ctx => EndTap(ctx);
            gardenControl.Plant.SecondaryTouchContact.started += _ => ZoomStart();
            gardenControl.Plant.SecondaryTouchContact.canceled += _ => ZoomEnd();
            _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        }
        

        public void ResetPosition()
        {
            //camFocusPoint.transform.position = startCamPosition;
            //camFocusPoint.transform.eulerAngles = startCamRotation;
            StopRotating();
            camFocusPoint.transform.DORotate(startCamRotation, 2f);
            camFocusPoint.transform.DOMove(startCamPosition, 2f);
        }
        public void SetTopView()
        {
            StopRotating();
            Vector3 rot = camFocusPoint.transform.eulerAngles;
            //camFocusPoint.transform.eulerAngles = new Vector3(85F, rot.y, rot.z);
            camFocusPoint.transform.DORotate(new Vector3(85F, rot.y, rot.z), 1.5f);
        }

        private void StopRotating()
        {
            if (afterRotate != null)
            {
                StopCoroutine(afterRotate);
                afterRotate = null;
                //rotatingScreen = false;
            }
        }
        private void ZoomStart()
        {
            twoFingers = true;
            if (zoomEndDelay != null)
            {
                StopCoroutine(zoomEndDelay);
            }
            zoomCoroutine = StartCoroutine(ZoomDetection());
        }
        private void ZoomEnd()
        {
            StopCoroutine(zoomCoroutine);
            zoomEndDelay = StartCoroutine(ZoomEndDelay());
        }

        private IEnumerator ZoomEndDelay()
        {
            yield return new WaitForSeconds(0.15F);
            twoFingers = false;
            zoomEndDelay = null;
        }

        private IEnumerator ZoomDetection()
        {
            float previousDistance = 0f, distance = 0f;
            isZooming = false;
            bool doneZooming = false;
            float minZoom = -63F;
            float maxZoom = -9.917652F;
            float maxZoomDistance = minZoom - maxZoom;
            yield return new WaitForSeconds(0.05F);
            while (true)
            {
                Vector2 firstFingerPos = gardenControl.Plant.FirstFingerPosition.ReadValue<Vector2>();
                Vector2 secondFingerPos = gardenControl.Plant.SecondaryFingerPosition.ReadValue<Vector2>();
                Vector2 deltaFirstFingerPos = (firstFingerPos - prevFirstFingerPos);
                Vector2 deltaSecondFingerPos = (secondFingerPos - prevSecondFingerPos);
                distance = Vector2.Distance(firstFingerPos, secondFingerPos);
                float dot = Vector2.Dot(deltaFirstFingerPos.normalized, deltaSecondFingerPos.normalized);
                if ((deltaFirstFingerPos == Vector2.zero && deltaSecondFingerPos == Vector2.zero)
                    || previousDistance == 0F || previousDistance == distance || (doneZooming && Mathf.Abs(distance - previousDistance) <= 0.35F))
                {
                    if(deltaFirstFingerPos == Vector2.zero && deltaSecondFingerPos == Vector2.zero && isZooming && zoomToPanEndDelay == null)
                    {
                        doneZooming = true;
                    }
                }
                else
                {
                    doneZooming = false;
                    //Camera pans
                    if (dot >= 0.01F || (dot >= -0.01F && dot != 0F))
                    {
                        //panToZoomEndDelay: To prevent panning when at end of zooming out
                        if (isZooming)
                        {
                            yield return null;
                            if (zoomToPanEndDelay == null)
                            {
                                zoomToPanEndDelay = StartCoroutine(ZoomToPanEndDelay());
                            }
                            continue;
                        }
                        isPanning = true;
                        Vector2 TouchDeltaPosition = (deltaFirstFingerPos + deltaSecondFingerPos) / 2F;
                        float t = (minZoom - cam.transform.position.z) / maxZoomDistance;
                        float panSpeed = Mathf.Lerp(panZoomSpeed, panZoomSpeed / 4F, t);
                        camFocusPoint.transform.Translate(panSpeed * Time.deltaTime * -TouchDeltaPosition.normalized.x, panSpeed * Time.deltaTime * -TouchDeltaPosition.normalized.y, 0F);
                        camFocusPoint.transform.position = new Vector3(Mathf.Clamp(camFocusPoint.transform.position.x, -20F, 20F), Mathf.Clamp(camFocusPoint.transform.position.y, -10F + 3.04F, 10F), camFocusPoint.transform.position.z);
                    }
                    //Detection
                    //Zoom out
                    else if (distance > previousDistance)
                    {
                        if (isPanning)
                        {
                            yield return null;
                            if (panToZoomEndDelay == null)
                            {
                                panToZoomEndDelay = StartCoroutine(PanToZoomEndDelay());
                            }
                            continue;
                        }
                        isZooming = true;
                        float offset = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z;
                        offset += 1F;
                        float newZValue =
                            Mathf.Lerp(_virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z,
                                offset, Time.deltaTime * cameraZoomSpeed);

                        if (newZValue > -10f)
                        {
                            newZValue = -10f;
                        }
                        else if (newZValue < -62f)
                        {
                            newZValue = -62f;
                        }
                        _virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
                            new Vector3(0, 0, newZValue);

                    }
                    //Zoom in
                    else if (distance < previousDistance)
                    {
                        if (isPanning)
                        {
                            yield return null;
                            if (panToZoomEndDelay == null)
                            {
                                panToZoomEndDelay = StartCoroutine(PanToZoomEndDelay());
                            }
                            continue;
                        }
                        isZooming = true;
                        float offset = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z;
                        offset -= 1F;
                        float newZValue =
                            Mathf.Lerp(_virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z,
                                offset, Time.deltaTime * cameraZoomSpeed);
                        if (newZValue > -10f)
                        {
                            newZValue = -10f;
                        }
                        else if (newZValue < -62f)
                        {
                            newZValue = -62f;
                        }
                        _virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
                            new Vector3(0, 0, newZValue);

                    }
                }
                previousDistance = distance;
                prevFirstFingerPos = firstFingerPos;
                prevSecondFingerPos = secondFingerPos;
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator ZoomToPanEndDelay()
        {
            yield return new WaitForSeconds(0.15F);
            isZooming = false;
            zoomToPanEndDelay = null;
            yield return null;
        }
        private IEnumerator PanToZoomEndDelay()
        {
            yield return new WaitForSeconds(0.075F);
            isPanning = false;
            panToZoomEndDelay = null;
            yield return null;
        }
        private IEnumerator UpdatePrevFingerPosition(Vector2 firstFingerPos, Vector2 secondFingerPos)
        {
            yield return new WaitForEndOfFrame();
            prevFirstFingerPos = firstFingerPos;
            prevSecondFingerPos = secondFingerPos;
        }
        private void StartTap(InputAction.CallbackContext context)
        {
            if (isOnPlantMenu)
            {
                Vector2 finger = gardenControl.Plant.FirstFingerPosition.ReadValue<Vector2>();
                Vector3 screenCoordinates = new Vector3(finger.x, finger.y, cameraMain.nearClipPlane);
                screenCoordinates.z = 0.0F;
                PointerEventData pointerEventData = new PointerEventData(eventSystem);
                pointerEventData.position = screenCoordinates;
                List<RaycastResult> results = new List<RaycastResult>();
                graphicRaycaster.Raycast(pointerEventData, results);
                foreach(RaycastResult result in results)
                {
                    if(result.gameObject.name.Equals("DeletePlant"))
                    {
                        Destroy(menu.menuObject);
                        Destroy(menu.plantObject);
                        isOnPlantMenu = false;
                        return;
                    }
                }
                if(isOnPlantMenu)
                {
                    Destroy(menu.menuObject);
                    isOnPlantMenu = false;
                }
            }

            if (!isOnPlantMenu)
            {
                StartCoroutine(WaitForTap());
            }

            stoptouch = false;
            isIdle = false;

            if (!stopDragging)
            {
                isDraggingSeed = false;
                isDragging_InScrollbar = false;
                rotatingScreen = false;
                _uiIndicator.gameObject.SetActive(false);
                _uiIndicator.ResetIndicator();
            }

            stopDragging = false;

        }

        private IEnumerator WaitForTap()
        {
            yield return new WaitForEndOfFrame();
            if (!isDraggingSeed)
            {
                //Check if player tapped on plant
                Vector2 finger = gardenControl.Plant.FirstFingerPosition.ReadValue<Vector2>();
                Vector3 screenCoordinates = new Vector3(finger.x, finger.y, cameraMain.nearClipPlane);
                screenCoordinates.z = 0.0F;
                RaycastHit hit;
                Ray ray = cameraMain.ScreenPointToRay(screenCoordinates);
                int layer_mask = LayerMask.GetMask("PlantTouch"); //I change it to plantTouch because that collider is changing according to plants' height.
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask))
                {
                    if (hit.transform.GetComponent<TouchDetectBox>())
                    {
                        hit.transform.GetComponent<TouchDetectBox>().IsTouched();
                    }
                    DeformPlant(hit);
                }
            }
        }
        private void DeformPlant(RaycastHit plant)
        {
            Fungus_MaterialChange plantModifier = plant.collider.GetComponentInParent<Fungus_MaterialChange>();
            if (plantModifier != null)
            {
                plantModifier.PlantTouchedWiggle();
            }
        }
        private void Update()
        {
            //Idle Check
            if (stoptouch && !isIdle)
            {
                currentTime = Time.time - stoptouchTime;
                if (currentTime >= idleTime)
                {
                    isIdle = true;
                }
            }
            
            if (!enableControl)
                return;

            if(isScrolling && !isDraggingSeed)
            {
                Vector2 finger = gardenControl.Plant.FirstFingerPosition.ReadValue<Vector2>();
                finger += fingerPositionOffset;
                Vector3 screenCoordinates = new Vector3(finger.x, finger.y, cameraMain.nearClipPlane);
                screenCoordinates.z = 0.0F;
                PointerEventData pointerEventData = new PointerEventData(eventSystem);
                pointerEventData.position = screenCoordinates;
                List<RaycastResult> UIresults = new List<RaycastResult>();
                UIgraphicRaycaster.Raycast(pointerEventData, UIresults);
                if (UIresults.Count == 0)
                {
                    scrollbar.enabled = false;
                }
                else
                {
                    scrollbar.enabled = true;
                    scrollbar.OnBeginDrag(pointerEventData);
                }
            }
            else if (isDraggingSeed)
            {
                //Convert finger to screen coordinates
                Vector2 finger = gardenControl.Plant.FirstFingerPosition.ReadValue<Vector2>();
                finger += fingerPositionOffset;
                Vector3 screenCoordinates = new Vector3(finger.x, finger.y, cameraMain.nearClipPlane);
                screenCoordinates.z = 0.0F;

                //Check if drag is still in scrollbar
                if (isDragging_InScrollbar)
                {
                    PointerEventData pointerEventData = new PointerEventData(eventSystem);
                    pointerEventData.position = screenCoordinates;
                    List<RaycastResult> UIresults = new List<RaycastResult>();
                    UIgraphicRaycaster.Raycast(pointerEventData, UIresults);
                    if (UIresults.Count == 0)
                    {
                        isDragging_InScrollbar = false;
                        scrollbar.enabled = false;
                        PlantManager.SelectPlantIcon(currSeed, true);
                    }
                }
                else
                {
                    //Set square indicator when user is dragging object
                    _uiIndicator.gameObject.SetActive(true);
                    Debug.Log("ActiveIndicator");
                    _uiIndicator.transform.position = screenCoordinates;
                    RaycastHit hit;
                    Ray ray = cameraMain.ScreenPointToRay(screenCoordinates);
                    int layer_mask = LayerMask.GetMask("Ground");
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask))
                    {               
                        //Don't let plants plant on top of each other
                        Transform objectHit = hit.transform;      
                        float radius = 0.5f;
                        if (currSeed == PlantType.Plant_Spike)
                        {
                            radius = 0.05f;
                        }
                        Collider[] collisions = Physics.OverlapSphere(hit.point, radius, LayerMask.GetMask("Plant"));//activeSeeds[0].plantRadius);
                        if (hit.transform.gameObject.name.Equals("Ground") && collisions.Length == 0)
                        {
                            _uiIndicator.CanPlant();
                        }
                        else
                        {
                            _uiIndicator.CannotPlant();
                        }
                    }
                    else
                    {
                        _uiIndicator.CannotPlant();
                    }
                }
            }
            else if(rotatingScreen)
            {
                if(twoFingers || touchPlantDrag)
                {
                    rotatingScreen = false;
                    return;
                }
                Vector2 fingerPos = gardenControl.Plant.FirstFingerPosition.ReadValue<Vector2>();
                Vector3 screenCoordinates = new Vector3(fingerPos.x, fingerPos.y, cameraMain.nearClipPlane);
                screenCoordinates.z = 0.0F;
                currentRotatePosition = screenCoordinates;
                rotateDirection = previousRotatePosition - currentRotatePosition;
                float rotationAroundYAxis = -rotateDirection.x * rotateSpeed * Time.deltaTime; //camera moves horizontally
                float rotationAroundXAxis = rotateDirection.y * rotateSpeed * Time.deltaTime; //camera moves vertically
                Vector3 rot = camFocusPoint.transform.localEulerAngles + 
                    new Vector3(rotationAroundXAxis, rotationAroundYAxis, 0f);
                rot.x = ClampAngle(rot.x, 0f, 85f);
                rot.z = 0;

                camFocusPoint.transform.localEulerAngles = rot;
                previousRotatePosition = currentRotatePosition;
            }
            // tutorial parameters
            isRotatingCamera = rotatingScreen;
            isZoomingCamera = isZooming;
            isPanningCamera = isPanning;         
        }
        
        float ClampAngle(float angle, float from, float to)
        {
            // accepts e.g. -80, 80
            if (angle < 0f) angle = 360 + angle;
            if (angle > 180f) return Mathf.Max(angle, 360 + from);
            return Mathf.Min(angle, to);
        }

        private IEnumerator SpinAfterRotate(Vector3 direction)
        {
            yield return new WaitForEndOfFrame();
            rotateStep -= Time.deltaTime * rotationSlowDownSpeed;
            float rotationAroundYAxis = direction.normalized.x * rotateStep; //camera moves horizontally
            Vector3 rot = camFocusPoint.transform.localEulerAngles +
                new Vector3(0f, rotationAroundYAxis, 0f);
            rot.x = ClampAngle(rot.x, 0f, 85f);
            rot.z = 0;
            camFocusPoint.transform.localEulerAngles = rot;
            if (rotateStep > 0.0F)
            {
                afterRotate = StartCoroutine(SpinAfterRotate(direction));
            }
            else
            {
                afterRotate = null;
            }
        }
        private void StartDrag(InputAction.CallbackContext context)
        {
            //Check for taps on UI seeds
            isdragging = true;
            StartCoroutine(WaitForDrag());
            stoptouch = false;
            isIdle = false;
        }

        private IEnumerator WaitForDrag()
        {
            yield return new WaitForEndOfFrame();
            if(twoFingers)
            {
                yield return null;
            }
            Vector2 fingerPos = gardenControl.Plant.FirstFingerPosition.ReadValue<Vector2>();
            Vector3 screenCoordinates = new Vector3(fingerPos.x, fingerPos.y, cameraMain.nearClipPlane);
            screenCoordinates.z = 0.0F;
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = screenCoordinates;
            List<RaycastResult> UIresults = new List<RaycastResult>();
            UIgraphicRaycaster.Raycast(pointerEventData, UIresults);
            foreach (RaycastResult result in UIresults)
            {
                isScrolling = true;
                foreach (KeyValuePair<PlantType, GameObject> plant in seeds)
                {
                    if(result.gameObject.name.Equals(plant.Key.ToString()))
                    {
                        if (!result.gameObject.GetComponentInParent<UIButtonCooldown>().GetCountDownState())
                        {
                            presseButtonCooldown =
                                result.gameObject.GetComponentInParent<UIButtonCooldown>();
                            isDraggingSeed = true;
                            isDragging_InScrollbar = true;
                            currSeed = plant.Key;  
                        }
                    }
                }
            }
            if(!isDraggingSeed && !twoFingers && UIresults.Count == 0)
            {
                RaycastHit hit;
                Ray ray = cameraMain.ScreenPointToRay(screenCoordinates);            
                int layer_mask = LayerMask.GetMask("PlantTouch");
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask))
                {
                    touchPlantDrag = true;
                    yield return null;
                }
                else
                {
                    rotatingScreen = true;
                    StartCoroutine(SaveStartDragPosition());
                }                
                StopRotating();
                camFocusPoint.transform.DOKill();
                previousRotatePosition = screenCoordinates;
            }
        }

        private IEnumerator SaveStartDragPosition()
        {
            yield return new WaitForEndOfFrame();
            startDragPosition = currentRotatePosition;
        }
        private IEnumerator WaitForEndDrag()
        {
            yield return new WaitForEndOfFrame();
            touchPlantDrag = false;
        }
        
        private void EndTap(InputAction.CallbackContext ctx)
        {
            //Debug.Log("EndTap");
            if (isdragging)
            {
                return;
            }
            isDraggingSeed = false;
            isDragging_InScrollbar = false;
            rotatingScreen = false;
            _uiIndicator.gameObject.SetActive(false);
            _uiIndicator.ResetIndicator();
        }
        private void EndDrag(InputAction.CallbackContext context)
        {
            stopDragging = true;
            isdragging = false;
            //Debug.Log("EndDrag");
            scrollbar.enabled = true;
            isScrolling = false;
            stoptouch = true;
            stoptouchTime = Time.time;
            if(touchPlantDrag)
            {
                StartCoroutine(WaitForEndDrag());
                return;
            }
            if(twoFingers)
            {
                rotatingScreen = false;
                _uiIndicator.gameObject.SetActive(false);
                return;
            }

            if (isDraggingSeed)
            {
                AttemptPlant(currSeed);
            }
            isDraggingSeed = false;
            isDragging_InScrollbar = false;
            if(rotatingScreen)
            {
                Vector3 dragRotationLength = currentRotatePosition - startDragPosition;
                if (Mathf.Abs(dragRotationLength.x) > Mathf.Abs(dragRotationLength.y))
                {
                    rotateStep = Mathf.Abs(Mathf.Clamp(dragRotationLength.x, -400F, 400F)) / 300F;
                    afterRotate = StartCoroutine(SpinAfterRotate(-rotateDirection));
                }
                else
                {
                    rotateStep = 0.0F;
                }
            }
            rotatingScreen = false;
            _uiIndicator.gameObject.SetActive(false);
            _uiIndicator.ResetIndicator();
        }

        private bool AttemptPlant(PlantType type)
        {
            bool success = false;
            Vector2 finger = gardenControl.Plant.FirstFingerPosition.ReadValue<Vector2>();
            finger += fingerPositionOffset;
            Vector3 screenCoordinates = new Vector3(finger.x, finger.y, cameraMain.nearClipPlane);
            screenCoordinates.z = 0.0F;
            RaycastHit hit;
            Ray ray = cameraMain.ScreenPointToRay(screenCoordinates);
            int layer_mask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask))
            {
                Transform objectHit = hit.transform;
                //Don't let plants plant on top of each other
                float radius = 0.5f;
                if (currSeed == PlantType.Plant_Spike)
                {
                    radius = 0.05f;
                }
                Collider[] collisions = Physics.OverlapSphere(hit.point, radius);
                if (hit.transform.gameObject.name.Equals("Ground") && collisions.Length == 1)
                {
                    StartCoroutine(PlantSeed(hit.point,seeds[currSeed]));
                    success = true;
                }
            }
            PlantManager.SelectPlantIcon(currSeed, false);
            return success;
        }
        IEnumerator PlantSeed(Vector3 instantiateP,GameObject seed)
        {
            presseButtonCooldown.StartCountDown();
            presseButtonCooldown = null;
            Instantiate(PrepareSeed, instantiateP, Quaternion.identity);
            yield return new WaitForSeconds(2.5f);
            GameObject newPlant = GameObject.Instantiate(seed);
            newPlant.transform.position = instantiateP;
        }     
        private void OnEnable()
        {
            gardenControl.Enable();
        }
        private void OnDisable()
        {
            gardenControl.Disable();
        }
        public struct PlantMenu
        {
            public GameObject menuObject;
            public GameObject plantObject;

            public PlantMenu(GameObject menu, GameObject plant)
            {
                menuObject = menu;
                plantObject = plant;
                Transform traverse = plant.transform;
                while (traverse != null)
                {
                    plantObject = traverse.gameObject;
                    traverse = traverse.parent;
                }
            }
        }
        public void EnableControl()
        {
            enableControl = true;
        }      
    }
}
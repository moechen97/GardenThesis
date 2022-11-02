using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Cinemachine;

namespace Planting
{
    public class GardenInput : MonoBehaviour
    {
        private Dictionary<PlantType, GameObject> seeds;
        private Dictionary<PlantType, string> plantNames;
        private GardenControl gardenControl;
        Camera cameraMain;
        [SerializeField] PlantManager plantManager;
        [SerializeField] GameObject cam;
        [SerializeField] Image indicator;
        [SerializeField] GameObject plantMenu;
        [SerializeField] GameObject plantMenu_Canvas;
        [SerializeField] EventSystem eventSystem;
        private GraphicRaycaster graphicRaycaster;
        private bool isOnPlantMenu = false;
        private bool rotatingScreen = false;
        private PlantMenu menu;
        [HideInInspector] public PlantType currSeed;
        [HideInInspector] public bool isDraggingSeed = false;
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
        [SerializeField] float cameraZoomSpeed = 4F;
        [SerializeField] private float rotationSlowDownSpeed = 1 / 2f;
        
        private Coroutine zoomEndDelay = null;
        private CinemachineVirtualCamera _virtualCamera;
        private bool twoFingers = false;
        private bool touchPlantDrag = false;
        public struct PlantMenu
        {
            public GameObject menuObject;
            public GameObject plantObject;

            public PlantMenu(GameObject menu, GameObject plant)
            {
                menuObject = menu;
                plantObject = plant;
                Transform traverse = plant.transform;
                //traverse to root transform of plant
                while (traverse != null)
                {
                    plantObject = traverse.gameObject;
                    traverse = traverse.parent;
                }
            }
        }
        void Awake()
        {
            seeds = plantManager.GetComponent<SeedDictionaryScript>().DeserializeDictionary();
            plantNames = plantManager.GetComponent<PlantNameDictionaryScript>().DeserializeDictionary();
            graphicRaycaster = plantMenu_Canvas.GetComponent<GraphicRaycaster>();
            gardenControl = new GardenControl();
            cameraMain = Camera.main;
            foreach(PlantType type in seeds.Keys)
            {
                PlantManager.AddPlant(type);
            }
        }
        private void Start()
        {
            gardenControl.Plant.Hold.started += ctx => StartDrag(ctx);
            gardenControl.Plant.Hold.canceled += ctx => EndDrag(ctx);
            gardenControl.Plant.Tap.started += ctx => StartTap(ctx);
            gardenControl.Plant.SecondaryTouchContact.started += _ => ZoomStart();
            gardenControl.Plant.SecondaryTouchContact.canceled += _ => ZoomEnd();
            //gardenControl.Plant.Tap.canceled += ctx => EndTap(ctx);
            //gardenControl.Plant.Drag.started += ctx => StartDrag(ctx);
            //UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
            
            _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
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
            while(true)
            {
                Vector2 firstFingerPos = gardenControl.Plant.FirstFingerPosition.ReadValue<Vector2>();
                Vector2 secondFingerPos = gardenControl.Plant.SecondaryFingerPosition.ReadValue<Vector2>();
                distance = Vector2.Distance(firstFingerPos, secondFingerPos);
                float dot = Vector2.Dot(firstFingerPos.normalized, secondFingerPos.normalized);
                //Camera pan
                if (dot > 0.9960975F)
                {
                    Vector2 TouchDeltaPosition;
                    if (Input.touchCount >= 2) 
                    {
                        TouchDeltaPosition = (Input.GetTouch(0).deltaPosition + Input.GetTouch(1).deltaPosition) / 2F;
                    }
                    else
                    {
                        TouchDeltaPosition = Input.GetTouch(0).deltaPosition;
                    }
                    //Vector2 TouchDeltaPosition = (gardenControl.Plant.FirstFingerPositionDelta.ReadValue<Vector2>() + gardenControl.Plant.SecondaryFingerPositionDelta.ReadValue<Vector2>()) / 2F;
                    camFocusPoint.transform.Translate(cameraZoomSpeed * Time.deltaTime * -TouchDeltaPosition.normalized.x, cameraZoomSpeed * Time.deltaTime * -TouchDeltaPosition.normalized.y, 0F);
                    camFocusPoint.transform.position = new Vector3(Mathf.Clamp(camFocusPoint.transform.position.x, -20F, 20F), Mathf.Clamp(camFocusPoint.transform.position.y, -10F + 3.04F, 10F), camFocusPoint.transform.position.z);
                }
                //Detection
                //Zoom out
                else if (distance > previousDistance)
                {

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
                else if(distance < previousDistance)
                { 
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

                previousDistance = distance;
                yield return null;
            }
        }
        private void StartSecondTouch(InputAction.CallbackContext context)
        {
            Debug.Log("Second Touch Information");
        }
        private void StartSecondaryFinger(InputAction.CallbackContext context)
        {
            Debug.Log("Secondary Finger Position");
        }
        private void StartTap(InputAction.CallbackContext context)
        {
            if (isOnPlantMenu)
            {
                Vector2 finger = gardenControl.Plant.FirstFingerPosition.ReadValue<Vector2>();
                Vector3 screenCoordinates = new Vector3(finger.x, finger.y, cameraMain.nearClipPlane);
                screenCoordinates.z = 0.0F;
                RaycastHit2D hit;
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
                int layer_mask = LayerMask.GetMask("Plant");
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask))
                {
                    //If true, create plant menu
                    isOnPlantMenu = true;
                    GameObject menuObject = GameObject.Instantiate(plantMenu);
                    //Give y-height boost of 0.25F so the menu doesn't spawn partially inside the ground
                    menuObject.transform.parent = plantMenu_Canvas.transform;
                    menuObject.transform.position = screenCoordinates;
                    menu = new PlantMenu(menuObject, hit.transform.gameObject);
                }
            }
        }

        private void Update()
        {
            if (isDraggingSeed)
            {
                //Set square indicator when user is dragging object
                indicator.gameObject.SetActive(true);
                //Convert finger to screen coordinates
                Vector2 finger = gardenControl.Plant.FirstFingerPosition.ReadValue<Vector2>();
                Vector3 screenCoordinates = new Vector3(finger.x, finger.y, cameraMain.nearClipPlane);
                screenCoordinates.z = 0.0F;
                indicator.transform.position = screenCoordinates;
                if (Resources.GetResourcesUsed() + PlantManager.resourceDict[currSeed] > 1.0F)
                {
                    indicator.color = Color.red;
                }
                else
                {
                    RaycastHit hit;
                    Ray ray = cameraMain.ScreenPointToRay(screenCoordinates);
                    int layer_mask = LayerMask.GetMask("Ground");
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask))
                    {
                        Transform objectHit = hit.transform;
                        //Debug.Log("Hit transform: " + hit.point);
                        //Debug.Log("Hit name: " + hit.transform);
                        //Don't let plants plant on top of each other
                        Collider[] collisions = Physics.OverlapSphere(hit.point, 0.5F, LayerMask.GetMask("Plant"));//activeSeeds[0].plantRadius);
                        if (hit.transform.gameObject.name.Equals("Ground") && collisions.Length == 0)
                        {
                            indicator.color = Color.green;
                        }
                        else
                        {
                            indicator.color = Color.red;
                        }
                    }
                    else
                    {
                        indicator.color = Color.red;
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
            //rotateStep -= Time.deltaTime * Mathf.Clamp(rotateStep, 1F, rotateSpeed) * rotateSpeed;
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
            StartCoroutine(WaitForDrag());
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
                foreach(KeyValuePair<PlantType, string> plant in plantNames)
                {
                    if(result.gameObject.name.Equals(plant.Value))
                    {
                        isDraggingSeed = true;
                        currSeed = plant.Key;
                    }
                }
            }
            if(!isDraggingSeed && !twoFingers && UIresults.Count == 0)
            {
                RaycastHit hit;
                Ray ray = cameraMain.ScreenPointToRay(screenCoordinates);
                int layer_mask = LayerMask.GetMask("Plant");
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask))
                {
                    touchPlantDrag = true;
                    yield return null;
                }
                rotatingScreen = true;
                StartCoroutine(SaveStartDragPosition());
                if(afterRotate != null)
                {
                    StopCoroutine(afterRotate);
                    afterRotate = null;
                }
                //rotateStep = rotateSpeed / 2F;
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
        private void EndDrag(InputAction.CallbackContext context)
        {
            if(touchPlantDrag)
            {
                StartCoroutine(WaitForEndDrag());
                return;
            }
            if(twoFingers)
            {
                rotatingScreen = false;
                indicator.gameObject.SetActive(false);
                return;
            }

            if (isDraggingSeed)
            {
                AttemptPlant(currSeed);
            }
            isDraggingSeed = false;
            if(rotatingScreen)
            {
                Vector3 dragRotationLength = currentRotatePosition - startDragPosition;
                if (Mathf.Abs(dragRotationLength.x) > Mathf.Abs(dragRotationLength.y))
                {
                    rotateStep = Mathf.Abs(Mathf.Clamp(dragRotationLength.x, -400F, 400F)) / 300F;
                    //rotateStep = 0.1F * Mathf.Abs(Mathf.Clamp(dragRotationLength.x, -7F, 7F));
                    if (rotateStep < 0.25F)
                    {
                        rotateStep = 0.25F;
                    }
                    afterRotate = StartCoroutine(SpinAfterRotate(dragRotationLength));
                }
                else
                {
                    rotateStep = 0.0F;
                }
            }
            rotatingScreen = false;
            indicator.gameObject.SetActive(false);
        }

        private bool AttemptPlant(PlantType type)
        {
            if (Resources.GetResourcesUsed() + PlantManager.resourceDict[type] > 1.0F)
            {
                return false;
            }
            bool success = false;
            Vector2 finger = gardenControl.Plant.FirstFingerPosition.ReadValue<Vector2>();
            Vector3 screenCoordinates = new Vector3(finger.x, finger.y, cameraMain.nearClipPlane);
            screenCoordinates.z = 0.0F;
            RaycastHit hit;
            Ray ray = cameraMain.ScreenPointToRay(screenCoordinates);
            //if (Physics.Raycast(ray, out hit))
            int layer_mask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask))
            {
                Transform objectHit = hit.transform;
                //Don't let plants plant on top of each other
                Collider[] collisions = Physics.OverlapSphere(hit.point, 0.5F);
                if (hit.transform.gameObject.name.Equals("Ground") && collisions.Length == 1)
                {
                    GameObject newPlant = GameObject.Instantiate(seeds[currSeed]);
                    newPlant.transform.position = hit.point;
                    success = true;
                }
            }
            //seed.ResetPosition();
            return success;
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
}

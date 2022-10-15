using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

namespace Planting
{
    public class GardenInput : MonoBehaviour
    {
        private Dictionary<PlantType, GameObject> seeds;
        private GardenControl gardenControl;
        Camera cameraMain;
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
        private float rotateStep;
        [SerializeField] float rotateSpeed = 1F;
        private Coroutine afterRotate = null;
        private Coroutine zoomCoroutine = null;
        [SerializeField] Transform camTransform;
        [SerializeField] float cameraZoomSpeed = 4F;
        private Coroutine zoomEndDelay = null;

        private bool twoFingers = false;
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
            seeds = GetComponent<SeedDictionaryScript>().DeserializeDictionary();
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
        }
        private void ZoomStart()
        {
            Debug.Log("ZOOM START");
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
            yield return new WaitForSeconds(0.5F);
            twoFingers = false;
            zoomEndDelay = null;
        }

        private IEnumerator ZoomDetection()
        {
            float previousDistance = 0f, distance = 0f;
            while(true)
            {
                distance = Vector2.Distance(gardenControl.Plant.FirstFingerPosition.ReadValue<Vector2>(),
                    gardenControl.Plant.SecondaryFingerPosition.ReadValue<Vector2>());
                //Detection
                //Zoom out
                if(distance > previousDistance)
                {
                    Vector3 targetPosition = camTransform.position;
                    targetPosition.z -= 1F;
                    //Camera.main.orthographicSize++;
                    camTransform.position = Vector3.Slerp(camTransform.position, 
                                                           targetPosition,
                                                           Time.deltaTime * cameraZoomSpeed);
                }
                //Zoom in
                else if(distance < previousDistance)
                {
                    Vector3 targetPosition = camTransform.position;
                    targetPosition.z += 1F;
                    //Camera.main.orthographicSize--;
                    //Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, Camera.main.orthographicSize--, Time.deltaTime);
                    camTransform.position = Vector3.Slerp(camTransform.position,
                                                          targetPosition,
                                                          Time.deltaTime * cameraZoomSpeed);
                }
                //Keep track of previous distance 
                Debug.Log("CAM TRANSFORM Z: " + camTransform.position.z);
                if(camTransform.position.z < -32F)
                {
                    camTransform.position = new Vector3(camTransform.position.x, camTransform.position.y, -32F);
                }
                if(camTransform.position.z > 6.87F)
                {
                    camTransform.position = new Vector3(camTransform.position.x, camTransform.position.y, 6.87F);
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
                if(twoFingers)
                {
                    rotatingScreen = false;
                    return;
                }
                Vector2 fingerPos = gardenControl.Plant.FirstFingerPosition.ReadValue<Vector2>();
                Vector3 screenCoordinates = new Vector3(fingerPos.x, fingerPos.y, cameraMain.nearClipPlane);
                screenCoordinates.z = 0.0F;
                currentRotatePosition = screenCoordinates;
                rotateDirection = previousRotatePosition - currentRotatePosition;
                float rotationAroundYAxis = -rotateDirection.x * rotateSpeed; //camera moves horizontally
                float rotationAroundXAxis = rotateDirection.y * rotateSpeed; //camera moves vertically
                //cam.transform.position = ground.transform.position;
                Debug.Log("Rotation around y axis: " + rotationAroundYAxis);
                Debug.Log("Rotation around x axis: " + rotationAroundXAxis);
                camFocusPoint.transform.RotateAround(camFocusPoint.transform.position, new Vector3(1, 0, 0), rotationAroundXAxis);
                camFocusPoint.transform.RotateAround(camFocusPoint.transform.position, new Vector3(0, 1, 0), rotationAroundYAxis);
                camFocusPoint.transform.eulerAngles = new Vector3(camFocusPoint.transform.eulerAngles.x, camFocusPoint.transform.eulerAngles.y, 0.0F);

                //Fix rotation point
                //FixRotationPoints(); 
                camFocusPoint.transform.eulerAngles = new Vector3(ClampAngle(camFocusPoint.transform.eulerAngles.x, 1F, 120F), camFocusPoint.transform.eulerAngles.y, camTransform.eulerAngles.z);
                //cam.transform.eulerAngles += new Vector3(12.312F, -4.502F, -0.004F);
                //ground.transform.Translate(new Vector3(0, 0, -2.76F));
                previousRotatePosition = currentRotatePosition;
                //ground.transform.eulerAngles = ground.transform.eulerAngles + 15 * new Vector3(-fingerPos.y, fingerPos.x, 0F);
            }
        }

        private void FixRotationPoints()
        {
            if (camFocusPoint.transform.localEulerAngles.x < 1F)
            {
                camFocusPoint.transform.eulerAngles = new Vector3(1F, camFocusPoint.transform.eulerAngles.y, camFocusPoint.transform.eulerAngles.z);
            }
            if (camFocusPoint.transform.localEulerAngles.x > 340F)
            {
                camFocusPoint.transform.eulerAngles = new Vector3(340F, camFocusPoint.transform.eulerAngles.y, camFocusPoint.transform.eulerAngles.z);
            }
        }
        float ClampAngle(float angle, float from, float to)
        {
            // accepts e.g. -80, 80
            if (angle < 0f) angle = 360 + angle;
            if (angle > 180f) return Mathf.Max(angle, 360 + from);
            return Mathf.Min(angle, to);
        }
        private IEnumerator SpinAfterRotate()
        {
            yield return new WaitForEndOfFrame();
            rotateStep -= Time.deltaTime / 5F;
            float rotationAroundYAxis = -rotateDirection.x * rotateStep; //camera moves horizontally
            camFocusPoint.transform.RotateAround(camFocusPoint.transform.position, new Vector3(0, 1, 0), rotationAroundYAxis);
            camFocusPoint.transform.eulerAngles = new Vector3(camFocusPoint.transform.eulerAngles.x, camFocusPoint.transform.eulerAngles.y, 0.0F);
            //FixRotationPoints();
            camFocusPoint.transform.eulerAngles = new Vector3(ClampAngle(camFocusPoint.transform.eulerAngles.x, 1F, 120F), camFocusPoint.transform.eulerAngles.y, camTransform.eulerAngles.z);
            if (rotateStep > 0.0F)
            {
                afterRotate = StartCoroutine(SpinAfterRotate());
            }
            else
            {
                afterRotate = null;
            }
        }
        private void StartDrag(InputAction.CallbackContext context)
        {
            //Debug.Log("START Drag " + context.ReadValue<float>());
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
            List<RaycastResult> results = new List<RaycastResult>();
            UIgraphicRaycaster.Raycast(pointerEventData, results);
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.name.Equals("MushroomDarkGreen"))
                { 
                    isDraggingSeed = true;
                    currSeed = PlantType.MushroomDarkGreen;
                }
                else if (result.gameObject.name.Equals("MushroomPink"))
                {
                    isDraggingSeed = true;
                    currSeed = PlantType.MushroomPink;
                }
            }
            if(!isDraggingSeed && !twoFingers && results.Count == 0)
            {
                rotatingScreen = true;
                if(afterRotate != null)
                {
                    StopCoroutine(afterRotate);
                    afterRotate = null;
                }
                rotateStep = rotateSpeed;
                previousRotatePosition = screenCoordinates;
            }
            
        }
        private void EndDrag(InputAction.CallbackContext context)
        {
            if(twoFingers)
            {
                rotatingScreen = false;
                indicator.gameObject.SetActive(false);
                return;
            }
            //Debug.Log("END DRAG!! " + gardenControl.Plant.FirstFingerPosition.ReadValue<Vector2>());
            if (isDraggingSeed)
            {
                AttemptPlant(currSeed);
            }
            isDraggingSeed = false;
            if(rotatingScreen)
            {
                afterRotate = StartCoroutine(SpinAfterRotate());
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

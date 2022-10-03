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
        private GardenControl gardenControl;
        private Camera cameraMain;
        private List<Seed> activeSeeds;
        [SerializeField] Image indicator;
        [SerializeField] Seed mushroom_darkgreen_UI;
        [SerializeField] Seed mushroom_pink_UI;
        [SerializeField] GameObject plantMenu;
        [SerializeField] GameObject plantMenu_Canvas;
        [SerializeField] EventSystem eventSystem;
        private GraphicRaycaster graphicRaycaster;
        private bool isOnPlantMenu = false;
        private PlantMenu menu;
        [HideInInspector] public Seed currSeed;
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
        [HideInInspector] public bool isDraggingSeed = false;
        void Awake()
        {
            graphicRaycaster = plantMenu_Canvas.GetComponent<GraphicRaycaster>();
            gardenControl = new GardenControl();
            cameraMain = Camera.main;
            activeSeeds = new List<Seed>();
            activeSeeds.Add(mushroom_darkgreen_UI);
            activeSeeds.Add(mushroom_pink_UI);
            PlantManager.AddPlant(new List<PlantType> { PlantType.MushroomDarkGreen, PlantType.MushroomPink });
        }

        private void Start()
        {
            gardenControl.Plant.Hold.started += ctx => StartDrag(ctx);
            gardenControl.Plant.Hold.canceled += ctx => EndDrag(ctx);
            gardenControl.Plant.Tap.started += ctx => StartTap(ctx);
            //gardenControl.Plant.Tap.canceled += ctx => EndTap(ctx);
            //gardenControl.Plant.Drag.started += ctx => StartDrag(ctx);
            //UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
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
                if (Resources.GetResourcesUsed() + PlantManager.resourceDict[currSeed.plantType] > 1.0F)
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
                        Collider[] collisions = Physics.OverlapSphere(hit.point, 0.05F, LayerMask.GetMask("Plant"));//activeSeeds[0].plantRadius);
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
        }

        private void StartDrag(InputAction.CallbackContext context)
        {
            //Debug.Log("START Drag " + context.ReadValue<float>());
            //Debug.Log("IS DRAGGING: " + activeSeeds[0].isDragging);
        }
        private void EndDrag(InputAction.CallbackContext context)
        {
            //Debug.Log("END DRAG!! " + gardenControl.Plant.FirstFingerPosition.ReadValue<Vector2>());
            if (isDraggingSeed)
            {
                foreach (Seed dragPlant in activeSeeds)
                {
                    if (dragPlant.isDragging)
                    {
                        AttemptPlant(dragPlant, dragPlant.plantType);
                        break;
                    }
                }
            }
            isDraggingSeed = false;
            indicator.gameObject.SetActive(false);
        }

        private bool AttemptPlant(Seed plant, PlantType type)
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
                Collider[] collisions = Physics.OverlapSphere(hit.point, 0.05F);
                if (hit.transform.gameObject.name.Equals("Ground") && collisions.Length == 1)
                {
                    GameObject newPlant = GameObject.Instantiate(plant.plant);
                    newPlant.transform.position = hit.point;
                    success = true;
                }
            }
            plant.ResetPosition();
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
           // if()
        }
    }
}

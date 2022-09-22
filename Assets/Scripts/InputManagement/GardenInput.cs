using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Planting
{
    public class GardenInput : MonoBehaviour
    {
        private GardenControl gardenControl;
        private Camera cameraMain;
        private List<Seed> activeSeeds;
        [SerializeField] UnityEngine.UI.Image indicator;
        [SerializeField] Seed mushroom_darkgreen_UI;
        [SerializeField] Seed mushroom_pink_UI;
        [SerializeField] GameObject plantMenu;
        private bool isOnPlantMenu = false;
        private GameObject menu;
        [HideInInspector] public bool isDraggingSeed = false;
        void Awake()
        {
            gardenControl = new GardenControl();
            cameraMain = Camera.main;
            activeSeeds = new List<Seed>();
            activeSeeds.Add(mushroom_darkgreen_UI);
            activeSeeds.Add(mushroom_pink_UI);
            GrowthManager.AddPlant(new List<PlantType> { PlantType.MushroomDarkGreen, PlantType.MushroomPink });
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
            if(isOnPlantMenu)
            {
                Debug.Log("IS ON PLANT MENU");
                Destroy(menu);
            }
            Debug.Log("Tap!");
            StartCoroutine(WaitForTap());
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
                    isOnPlantMenu = true;
                    menu = GameObject.Instantiate(plantMenu);
                    menu.transform.position = hit.transform.position + new Vector3(0F, 0.25F, 0F);
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
                        AttemptPlant(dragPlant);
                        break;
                    }
                }
            }
            isDraggingSeed = false;
            indicator.gameObject.SetActive(false);
        }

        private bool AttemptPlant(Seed plant)
        {
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

using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Simple Drag and Drop for UI objects with a RectTransform.
/// For 2d colliders, add a Physics Raycaster 2D component to your camera. Adapt the script to your needs.
/// For 3d colliders, add a Physics Raycaster component to your camera. Adapt the script to your needs.
/// There are several other interfaces you can implement for dragging such as IBeginDragHandler and IEndDragHandler.
/// To use with the new input system, make sure you have the package installed and in the Event System component replace the Standalone Input Module with the new one.
/// </summary>
namespace Planting
{
    [RequireComponent(typeof(RectTransform))]
    public class Seed : MonoBehaviour, IDragHandler
    {
        [SerializeField] public PlantType plantType;
        [SerializeField, Tooltip("Approximately the time it will take to reach the target. A smaller value will reach the target faster.")]
        private float dampingSpeed = .05f;

        private RectTransform draggingObjectRectTransform;
        private Vector3 velocity = Vector3.zero;
        private Vector3 startPos;
        [HideInInspector] public bool isDragging;
        [SerializeField] public float plantRadius;
        [SerializeField] public GameObject plant;
        private GardenInput gardenInput;

        private void Awake()
        {
            startPos = transform.localPosition;
            draggingObjectRectTransform = transform as RectTransform;
            gardenInput = GameObject.FindGameObjectWithTag("GardenInput").GetComponent<GardenInput>();
        }

        /// <summary>
        /// Called when the user drags the RectTransform across the screen. 
        /// </summary>
        /// <param name="eventData">Information regarding the UI event.</param>
        public void OnDrag(PointerEventData eventData)
        {
            gardenInput.isDraggingSeed = true;
            isDragging = true;
            // Convert the mouse position in screen coordinates to world coordinates with respect to the plane of the given RectTransform. Used to move the gameobject right under the mouse while dragging.
            //if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingObjectRectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePosition)) {
            //draggingObjectRectTransform.position = Vector3.SmoothDamp(draggingObjectRectTransform.position, globalMousePosition, ref velocity, dampingSpeed);
            //}
        }

        public void ResetPosition()
        {
            transform.localPosition = startPos;
            isDragging = false;
        }
    }
}

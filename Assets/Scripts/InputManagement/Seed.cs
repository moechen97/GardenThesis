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
    public class Seed : MonoBehaviour, IDragHandler
    {
        [SerializeField] public PlantType plantType;
        private Vector3 startPos;
        [HideInInspector] public bool isDragging;
        [SerializeField] public float plantRadius;
        [SerializeField] public GameObject plant;
        private GardenInput gardenInput;

        private void Awake()
        {
            startPos = transform.localPosition;
            gardenInput = GameObject.FindGameObjectWithTag("GardenInput").GetComponent<GardenInput>();
        }
        public void OnDrag(PointerEventData eventData)
        {
            gardenInput.isDraggingSeed = true;
            gardenInput.currSeed = this;
            isDragging = true;
        }
        public void ResetPosition()
        {
            transform.localPosition = startPos;
            isDragging = false;
        }
    }
}

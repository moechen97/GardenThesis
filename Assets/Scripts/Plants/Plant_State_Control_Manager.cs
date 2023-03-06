using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_State_Control_Manager : MonoBehaviour
{
    public static Plant_State_Control_Manager Instance;
    private List<Plant_StateControl> allPlants;
    private List<Plant_StateControl> interactingPlants;
    private int prevInteractionCount = 0;
    [SerializeField] private float minimumPlantVolume = 0.05f;
    [SerializeField] private float volumeUpTime = 6.895f;
    [SerializeField] private float volumeDownTime = 0.675f;
    private float timer = 0f;
    private bool volumeAdjustment = false;
    private enum Volume { Up, Down, None }
    private Volume volumeDirection = Volume.None;
    private Volume lastVolumeDirection = Volume.None;
    private float uniformVolume = 1f;
    private void Awake()
    {
        Instance = this;
        allPlants = new List<Plant_StateControl>();
        interactingPlants = new List<Plant_StateControl>();
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            Debug.Log("Uniform Volume: " + uniformVolume + " || INTERACT COUNT: " + interactingPlants.Count + " || " + "Volume direction: " + volumeDirection + " || Interactable plant count: " + interactingPlants.Count + " || Volume Adjustment: " + volumeAdjustment);
        }
        if (prevInteractionCount != interactingPlants.Count)
        {
            volumeAdjustment = true;
            if (interactingPlants.Count == 0)
            {
                volumeDirection = Volume.Up;
            }
            else
            {
                volumeDirection = Volume.Down;
            }

            if (lastVolumeDirection != volumeDirection)
            {
                if(volumeDirection == Volume.Down)
                {
                    timer = timer / volumeUpTime * volumeDownTime;
                }
                else
                {
                    timer = timer / volumeDownTime * volumeUpTime;
                }
            }
        }
        if (volumeAdjustment)
        {
            timer += Time.deltaTime;
            if (volumeDirection == Volume.Down)
            {
                uniformVolume = Mathf.Lerp(1f, minimumPlantVolume, (timer / volumeDownTime));
            }
            else if(volumeDirection == Volume.Up)
            {
                uniformVolume = Mathf.Lerp(minimumPlantVolume, 1f, (timer / volumeUpTime));
            }

            foreach (Plant_StateControl plant in allPlants)
            {
                if (plant.GetInteracting())
                {
                    plant.SetVolume(1f);
                }
                else
                {
                    plant.SetVolume(uniformVolume);
                }
            }

            Debug.Log("Uniform volume: " + uniformVolume + "|| Volume Direction: " + volumeDirection);
            if ((volumeDirection == Volume.Down && timer >= volumeDownTime) || (volumeDirection == Volume.Up && timer >= volumeUpTime))
            {
                Debug.Log("End Volume Adjustment");
                EndVolumeAdjustment();
            }
        }

        prevInteractionCount = interactingPlants.Count;
        lastVolumeDirection = volumeDirection;
    }
    private void EndVolumeAdjustment()
    {
        volumeAdjustment = false;
        timer = 0f;
    }
    public void AddPlant(Plant_StateControl plant)
    {
        allPlants.Add(plant);
    }
    public void RemovePlant(Plant_StateControl plant)
    {
        allPlants.Remove(plant);
    }
    public void AddInteractingPlant(Plant_StateControl plant)
    {
        interactingPlants.Add(plant);
    }
    public void RemoveInteractingPlant(Plant_StateControl plant)
    {
        interactingPlants.Remove(plant);
    }
    public float GetUniformVolume()
    {
        return uniformVolume;
    }
}

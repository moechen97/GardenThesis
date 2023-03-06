using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_State_Control_Manager : MonoBehaviour
{
    public static Plant_State_Control_Manager Instance;
    private List<Plant_StateControl> allPlants;
    private List<Plant_StateControl> interactingPlants;
    private int prevInteractionCount = 0;
    private float uniformVolume = 0f;
    [SerializeField] private float minimumPlantVolume = 0.05f;
    [SerializeField] private float volumeUpTime = 6.895f;
    [SerializeField] private float volumeDownTime = 0.675f;
    private void Awake()
    {
        Instance = this;
        allPlants = new List<Plant_StateControl>();
        interactingPlants = new List<Plant_StateControl>();
        foreach(Plant_StateControl plant in allPlants)
        {
            if(plant.interacting)
            {
                //AdjustVolumes = 1;
            }
            else
            {
                //volume = uniformVolume
            }
        }

        bool value for all plant
            1 plant is interacted: bool is true
            if no interact or 1 plant stop: 2 second countdown -> after, set bool value to false
            if bool is true: set initial plant sound volume to minimum
            else: set other plants sound volume to 
    }
    private void Update()
    {
        if (interactingPlants.Count != prevInteractionCount)
        {
            AdjustVolumes();
        }
        prevInteractionCount = interactingPlants.Count;
    }
    public void AdjustVolumes()
    {
        if (interactingPlants.Count == 0)
        {
            foreach (Plant_StateControl plant in allPlants)
            {
                plant.AdjustVolume(false);
            }
        }
        else
        {
            foreach (Plant_StateControl plant in allPlants)
            {
                plant.AdjustVolume(true);
            }
        }
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

    public bool IsInteractingPlant()
    {
        return interactingPlants.Count > 0;
    }
}

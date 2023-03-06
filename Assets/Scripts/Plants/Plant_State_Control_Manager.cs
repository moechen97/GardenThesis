using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_State_Control_Manager : MonoBehaviour
{
    public static Plant_State_Control_Manager Instance;
    private List<Plant_StateControl> allPlants;
    private List<Plant_StateControl> interactingPlants;
    private int prevInteractionCount = 0;
    private void Awake()
    {
        Instance = this;
        allPlants = new List<Plant_StateControl>();
        interactingPlants = new List<Plant_StateControl>();
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

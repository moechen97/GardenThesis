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
    private bool volumeDown = false;
    private bool lastVolumeDirection = false;
    private float uniformVolume = 1f;
    private void Awake()
    {
        Instance = this;
        allPlants = new List<Plant_StateControl>();
        interactingPlants = new List<Plant_StateControl>();
    }
    private void Update()
    {
        if (prevInteractionCount != interactingPlants.Count)
        {
            volumeAdjustment = true;
            if (interactingPlants.Count == 0)
            {
                volumeDown = false;
            }
            else
            {
                volumeDown = true;
            }

            if (lastVolumeDirection != volumeDown)
            {
                if(volumeDown)
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
            if (volumeDown)
            {
                uniformVolume = Mathf.Lerp(1f, minimumPlantVolume, (timer / volumeDownTime));
            }
            else
            {
                uniformVolume = Mathf.Lerp(minimumPlantVolume, 1f, (timer / volumeUpTime));
            }
            foreach (Plant_StateControl plant in allPlants)
            {
                if (plant.interacting)
                {
                    plant.SetVolume(1f);
                }
                else
                {
                    plant.SetVolume(uniformVolume);
                }
            }
            Debug.Log("Uniform volume: " + uniformVolume + "|| Volume Down: " + volumeDown);
            if ((volumeDown && uniformVolume == minimumPlantVolume) || (!volumeDown && uniformVolume == 1f))
            {
                Debug.Log("End Volume Adjustment");
                EndVolumeAdjustment();
            }
        }

        prevInteractionCount = interactingPlants.Count;
        lastVolumeDirection = volumeDown;
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

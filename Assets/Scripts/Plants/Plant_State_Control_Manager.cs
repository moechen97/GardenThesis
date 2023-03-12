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
        if (prevInteractionCount != interactingPlants.Count)
        {
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
                if(volumeDirection == Volume.Up)
                {
                    Debug.Log( "Uptimer" + timer + "," + timer / volumeDownTime);
                    timer = timer / volumeDownTime * volumeUpTime;
                    //timer = (1 - timer / volumeDownTime) * volumeUpTime;
                }
                else if(volumeDirection == Volume.Down)
                {
                    Debug.Log( "Downtimer" + + timer + "," + timer / volumeUpTime);
                    timer = timer / volumeUpTime * volumeDownTime;
                    //timer = (1 - timer / volumeUpTime) * volumeDownTime;
                }
            }
        }
        if (volumeDirection != Volume.None)
        {
            timer += Time.deltaTime;
            if (volumeDirection == Volume.Up)
            {
                uniformVolume = Mathf.Lerp(minimumPlantVolume, 1f, (timer / volumeUpTime));
            }
            else if(volumeDirection == Volume.Down)
            {
                uniformVolume = Mathf.Lerp(1f, minimumPlantVolume, (timer / volumeDownTime));
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
            if ((volumeDirection == Volume.Down && timer >= volumeDownTime) || (volumeDirection == Volume.Up && timer >= volumeUpTime))
            {
                EndVolumeAdjustment();
            }
        }
        prevInteractionCount = interactingPlants.Count;
        lastVolumeDirection = volumeDirection;
    }
    private void EndVolumeAdjustment()
    {
        timer = 0f;
        volumeDirection = Volume.None;
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

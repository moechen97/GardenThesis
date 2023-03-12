using System;
using System.Collections;
using System.Collections.Generic;
using Planting;
using UnityEngine;

public class SaveState
{
    public List<string> plants;
    public bool tutorialFinished;
    public string plantedPlantCounterDict;
    public string bredPlantCounterDict;
    private DateTime currLoginTime;
    public SaveState()
    {
        CreateNewSaveState();
    }
    private void CreateNewSaveState()
    {
        currLoginTime = DateTime.Now;
        if (!PlayerPrefs.HasKey("save"))
        {
            plants = new List<string>();
            tutorialFinished = false;
            plantedPlantCounterDict = "";
            bredPlantCounterDict = "";
            PlayerPrefs.SetString("firstLoginTime", currLoginTime.ToBinary().ToString());
            PlayerPrefs.SetString("prevLoginTime", currLoginTime.ToBinary().ToString());
        }
        CheckTimeReset();
    }
    public void AddPlant(PlantType plant)
    {
        Debug.Log("Add plant to save system: " + plant);
        plants.Add(plant.ToString());
        SaveManager.Instance.Save();
    }
    public void PrintState()
    {
        Debug.Log("~SaveState~");
        Debug.Log("Tutorial Is Complete: " + tutorialFinished);
        for (int i = 0; i < plants.Count; i++)
        {
            Debug.Log("Plant " + i +": " + plants[i]);
        }
        Debug.Log("Planted Plant Counter Dict: \n" + plantedPlantCounterDict);
        Debug.Log("Bred Plant Counter Dict: \n" + bredPlantCounterDict);
    }
    public void CheckTimeReset()
    {
        //DateTime firstLoginTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("firstLoginTime")));
        DateTime prevLoginTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("prevLoginTime")));
        TimeSpan timeSinceLastLogin = currLoginTime.Subtract(prevLoginTime);
        PlayerPrefs.SetString("prevLoginTime", currLoginTime.ToBinary().ToString());

        //Reset tutorial if time since last login >= 7 days
        if (timeSinceLastLogin.Days >= 7)
        {
            tutorialFinished = false;
        }
        Debug.Log("Plant County: " + plants.Count);
        //Reset stored plants 12 hours after last login
        if(timeSinceLastLogin.Seconds >= 12)
        {
            plants.Clear();
        }
    }
    public void UpdatePlantedPlantCounter(string dict)
    {
        Debug.Log("~Planted Plant Counter Dict Save - " + dict);
        plantedPlantCounterDict = dict;
    }
    public void UpdateBredPlantCounter(string dict)
    {
        Debug.Log("~Bred Plant Counter Dict Save - " + dict);
        bredPlantCounterDict = dict;
    }
}

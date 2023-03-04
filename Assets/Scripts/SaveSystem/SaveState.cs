using System;
using System.Collections;
using System.Collections.Generic;
using Planting;
using UnityEngine;

public class SaveState
{
    public List<string> plants;
    public bool tutorialFinished;
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
        Debug.Log("Tutorial Complete: " + tutorialFinished);
        for (int i = 0; i < plants.Count; i++)
        {
            Debug.Log("Plant " + i +": " + plants[i]);
        }
    }
    public void CheckTimeReset()
    {
        DateTime firstLoginTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("firstLoginTime")));
        DateTime prevLoginTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("prevLoginTime")));
        Debug.Log("FIRST LOGIN: " + firstLoginTime);
        Debug.Log("PREV LOGIN: " + prevLoginTime);
        Debug.Log("CURR LOGIN: " + currLoginTime);
        TimeSpan timeSinceLastLogin = currLoginTime.Subtract(prevLoginTime);

        //Reset tutorial if time since last login >= 7 days
        if (timeSinceLastLogin.Days >= 7)
        {
            tutorialFinished = false;
        }
        //Reset stored plants 12 hours after last login
        if(timeSinceLastLogin.Hours >= 12)
        {
            plants.Clear();
        }
        PlayerPrefs.SetString("prevLoginTime", currLoginTime.ToBinary().ToString());
    }
}

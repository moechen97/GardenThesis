using System;
using System.Collections;
using System.Collections.Generic;
using Planting;
using UnityEngine;

public class SaveState
{
    public List<string> plants;
    public bool tutorialFinished;
    public SaveState()
    {
        Debug.Log("CONSTRUCTOR!");
        CreateNewSaveState();
    }
    private void CreateNewSaveState()
    {
        if(PlayerPrefs.HasKey("save"))
        {
            Debug.Log("YOOYO");
        }
        else 
        {
            plants = new List<string>();
            tutorialFinished = false;
            PlayerPrefs.SetString("lastLoginTime", DateTime.Now.ToBinary().ToString());
        }

    }
    public void AddPlant(PlantType plant)
    {
        Debug.Log("Add plant " + plant);
        plants.Add(plant.ToString());
        SaveManager.Instance.Save();
    }
    public void PrintState()
    {
        Debug.Log("SaveState");
        Debug.Log("Tutorial Complete: " + tutorialFinished);
        for (int i = 0; i < plants.Count; i++)
        {
            Debug.Log("Plant " + i +": " + plants[i]);
        }
    }
    public void CheckTimeReset()
    {
        Debug.Log("Check time in save state");
        DateTime currLogin = DateTime.Now;
        DateTime prevLogin = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("lastLoginTime")));
        TimeSpan difference = currLogin.Subtract(prevLogin);
        if(difference.Days >= 7)
        {
            ResetSaveState();
        }
        Debug.Log("Difference since last login: " + difference);
        Debug.Log("Difference in seconds: " + difference.TotalSeconds);
    }
    private void ResetSaveState()
    {
        PlayerPrefs.DeleteAll();
        CreateNewSaveState();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { set; get; }
    public SaveState state;
    private DateTime currLoginTime;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();
        currLoginTime = DateTime.Now;
        //Debug.Log("STATE: " + SaveHelper.Serialize<SaveState>(state));
    }
    // Update is called once per frame
    public void Save()
    {
        PlayerPrefs.SetString("save", SaveHelper.Serialize<SaveState>(state));
    }

    public void Load()
    {
        if(PlayerPrefs.HasKey("save"))
        {
            //state = Deserialized class
            state = SaveHelper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
            Debug.Log("STATE:\n" + state.ToString());
            Debug.Log("Loading plants: " + state.plants.Count);
            //state.PrintState();
            StartCoroutine(CheckTime());
        }
        else
        {
            state = new SaveState();            
            Save();
            Debug.Log("No save file found, creating a new one!");
        }
    }
    public void TutorialFinished()
    {
        state.tutorialFinished = true;
        Save();
    }
    public void UpdatePlantedPlantCounter(string dict)
    {
        state.UpdatePlantedPlantCounter(dict);
        StartCoroutine(DelayedSave());
    }
    public void UpdateBredPlantCounter(string dict)
    {
        state.UpdateBredPlantCounter(dict);
        StartCoroutine(DelayedSave());
    }
    public void SaveDelay()
    {
        StartCoroutine(DelayedSave());
    }
    private IEnumerator DelayedSave()
    {
        yield return new WaitForFixedUpdate();
        Save();
    }

    private IEnumerator CheckTime()
    {
        yield return new WaitUntil(() => state.plants != null);        
        DateTime prevLoginTime;
        if (PlayerPrefs.HasKey("prevLoginTime"))
        {
            prevLoginTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("prevLoginTime")));
        }
        else
        {
            prevLoginTime = DateTime.Now;
        }
        TimeSpan timeSinceLastLogin = currLoginTime.Subtract(prevLoginTime);
        PlayerPrefs.SetString("prevLoginTime", currLoginTime.ToBinary().ToString());
        Debug.Log("~MADE IT~");
        //Reset tutorial if time since last login >= 7 days

        bool modify = false;
        if (timeSinceLastLogin.Days >= 7)
        {
            state.tutorialFinished = false;
            modify = true;
        }

        //Reset stored plants 12 hours after last login
        if (timeSinceLastLogin.Seconds >= 12)
        {
            Debug.Log("Cleared");
            state.plants.Clear();
            state.plantedPlantCounterDict = "";
            state.bredPlantCounterDict = "";
            modify = true;
        }

        if(modify)
        {
            Save();
        }
    }
}

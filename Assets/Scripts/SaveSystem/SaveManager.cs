using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { set; get; }
    public SaveState state;
    private DateTime currLoginTime;
    [HideInInspector] public bool hasCheckedReset = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        currLoginTime = DateTime.Now;
        Load();
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
            state = SaveHelper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
            StartCoroutine(CheckTime());
        }
        else
        {
            state = new SaveState();
            PlayerPrefs.SetString("firstLoginTime", currLoginTime.ToBinary().ToString());
            PlayerPrefs.SetString("lastTimePlayed", currLoginTime.ToBinary().ToString());
            Save();
            Debug.Log("No save file found, creating a new one!");
            hasCheckedReset = true;
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
        DateTime lastTimePlaying = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("lastTimePlaying")));
        TimeSpan timeSinceLastLogin = currLoginTime.Subtract(lastTimePlaying);
        PlayerPrefs.SetString("lastTimePlaying", currLoginTime.ToBinary().ToString());
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
            ClearSave();
            modify = true;
        }
        if(modify)
        {
            Save();
        }
        hasCheckedReset = true;
    }
    public void ClearSave()
    {
        state.plants.Clear();
        state.tutorialFinished = false;
        state.plantedPlantCounterDict = "";
        state.bredPlantCounterDict = "";
        Save();
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("lastTimePlaying", DateTime.Now.ToBinary().ToString());
    }

    //iOS
    private void OnApplicationFocus(bool focus)
    {
        PlayerPrefs.SetString("lastTimePlaying", DateTime.Now.ToBinary().ToString());
    }

    //iOS
    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetString("lastTimePlaying", DateTime.Now.ToBinary().ToString());
    }
}

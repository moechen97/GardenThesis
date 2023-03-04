using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { set; get; }
    public SaveState state;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();

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
            Debug.Log("Loading plants: " + state.plants.Count);
            state.PrintState();
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
}

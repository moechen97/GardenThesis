using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

namespace Planting
{
    public class UnlockablePlantManager : MonoBehaviour
    {
        public static bool unlockDisplayOpen = false;
        private Dictionary<PlantType, GameObject> unlockable_icons;
        [SerializeField] GameObject seedPanel;
        [SerializeField] private GameObject newSeedPanel;

        private int indexcount = 0;
        private Dictionary<Unlockable, GameObject> unlockables;
        private Unlockable lastUnlock = null;
        private void Awake()
        {
            unlockable_icons = GetComponent<UnlockableIconDictionaryScript>().DeserializeDictionary();
        }
        private void Start()
        {
            SaveManager.Instance.state.PrintState();
            if (SaveManager.Instance.state.plants.Count == 0)
            {
                SavePlantUnlock(PlantType.Fungus_Green);
            }
            //Add your unlocked plants to the game
            List<PlantType> unlockedPlants = new List<PlantType>();
            List<string> unlockedString = SaveManager.Instance.state.plants;
            foreach(string unlockedPlant in unlockedString)
            {
                PlantType type = (PlantType)System.Enum.Parse(typeof(PlantType), unlockedPlant);
                unlockedPlants.Add(type);
                SpawnPlantIcon(type, indexcount++, false);
            }
            unlockables = new Dictionary<Unlockable, GameObject>();
            List<KeyValuePair<PlantType, GameObject>> unlockablesList = unlockable_icons.ToList();
            foreach (KeyValuePair<PlantType, GameObject> unlockable in unlockablesList)
            {
                if(unlockedPlants.Contains(unlockable.Key))
                {
                    continue;
                }
                Unlockable u = null;
                if(unlockable.Key == PlantType.Plant_Peach)
                {
                    u = new Unlock_Plant_Peach(unlockable.Key);
                }
                else if(unlockable.Key == PlantType.Plant_Drum)
                {
                    u = new Unlock_Plant_Drum(unlockable.Key);
                }
                else if(unlockable.Key == PlantType.Plant_Spike)
                {
                    u = new Unlock_Plant_Spike(unlockable.Key);
                }
                else if(unlockable.Key == PlantType.Plant_Bubble)
                {
                    u = new Unlock_Plant_Bubble(unlockable.Key);
                }
                else if(unlockable.Key == PlantType.Plant_Capture)
                {
                    u = new Unlock_Plant_Capture(unlockable.Key);
                }
                else if(unlockable.Key == PlantType.Plant_Rings)
                {
                    u = new Unlock_Plant_Rings(unlockable.Key);
                }
                else if(unlockable.Key == PlantType.Plant_Lotus)
                {
                    u = new Unlock_Plant_Lotus(unlockable.Key);
                }
                else if(u == null) //Fungus_Purple
                {
                    continue;
                }
                unlockables.Add(u, unlockable.Value);
            }
            //Shuffle unlockables dictionary to make the unlock order more unpredictable 
            unlockables = unlockables.Shuffle();
        }
        public void UnlockCheck()
        {
            if(unlockDisplayOpen)
            {
                return;
            }
            bool unlocked = false;
            foreach (KeyValuePair<Unlockable, GameObject> unlockable in unlockables)
            {
                unlocked = unlockable.Key.CheckUnlock(lastUnlock);
                if (unlocked)
                {
                    UnlockPlant(unlockable.Key.ID);
                    unlockables.Remove(unlockable.Key);
                    lastUnlock = unlockable.Key;
                    break;
                }
            }
        }
        private void UnlockPlant(PlantType plant)
        {
            SavePlantUnlock(plant);
            SpawnPlantIcon(plant, indexcount++);
            string plantStr = plant.ToString();
            string plant_id = plantStr.Substring(plantStr.IndexOf('_') + 1);
            RecordAnalyticsData(Time.time, plant_id);
        }
        private void SavePlantUnlock(PlantType plant)
        {
            SaveManager.Instance.state.AddPlant(plant);
        }
        private void SpawnPlantIcon(PlantType plant, int index, bool showUnlockPanel = true)
        {
            GameObject icon = GameObject.Instantiate(unlockable_icons[plant]);
            icon.name = plant.ToString();
            icon.transform.parent = seedPanel.transform;
            icon.transform.SetSiblingIndex(index);
            icon.transform.localScale = new Vector3(1F, 1F, 1F);
            unlockable_icons.Remove(plant);
            //delete gray icon gameobject
            int i = seedPanel.transform.childCount - 1;
            Object.Destroy(seedPanel.transform.GetChild(i).gameObject);

            if (showUnlockPanel)
            {
                unlockDisplayOpen = true;
                //new seed unlock panel appear
                GameObject newPanel = Instantiate(newSeedPanel);
                newPanel.GetComponent<UINewSeedPanel>().GetNewSeedInfo(icon);
            }
            //add icon to PlantManager
            PlantManager.AddPlantIconBG(plant, icon.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>());
        }
        void RecordAnalyticsData(float time, string name)
        {
            Dictionary<string, object> analyticsData = new Dictionary<string, object>()
            {
                { "Plant name", name },
                { "Time", time }
            };
            AnalyticsResult a = Analytics.CustomEvent("unlockPlant", analyticsData);
        }
    }
}
public static class DictionaryExtensions
{
    public static Dictionary<TKey, TValue> Shuffle<TKey, TValue>(
       this Dictionary<TKey, TValue> source)
    {
        System.Random r = new System.Random();
        return source.OrderBy(x => r.Next())
           .ToDictionary(item => item.Key, item => item.Value);
    }
}
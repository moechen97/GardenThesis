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
        private Dictionary<PlantType, GameObject> unlockable_icons_copy;
        [SerializeField] GameObject seedPanel;
        [SerializeField] private GameObject newSeedPanel;
        [SerializeField] private GameObject grayIcon;

        private int indexcount = 0;
        private Dictionary<Unlockable, GameObject> unlockables;
        private Dictionary<Unlockable, GameObject> unlockables_copy;
        private Unlockable lastUnlock = null;
        private SaveManager saveManager;
        private bool hasStartedGame = false;
        public static UnlockablePlantManager Instance;
        public void ResetSave()
        {
            unlockable_icons = unlockable_icons_copy;
            unlockables = unlockables_copy;
            lastUnlock = null;
        }
        private void Awake()
        {
            Instance = this;
            unlockable_icons = GetComponent<UnlockableIconDictionaryScript>().DeserializeDictionary();
            unlockable_icons_copy = new Dictionary<PlantType, GameObject>(unlockable_icons);
        }

        private void Start()
        {
            saveManager = SaveManager.Instance;
            StartCoroutine(Initialize());
        }
        private IEnumerator Initialize()
        {
            yield return new WaitUntil(() => saveManager.hasCheckedReset);
            yield return new WaitUntil(() => hasStartedGame);
            if (saveManager.state.plants.Count == 0)
            {
                SavePlantUnlock(PlantType.Fungus_Green);
            }
            //Add your unlocked plants to the game
            List<PlantType> unlockedPlants = new List<PlantType>();
            List<string> unlockedString = saveManager.state.plants;
            foreach (string unlockedPlant in unlockedString)
            {
                PlantType type = (PlantType)System.Enum.Parse(typeof(PlantType), unlockedPlant);
                unlockedPlants.Add(type);
                SpawnPlantIcon(type, indexcount++, false, false);
            }
            for (int i = unlockedPlants.Count; i < PlantManager.activePlants.Count; i++)
            {
                SpawnGrayIcon();
            }
            unlockables = new Dictionary<Unlockable, GameObject>();
            unlockables_copy = new Dictionary<Unlockable, GameObject>();
            List<KeyValuePair<PlantType, GameObject>> unlockablesList = unlockable_icons.ToList();
            foreach (KeyValuePair<PlantType, GameObject> unlockable in unlockablesList)
            {
                Unlockable u = null;
                if (unlockable.Key == PlantType.Plant_Peach)
                {
                    u = new Unlock_Plant_Peach(unlockable.Key);
                }
                else if (unlockable.Key == PlantType.Plant_Drum)
                {
                    u = new Unlock_Plant_Drum(unlockable.Key);
                }
                else if (unlockable.Key == PlantType.Plant_Spike)
                {
                    u = new Unlock_Plant_Spike(unlockable.Key);
                }
                else if (unlockable.Key == PlantType.Plant_Bubble)
                {
                    u = new Unlock_Plant_Bubble(unlockable.Key);
                }
                else if (unlockable.Key == PlantType.Plant_Capture)
                {
                    u = new Unlock_Plant_Capture(unlockable.Key);
                }
                else if (unlockable.Key == PlantType.Plant_Rings)
                {
                    u = new Unlock_Plant_Rings(unlockable.Key);
                }
                else if (unlockable.Key == PlantType.Plant_Lotus)
                {
                    u = new Unlock_Plant_Lotus(unlockable.Key);
                }
                else if (u == null) //Fungus_Purple
                {
                    continue;
                }
                unlockables_copy.Add(u, unlockable.Value);
                if (!unlockedPlants.Contains(unlockable.Key))
                {
                    unlockables.Add(u, unlockable.Value);
                }
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
            saveManager.state.AddPlant(plant);
        }
        private void SpawnPlantIcon(PlantType plant, int index, bool newUnlock = true, bool showUnlockPanel = true)
        {
            //delete gray icon gameobject
            if (newUnlock)
            {
                GameObject.Destroy(seedPanel.transform.GetChild(seedPanel.transform.childCount - 1).gameObject);
            }

            //spawn in icon
            GameObject icon = GameObject.Instantiate(unlockable_icons[plant]);
            icon.name = plant.ToString();
            icon.transform.parent = seedPanel.transform;
            icon.transform.SetSiblingIndex(index);
            icon.transform.localScale = new Vector3(1F, 1F, 1F);
            unlockable_icons.Remove(plant);

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
        private void SpawnGrayIcon()
        {
            GameObject icon = GameObject.Instantiate(grayIcon);
            icon.transform.parent = seedPanel.transform;
            icon.transform.SetSiblingIndex(seedPanel.transform.childCount - 1);
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
        public void HasStartedGame()
        {
            hasStartedGame = true;
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
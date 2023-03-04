using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

namespace Planting {

    public class UnlockablePlants : MonoBehaviour
    {
        private Dictionary<PlantType, GameObject> unlockable_icons;
        [SerializeField] GameObject seedPanel;
        [SerializeField] private GameObject newSeedPanel;

        private int indexcount = 0;

        private void Awake()
        {
            unlockable_icons = GetComponent<UnlockableIconDictionaryScript>().DeserializeDictionary();
        }
        // Start is called before the first frame update
        void Start()
        {
            SaveManager.Instance.state.PrintState();
            if (SaveManager.Instance.state.plants.Count == 0)
            {
                Debug.Log("Saving in fungus green");
                SavePlantUnlock(PlantType.Fungus_Green);
            }
            //Add your unlocked plants to the game
            List<string> plants = SaveManager.Instance.state.plants;
            foreach (string plant in plants)
            {
                Debug.Log("ADD PLANT TO GAME: " + plant);
                SpawnPlantIcon((PlantType)System.Enum.Parse(typeof(PlantType), plant), indexcount++, false);
            }
        }

        // Update is called once per frame
        void Update()
        {


        }
        public void Unlock_Progress()
        {
            List<PlantType> unlocks = new List<PlantType>();
            foreach (KeyValuePair<PlantType, GameObject> unlockable in unlockable_icons.ToList())
            {
                if (unlockable.Key == PlantType.Plant_Peach)
                {
                    if (PlantManager.plantedPlantCounter[PlantType.Fungus_Green] >= 2 &&
                       PlantManager.bredPlantCounter[PlantType.Fungus_Green] >= 1)
                    {
                        SavePlantUnlock(PlantType.Plant_Peach);
                        SpawnPlantIcon(PlantType.Plant_Peach, indexcount++);
                        RecordAnalyticsData(Time.time, "Peach");
                    }
                }
                else if (unlockable.Key == PlantType.Plant_Drum)
                {
                    if (PlantManager.plantedPlantCounter[PlantType.Plant_Peach] >= 3 &&
                       PlantManager.bredPlantCounter[PlantType.Plant_Peach] >= 1)
                    {
                        SavePlantUnlock(PlantType.Plant_Drum);
                        SpawnPlantIcon(PlantType.Plant_Drum, indexcount++);
                        RecordAnalyticsData(Time.time, "Drum");
                    }
                }
                else if (unlockable.Key == PlantType.Plant_Spike)
                {
                    if (PlantManager.allPlants.Count > 15)
                    {
                        SavePlantUnlock(PlantType.Plant_Spike);
                        SpawnPlantIcon(PlantType.Plant_Spike, indexcount++);
                        RecordAnalyticsData(Time.time, "Spike");
                    }
                }
                else if (unlockable.Key == PlantType.Plant_Bubble)
                {
                    if (PlantManager.plantedPlantCounter[PlantType.Fungus_Green] >= 5 &&
                       PlantManager.bredPlantCounter[PlantType.Fungus_Green] >= 3)
                    {
                        SavePlantUnlock(PlantType.Plant_Bubble);
                        SpawnPlantIcon(PlantType.Plant_Bubble, indexcount++);
                        RecordAnalyticsData(Time.time, "Bubble");
                    }
                }
                else if (unlockable.Key == PlantType.Plant_Capture)
                {
                    if (PlantManager.plantedPlantCounter[PlantType.Plant_Bubble] >= 5 &&
                       PlantManager.bredPlantCounter[PlantType.Plant_Bubble] >= 1)
                    {
                        SavePlantUnlock(PlantType.Plant_Capture);
                        SpawnPlantIcon(PlantType.Plant_Capture, indexcount++);
                        RecordAnalyticsData(Time.time, "Capture");
                    }
                }
                else if (unlockable.Key == PlantType.Plant_Rings)
                {
                    if (PlantManager.plantedPlantCounter[PlantType.Plant_Peach] >= 5 &&
                       PlantManager.bredPlantCounter[PlantType.Plant_Peach] >= 2)
                    {
                        SavePlantUnlock(PlantType.Plant_Rings);
                        SpawnPlantIcon(PlantType.Plant_Rings, indexcount++);
                        RecordAnalyticsData(Time.time, "Rings");
                    }
                }
                else if (unlockable.Key == PlantType.Plant_Lotus)
                {
                    if (PlantManager.plantedPlantCounter[PlantType.Plant_Drum] >= 4 &&
                       PlantManager.bredPlantCounter[PlantType.Fungus_Green] >= 0)
                    {
                        SavePlantUnlock(PlantType.Plant_Lotus);
                        SpawnPlantIcon(PlantType.Plant_Lotus, indexcount++);
                        RecordAnalyticsData(Time.time, "Lotus");
                    }
                }
            }
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
            AnalyticsResult a = Analytics.CustomEvent("unlockPlant",analyticsData);
        }
    }
}

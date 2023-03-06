using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Planting
{
    public enum PlantType
    {
        MushroomDarkGreen, MushroomPink, Fungus_Green, Fungus_Jelly, Fungus_Purple, Plant_Peach,
        Plant_Drum, Plant_Spike, Plant_Bubble, Plant_Capture, Plant_Rings, Plant_Lotus
    }
    public class PlantManager : MonoBehaviour
    {
        public static PlantManager instance;
        private SaveManager saveManager;
        public static Transform PlantTransform { get; private set; }
        public static Transform SpikeTransform { get; private set; }

        //plant counting for unlockables
        public static Dictionary<PlantType, int> plantCounter = new Dictionary<PlantType, int>();
        public static Dictionary<PlantType, int> plantedPlantCounter;
        public static Dictionary<PlantType, int> bredPlantCounter;

        public static float plantLifeFactor = 1;
        private void Awake()
        {
            instance = this;
            PlantTransform = GameObject.FindGameObjectWithTag("Plants").transform;
            SpikeTransform = GameObject.FindGameObjectWithTag("Spikes").transform;
        }
        private UnlockablePlantManager unlockablePlantManager;
        //list of the plants used in game currently
        private static List<PlantType> activePlants = new List<PlantType> { PlantType.Fungus_Green, PlantType.Plant_Peach, PlantType.Plant_Drum,
                                            PlantType.Plant_Spike, PlantType.Plant_Bubble, PlantType.Plant_Capture, PlantType.Plant_Rings, PlantType.Plant_Lotus };
        private void Start()
        {
            GameEvents.current.onPlantFullyGrownTrigger += FullyGrownPlant;
            unlockablePlantManager = GetComponent<UnlockablePlantManager>();
            saveManager = SaveManager.Instance;
            plantedPlantCounter = JsonConvert.DeserializeObject<Dictionary<PlantType, int>>(saveManager.state.plantedPlantCounterDict);
            bredPlantCounter = JsonConvert.DeserializeObject<Dictionary<PlantType, int>>(saveManager.state.bredPlantCounterDict);
            if (plantedPlantCounter == null)
            {
                plantedPlantCounter = new Dictionary<PlantType, int>();
            }
            if (bredPlantCounter == null)
            {
                bredPlantCounter = new Dictionary<PlantType, int>();
            }
            foreach(PlantType type in activePlants)
            {
                SetPlantCounter(type);
            }
        }
        private void SetPlantCounter(PlantType type)
        {
            if(!plantedPlantCounter.ContainsKey(type))
            {
                plantCounter[type] = 0;
                plantedPlantCounter[type] = 0;
                bredPlantCounter[type] = 0;
            }
        }
        public static Dictionary<PlantType, float> resourceDict = new
            Dictionary<PlantType, float>() {
                { PlantType.MushroomDarkGreen, 0.05F },
                { PlantType.MushroomPink, 0.01F },
                { PlantType.Fungus_Green , 0.05f},
                { PlantType.Fungus_Jelly , 0.05f },
                { PlantType.Fungus_Purple, 0.01f},
                { PlantType.Plant_Peach, 0.05F},
                { PlantType.Plant_Drum, 0.1F },
                { PlantType.Plant_Spike, 0.0F },
                { PlantType.Plant_Bubble, 0.05F },
                { PlantType.Plant_Capture, 0.05F },
                { PlantType.Plant_Rings, 0.05F },
                { PlantType.Plant_Lotus, 0.1F }
            };

        public static Dictionary<PlantType, Image> seedIconBGs = new Dictionary<PlantType, Image>();

        public static Dictionary<PlantType, int> maxPlants = new
            Dictionary<PlantType, int>()
        {
            { PlantType.MushroomDarkGreen, 20 },
            { PlantType.MushroomPink, 20 },
            { PlantType.Fungus_Green , 20 },
            { PlantType.Fungus_Jelly , 40 },
            { PlantType.Fungus_Purple, 20 },
            { PlantType.Plant_Peach, 20},
            { PlantType.Plant_Drum, 5 },
            { PlantType.Plant_Spike, 100},
            { PlantType.Plant_Bubble, 20 },
            { PlantType.Plant_Capture, 20 },
            { PlantType.Plant_Rings, 20 },
            { PlantType.Plant_Lotus, 10 }
        };
        public static List<Plant> allPlants = new List<Plant>();
        public static bool CanSpawnPlantBreed(PlantType type)
        {
            //if(Resources.GetResourcesUsed() + resourceDict[type] > 1.0F)
            //{
            //    return false;
            //}
            float random = Random.value;
            //if (type == PlantType.MushroomDarkGreen)
            //{
                //randomize mushroom spawning validity
                if(plantCounter[type] < 5)
                {
                    return true;
                }
                if (random < 1.0F - 0.1 * plantCounter[type])
                {
                    return true;
                }
            //}
            return false;
        }
        public static int GetNumPlants(PlantType type)
        {
            return plantCounter[type];
        }
        public static bool CanBreedPlant(PlantType type)
        {
            return plantCounter[type] < maxPlants[type];
        }
        public static void IncrementPlant(PlantType type, Plant plant, bool isBred = false)
        {
            allPlants.Add(plant);
            plantCounter[type]++;
            Resources.IncrementResources(resourceDict[type],plant.ReturnHue());
        }
        public static void DecrementPlant(PlantType type, Plant plant)
        {
            allPlants.Remove(plant);
            plantCounter[type]--;
            Resources.DecrementResources(resourceDict[type],plant.ReturnHue());
        }

        //tracker for unlocking
        public void FullyGrownPlant(PlantType type, bool isBred = false)
        {
            if (isBred)
            {
                bredPlantCounter[type]++;
                saveManager.UpdateBredPlantCounter(JsonConvert.SerializeObject(bredPlantCounter));
                Debug.Log("Bred plant counter - " + type + ": " + bredPlantCounter[type]);
            }
            else
            {
                plantedPlantCounter[type]++;
                saveManager.UpdatePlantedPlantCounter(JsonConvert.SerializeObject(plantedPlantCounter));
                Debug.Log("Planted plant counter - " + type + ": " + plantedPlantCounter[type]);
            }
            unlockablePlantManager.UnlockCheck();
        }
        public static void UpdatePlantsAnimationSpeed(float speedFactor)
        {
            foreach(Plant plant in allPlants)
            {
                plant.UpdateAnimationSpeed(speedFactor);
            }
        }   
        public static void UpdatePlantsLifeSpeed(float speedFactor)
        {
            plantLifeFactor = speedFactor;
            foreach(Plant plant in allPlants)
            {
                plant.UpdateLifeTime(speedFactor);
            }
        }
        public static void AddPlantIconBG(PlantType plant, Image icon)
        {
            seedIconBGs.Add(plant, icon);
        }

        public static Color iconGray = new Color(243F / 255F, 234F / 255F, 219F / 255F,0.5f);
        public static Color iconWhite = new Color(243F / 255F, 234F / 255F, 219F / 255F);
        public static void SelectPlantIcon(PlantType plant, bool selected)
        {
            Image bg = seedIconBGs[plant];
            if(selected)
            {
                bg.color = iconGray;
            }
            else
            {
                bg.color = iconWhite;
            }
        }
    }
}
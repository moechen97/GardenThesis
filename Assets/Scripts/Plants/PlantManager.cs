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
        public static Transform PlantTransform { get; private set; }
        public static Transform SpikeTransform { get; private set; }

        public static float plantLifeFactor = 1;
        private void Awake()
        {
            instance = this;
            PlantTransform = GameObject.FindGameObjectWithTag("Plants").transform;
            SpikeTransform = GameObject.FindGameObjectWithTag("Spikes").transform;
        }
        private UnlockablePlants unlockablePlants;
        private void Start()
        {
            GameEvents.current.onPlantFullyGrownTrigger += FullyGrownPlant;
            unlockablePlants = GetComponent<UnlockablePlants>();
        }
        public static Dictionary<PlantType, int> plantCounter = new Dictionary<PlantType, int>();
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
        //public static int num_MushroomDarkGreen = 0;
        //public static int max_MushroomDarkGreen = 10;
        //public static int num_MushroomPink = 0;
        //public static int max_Fungus_Green = 10;
        //public static int max_Fungus_Jelly = 15;

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

        //for unlocks
        public static Dictionary<PlantType, int> plantedPlantCounter = new Dictionary<PlantType, int>();
        public static Dictionary<PlantType, int> bredPlantCounter = new Dictionary<PlantType, int>();
        public static void AddPlant(PlantType type)
        {
            plantCounter[type] = 0;
            plantedPlantCounter[type] = 0;
            bredPlantCounter[type] = 0;
        }
        public static bool CanSpawnPlantBreed(PlantType type)
        {
            if(Resources.GetResourcesUsed() + resourceDict[type] > 1.0F)
            {
                return false;
            }
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
            //Debug.Log("Plant counter - " + type + ": " + plantCounter[type]);
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
                Debug.Log("Bred plant counter - " + type + ": " + bredPlantCounter[type]);
            }
            else
            {
                plantedPlantCounter[type]++;
                Debug.Log("Planted plant counter - " + type + ": " + plantedPlantCounter[type]);
            }
            unlockablePlants.Unlock_Progress();
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
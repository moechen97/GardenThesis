using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                { PlantType.MushroomDarkGreen, 0.1F },
                { PlantType.MushroomPink, 0.05F },
                { PlantType.Fungus_Green , 0.1f},
                { PlantType.Fungus_Jelly , 0.05f },
                { PlantType.Fungus_Purple, 0.05f},
                { PlantType.Plant_Peach, 0.1F},
                { PlantType.Plant_Drum, 0.3F },
                { PlantType.Plant_Spike, 0.0F },
                { PlantType.Plant_Bubble, 0.2F },
                { PlantType.Plant_Capture, 0.1F },
                { PlantType.Plant_Rings, 0.1F },
                { PlantType.Plant_Lotus, 0.3F }
            };

        //public static int num_MushroomDarkGreen = 0;
        //public static int max_MushroomDarkGreen = 10;
        //public static int num_MushroomPink = 0;
        //public static int max_Fungus_Green = 10;
        //public static int max_Fungus_Jelly = 15;
        
        public static Dictionary<PlantType, int> maxPlants = new
            Dictionary<PlantType, int>()
        {
            { PlantType.MushroomDarkGreen, 10 },
            { PlantType.MushroomPink, 20 },
            { PlantType.Fungus_Green , 10 },
            { PlantType.Fungus_Jelly , 20 },
            { PlantType.Fungus_Purple, 20 },
            { PlantType.Plant_Peach, 20},
            { PlantType.Plant_Drum, 5 },
            { PlantType.Plant_Spike, 100},
            { PlantType.Plant_Bubble, 6 },
            { PlantType.Plant_Capture, 10 },
            { PlantType.Plant_Rings, 10 },
            { PlantType.Plant_Lotus, 3 }
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
            Resources.IncrementProgress(resourceDict[type]);
        }
        public static void DecrementPlant(PlantType type, Plant plant)
        {
            //plantCounter[type]--;
            allPlants.Remove(plant);
            Resources.DecrementProgress(resourceDict[type]);
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
    }
}
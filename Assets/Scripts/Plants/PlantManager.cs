using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting
{
    public enum PlantType { MushroomDarkGreen, MushroomPink, Fungus_Green, Fungus_Jelly, Fungus_Purple }
    public class PlantManager : MonoBehaviour
    {
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
                { PlantType.Fungus_Purple, 0.05f}
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
            { PlantType.Fungus_Purple, 20 }
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
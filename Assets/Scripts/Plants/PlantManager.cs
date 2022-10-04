using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting
{
    public enum PlantType { MushroomDarkGreen, MushroomPink }
    public static class PlantManager
    {
        public static Dictionary<PlantType, int> plantCounter = new Dictionary<PlantType, int>();
        public static Dictionary<PlantType, float> resourceDict = new 
            Dictionary<PlantType, float>() { { PlantType.MushroomDarkGreen, 0.1F }, { PlantType.MushroomPink, 0.05F } };
        public static int num_MushroomDarkGreen = 0;
        public static int max_MushroomDarkGreen = 10;
        public static int num_MushroomPink = 0;
        public static List<Plant> allPlants = new List<Plant>();

        public static void AddPlant(List<PlantType> type)
        {
            //keep counter of each plant
            foreach (PlantType plantType in type)
            {
                plantCounter[plantType] = 0;
            }
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
            if (type == PlantType.MushroomDarkGreen)
            {
                return plantCounter[type] < max_MushroomDarkGreen;
            }
            return false;
        }

        public static void IncrementPlant(PlantType type, Plant plant)
        {
            plantCounter[type]++;
            allPlants.Add(plant);
            Resources.IncrementProgress(resourceDict[type]);
        }
        public static void DecrementPlant(PlantType type, Plant plant)
        {
            plantCounter[type]--;
            allPlants.Remove(plant);
            Resources.DecrementProgress(resourceDict[type]);
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
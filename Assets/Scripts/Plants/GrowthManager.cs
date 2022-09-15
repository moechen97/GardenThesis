using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GrowthManager
{
    public static int num_MushroomDarkGreen = 0;
    public static int max_MushroomDarkGreen = 10;
    public static int num_MushroomPink = 0;
    public static bool SpawnPlantBreed(string id)
    {
        float random = Random.value;
        if (id.Equals("Mushroom_DarkGreen"))
        {
            if (random < 1.0F - 0.1 * num_MushroomDarkGreen)
            {
                return true;
            }
        }
        return false;
    }

    public static int GetNumPlants(string id)
    {
        if(id.Equals("Mushroom_DarkGreen"))
        {
            return num_MushroomDarkGreen;
        }
        return 0;
    }

    public static bool CanBreedPlant(string id)
    {
        if (id.Equals("Mushroom_DarkGreen"))
        {
            return num_MushroomDarkGreen < max_MushroomDarkGreen;
        }
        return false;
    }

    public static void DecrementPlant(string id)
    {
        if (id.Equals("Mushroom_DarkGreen"))
        {
            num_MushroomDarkGreen--;
        }
    }
}

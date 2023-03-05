using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting
{
    public abstract class Unlockable
    {
        public PlantType ID;
        public Unlockable(PlantType type)
        {
            ID = type;
        }
        public virtual bool CheckUnlock(Unlockable lastUnlock)
        {
            if(lastUnlock == null || PlantManager.plantedPlantCounter[lastUnlock.ID] == 0)
            {
                return false;
            }
            return true;
        }
    }

    public class Unlock_Plant_Peach : Unlockable
    {
        public Unlock_Plant_Peach(PlantType type) : base(type) { }
        public override bool CheckUnlock(Unlockable lastUnlock)
        {
            if(base.CheckUnlock(lastUnlock))
            {
                if (PlantManager.plantedPlantCounter[PlantType.Fungus_Green] >= 2 &&
                PlantManager.bredPlantCounter[PlantType.Fungus_Green] >= 1)
                {
                    return true;
                }
            }           
            return false;
        }
    }
    public class Unlock_Plant_Drum : Unlockable
    {
        public Unlock_Plant_Drum(PlantType type) : base(type) { }
        public override bool CheckUnlock(Unlockable lastUnlock)
        {
            if (base.CheckUnlock(lastUnlock))
            {
                if (PlantManager.plantedPlantCounter[PlantType.Plant_Peach] >= 3 &&
                       PlantManager.bredPlantCounter[PlantType.Plant_Peach] >= 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public class Unlock_Plant_Spike : Unlockable
    {
        public Unlock_Plant_Spike(PlantType type) : base(type) { }
        public override bool CheckUnlock(Unlockable lastUnlock)
        {
            if (base.CheckUnlock(lastUnlock))
            {
                if (PlantManager.allPlants.Count > 15)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class Unlock_Plant_Bubble : Unlockable
    {
        public Unlock_Plant_Bubble(PlantType type) : base(type) { }
        public override bool CheckUnlock(Unlockable lastUnlock)
        {
            if (base.CheckUnlock(lastUnlock))
            {
                if (PlantManager.plantedPlantCounter[PlantType.Fungus_Green] >= 5 &&
                       PlantManager.bredPlantCounter[PlantType.Fungus_Green] >= 3)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public class Unlock_Plant_Capture : Unlockable
    {
        public Unlock_Plant_Capture(PlantType type) : base(type) { }
        public override bool CheckUnlock(Unlockable lastUnlock)
        {
            if (base.CheckUnlock(lastUnlock))
            {
                if (PlantManager.plantedPlantCounter[PlantType.Plant_Bubble] >= 5 &&
                       PlantManager.bredPlantCounter[PlantType.Plant_Bubble] >= 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public class Unlock_Plant_Rings : Unlockable
    {
        public Unlock_Plant_Rings(PlantType type) : base(type) { }
        public override bool CheckUnlock(Unlockable lastUnlock)
        {
            if (base.CheckUnlock(lastUnlock))
            {
                if (PlantManager.plantedPlantCounter[PlantType.Plant_Peach] >= 5 &&
                      PlantManager.bredPlantCounter[PlantType.Plant_Peach] >= 2)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public class Unlock_Plant_Lotus : Unlockable
    {
        public Unlock_Plant_Lotus(PlantType type) : base(type) { }
        public override bool CheckUnlock(Unlockable lastUnlock)
        {
            if (base.CheckUnlock(lastUnlock))
            {
                if (PlantManager.plantedPlantCounter[PlantType.Plant_Drum] >= 4 &&
                       PlantManager.bredPlantCounter[PlantType.Fungus_Green] >= 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
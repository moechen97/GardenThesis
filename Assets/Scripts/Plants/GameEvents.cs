using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting
{
    public class GameEvents : MonoBehaviour
    {
        public static GameEvents current;
        void Awake()
        {
            current = this;
        }

        public event Action<PlantType, bool> onPlantFullyGrownTrigger;
        public void PlantFullyGrown(PlantType type, bool isBred)
        {
            if (onPlantFullyGrownTrigger != null)
            {
                onPlantFullyGrownTrigger(type, isBred);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting
{
    public class Fungus_Green : Plant
    {
        [SerializeField, Tooltip("Minimum height")] float height_minRange = 0.65F;
        [SerializeField, Tooltip("Maximum height")] float height_maxRange = 1.0F;
        [SerializeField, Tooltip("X/Y Scale")] float XZScale = 1F;
        protected override void Start()
        {
            transform.localScale = new Vector3(XZScale, Random.Range(height_minRange, height_maxRange), XZScale);
            id = PlantType.Fungus_Green;
            finishGrowAnimationName = "Fungus_Fully_Grow";
            base.Start(); 
            //Debug.Log("Mushroom Dark Green: " + PlantManager.plantCounter[id]);
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}

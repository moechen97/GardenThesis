using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting
{
    public class Mushroom_DarkGreen : Plant
    {
        [SerializeField, Tooltip("Minimum height")] float height_minRange = 0.65F;
        [SerializeField, Tooltip("Maximum height")] float height_maxRange = 1.0F;
        [SerializeField, Tooltip("X/Y Scale")] float XZScale = 10F;
        protected override void Start()
        {
            transform.parent.localScale = new Vector3(XZScale, Random.Range(height_minRange, height_maxRange), XZScale);
            id = PlantType.MushroomDarkGreen;
            finishGrowAnimationName = "Fungus_Stem_Darkgreen_FinishGrow";
            base.Start(); 
            //Debug.Log("Mushroom Dark Green: " + PlantManager.plantCounter[id]);
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}
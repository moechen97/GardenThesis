using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting
{
    public class Fungus_Jelly : Plant
    {
        [SerializeField, Tooltip("Minimum height")] float height_minRange = 0.6F;
        [SerializeField, Tooltip("Maximum height")] float height_maxRange = 1.2F;
        [SerializeField, Tooltip("X/Y Scale")] float XZScale = 1F;
        protected override void Start()
        {
            transform.localScale = new Vector3(XZScale, Random.Range(height_minRange, height_maxRange), XZScale);
            id = PlantType.Fungus_Jelly;
            finishGrowAnimationName = "ThinStem_FinishGrowth";
            base.Start(); 
            
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}


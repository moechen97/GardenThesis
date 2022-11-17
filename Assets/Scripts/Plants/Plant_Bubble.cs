using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting
{
    public class Plant_Bubble : Plant
    {
        [SerializeField, Tooltip("Minimum height")] float height_minRange = 0.6F;
        [SerializeField, Tooltip("Maximum height")] float height_maxRange = 1.0F;
        [SerializeField, Tooltip("X/Y Scale")] float XZScale = 1F;
        protected override void Start()
        {
            float randomRotateY = Random.Range(0f, 360f);
            transform.rotation = Quaternion.Euler(new Vector3(0,randomRotateY,0));
            transform.localScale = new Vector3(XZScale, Random.Range(height_minRange, height_maxRange), XZScale);
            id = PlantType.Plant_Bubble;
            finishGrowAnimationName = "Plant_Bubble_FullyGrow";
            base.Start(); 
            //Debug.Log("Mushroom Dark Green: " + PlantManager.plantCounter[id]);
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}


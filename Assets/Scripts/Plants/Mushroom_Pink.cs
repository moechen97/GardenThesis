using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting
{
    public class Mushroom_Pink : Plant
    {
        protected override void Start()
        {
            id = PlantType.MushroomPink;
            finishGrowAnimationName = "Fungus_Stem_White_FinishGrow";
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}


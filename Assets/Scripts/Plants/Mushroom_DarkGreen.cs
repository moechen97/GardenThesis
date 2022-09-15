using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_DarkGreen : Plant
{
    protected override void Start()
    {
        id = "Mushroom_DarkGreen";
        finishGrowAnimationName = "Fungus_Stem_Darkgreen_FinishGrow";
        GrowthManager.num_MushroomDarkGreen++;
        Debug.Log("Mushroom Dark Green: " + GrowthManager.num_MushroomDarkGreen);
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_Pink : Plant
{
    protected override void Start()
    {
        id = "Mushroom_Pink";
        finishGrowAnimationName = "Fungus_Stem_White_FinishGrow";
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}

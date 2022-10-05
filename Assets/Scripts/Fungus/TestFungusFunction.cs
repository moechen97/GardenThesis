using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFungusFunction : MonoBehaviour
{
    [SerializeField] private bool isWithered = false;
    [SerializeField] private bool isExploded = false;

    [SerializeField] private Fungus_MaterialChange testedFungus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isWithered)
        {
            testedFungus.Withered();
            isWithered = false;
        }

        if (isExploded)
        {
            testedFungus.Exploded();
            isExploded = false;
        }
    }
}

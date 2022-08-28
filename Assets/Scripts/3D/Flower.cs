using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] private Transform seedManager;
    [SerializeField] GameObject seedPrefab;
    private GameObject currentSeed;
    private bool hasSeed;
    [SerializeField] private float seedCooldown = 5.0F;
    private float seedTimer;
    [SerializeField] Vector3 seedPosition;
    void Start()
    {
        hasSeed = false;
        seedTimer = 0.0F;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasSeed)
        {
            seedTimer += Time.deltaTime;
            if(seedTimer >= seedCooldown)
            {
                currentSeed = GameObject.Instantiate(seedPrefab, transform);
                currentSeed.transform.localPosition = seedPosition;
                currentSeed.transform.parent = seedManager;
                currentSeed.transform.localScale = new Vector3(0.8497207F, 0.8497207F, 0.8497207F);
                seedTimer = 0.0F;
            }
        }
    }
}

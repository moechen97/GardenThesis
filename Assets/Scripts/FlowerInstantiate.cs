using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlowerInstantiate : MonoBehaviour
{
    [SerializeField] private float minDistance;
    [SerializeField] private GameObject Seed;
    [SerializeField] private float maxSeed;
    private bool generateSeed = false;
    private Vector3 MousePosition;
    private int seedCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (generateSeed)
            return;
        MousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
        float distance = Vector3.Distance(transform.position, MousePosition);
        if (distance < minDistance)
        {
            Vector2 randomVector2 = Random.insideUnitCircle.normalized;
            Vector3 randomVector3 = new Vector3(randomVector2.x, randomVector2.y, 0)*0.2f;
            Instantiate(Seed, transform.position+randomVector3, quaternion.identity);
            seedCount++;
        }

        if (seedCount >= maxSeed)
        {
            generateSeed = true;
        }
    }
}

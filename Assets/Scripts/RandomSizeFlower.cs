using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSizeFlower : MonoBehaviour
{
    public Gradient colorGradient;

    [SerializeField] private SpriteRenderer[] petals;
    
    
    // Start is called before the first frame update
    void Start()
    {
        float randomsize = Random.Range(0.6f, 1.2f);
        transform.localScale = Vector3.one * randomsize;
        float randomColor = Random.Range(0f, 1f);
        foreach (var VARIABLE in petals)
        {
            VARIABLE.color = colorGradient.Evaluate(randomColor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

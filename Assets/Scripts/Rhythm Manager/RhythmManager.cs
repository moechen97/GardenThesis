using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    [SerializeField] private SingleLand[] Lands;

    [SerializeField] private float lagTime;

    private int count = 0;
    private float timeRecord = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timeRecord > lagTime)
        {
            Lands[count].ActiveLand();
            if (count < Lands.Length-1)
            {
                count++;
            }
            else
            {
                count = 0;
            }

            timeRecord = Time.time;
        }
    }
}

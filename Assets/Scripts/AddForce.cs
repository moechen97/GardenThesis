using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField] private GameObject TouchHint;

    private GameObject generatedHint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
            generatedHint = Instantiate(TouchHint, mousePosition, quaternion.identity);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            generatedHint.GetComponent<TouchPoint>().DeleteItself();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Blow : MonoBehaviour
{
    [SerializeField] private GameObject WindZone;
    [SerializeField] private float deltaTimeLength;

    private Vector3 lastPos;
    private Vector3 delta;
    private Vector3 mouseWorldPosition;
    private float lastTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DrawWind();
    }

    void DrawWind()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPos = Input.mousePosition;
            lastTime = Time.time;
        }
        else if (Input.GetMouseButton(0))
        {
            if (Time.time - lastTime > deltaTimeLength)
            {
                delta = Input.mousePosition - lastPos;
                lastPos = Input.mousePosition;
                lastTime = Time.time;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            delta = Input.mousePosition - lastPos;
            Debug.Log(delta);
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
            if (delta != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward,delta);
                Instantiate(WindZone, mouseWorldPosition, toRotation);
            }
        }
    }
}

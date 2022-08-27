using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotataeFollowMouse : MonoBehaviour
{
    [SerializeField] private float deltaTimeLength;

    private Vector3 lastPos;
    private Vector3 delta;
    private Vector3 mouseWorldPosition;
    private float lastTime = 0;
    private Quaternion toRotation;
    private Quaternion _face;
    
    // Start is called before the first frame update
    void Start()
    {
        _face = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPos = Input.mousePosition;
            lastTime = Time.time;
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
            transform.position = mouseWorldPosition;
        }
        else if (Input.GetMouseButton(0))
        {
            if (Time.time - lastTime > deltaTimeLength)
            {
                delta = Input.mousePosition - lastPos;
                lastPos = Input.mousePosition;
                lastTime = Time.time;
            }
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
            transform.position = mouseWorldPosition;
            toRotation = Quaternion.LookRotation(Vector3.forward,delta);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            delta = Input.mousePosition - lastPos;
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
            if (delta != Vector3.zero)
            {
                toRotation = Quaternion.LookRotation(Vector3.forward,delta.normalized);
            }
        }
        Debug.Log(toRotation);
        transform.rotation = toRotation * _face;
    }
}

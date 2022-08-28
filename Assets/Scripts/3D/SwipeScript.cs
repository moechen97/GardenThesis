using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeScript : MonoBehaviour
{
    Vector2 startPos, endPos, direction;
    float touchTimeStart, touchTimeFinish, timeInterval;

    [SerializeField] private float throwForceInXAndZ = 1F;
    [SerializeField] private float throwForceInY = 50F;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchTimeStart = Time.time;
            startPos = Input.GetTouch(0).position;

            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touchTimeFinish = Time.time;
                timeInterval = touchTimeFinish - touchTimeStart;
                endPos = Input.GetTouch(0).position;
                direction = startPos - endPos;

                rb.isKinematic = false;
                rb.AddForce(-direction.x * throwForceInXAndZ, -direction.y / timeInterval, throwForceInXAndZ / timeInterval);

            }
        }
    }
}

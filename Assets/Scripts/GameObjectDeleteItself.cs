using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDeleteItself : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private GameObject Flower;
    [SerializeField] private LayerMask detectLayer;
    private Rigidbody2D _rigidbody;

    private bool isMoving = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rigidbody.velocity.magnitude > 0f)
        {
            isMoving = true;
        }

        if (isMoving && _rigidbody.velocity.magnitude < minSpeed)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector2.zero;
            if (Physics2D.OverlapCircle(transform.position, 0.5f, detectLayer))
            {
                Instantiate(Flower, transform.position, quaternion.identity);
            }
            isMoving = false;
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fungus_JellyHead : MonoBehaviour
{
    private Transform followT;
    private Transform rotationT;
    [SerializeField] private Animator jellyAnimator;
    [SerializeField] private Fungus_MaterialChange _materialChange;
    [SerializeField] private Transform Mesh;

    private bool canMove = false;

    // Update is called once per frame
    void Update()
    {
        if(!followT)
            Destroy(this.gameObject);
        if (canMove)
        {
            transform.position = followT.position;
            transform.rotation = rotationT.rotation;
        }
        
    }

    public void GetPosition(Transform followP, Transform rotationP)
    {
        followT = followP;
        rotationT = rotationP;
        Mesh.gameObject.SetActive(true);
        canMove = true;
    }

    public void JellyBloom()
    {
        jellyAnimator.SetBool("isBloomed",true);
    }

    public void jellyWithered()
    {
        jellyAnimator.SetBool("isWithered",true);
    }

    public void JellyDead()
    {
        _materialChange.Die();
    }
    
    
}

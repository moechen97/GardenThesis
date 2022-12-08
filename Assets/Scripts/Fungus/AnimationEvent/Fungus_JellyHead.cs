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
    private Plant_StateControl parentState;
    
    private bool canMove = false;
    private bool iskilled = false;

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

        if (parentState.returnKilledState() && !iskilled)
        {
            _materialChange.Killed();
            iskilled = true;
        }
        
    }

    public void GetPosition(Transform followP, Transform rotationP, Transform PParent)
    {
        followT = followP;
        rotationT = rotationP;
        Mesh.gameObject.SetActive(true);
        canMove = true;
        parentState = PParent.GetComponent<Plant_StateControl>();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFlower : MonoBehaviour
{
    private Transform followT;
    [SerializeField] private Animator flowerAnimator;
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
            transform.rotation = followT.rotation;
        }
        
    }

    public void GetPosition(Transform followP)
    {
        followT = followP;
        Mesh.gameObject.SetActive(true);
        canMove = true;
    }
    
    public void SpikeFlowerBloom()
    {
        flowerAnimator.SetBool("isBloom",true);
        
    }
    

    public void SpikeFlowerWithered()
    {
        flowerAnimator.SetBool("isWithered",true);
        _materialChange.Withered();
    }

    public void SpikeFlowerDie()
    {
        _materialChange.Die();
    }
}

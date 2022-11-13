using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Spike : MonoBehaviour
{
    [SerializeField] private GameObject SpikeFlower;
    [SerializeField] private Transform SpikeFlowerPosition;
    [SerializeField] private Fungus_MaterialChange _materialChange;

    private GameObject spikeFlower;
    
    private void Start()
    {
        StartCoroutine(InstantiateJellyHead());

    }

    private IEnumerator InstantiateJellyHead()
    {
        yield return new WaitForEndOfFrame();
        spikeFlower = Instantiate(SpikeFlower, SpikeFlowerPosition.position, Quaternion.identity,
            this.transform.parent);
        spikeFlower.GetComponent<SpikeFlower>().GetPosition(SpikeFlowerPosition);
        
    }
    
    public void Bloom()
    {
        spikeFlower.GetComponent<SpikeFlower>().SpikeFlowerBloom();
        
    }
    
    public void Withered()
    {
        spikeFlower.GetComponent<SpikeFlower>().SpikeFlowerWithered();
        _materialChange.MaterialWithered();
    }

    public void Dead()
    {
        _materialChange.Die();
        spikeFlower.GetComponent<SpikeFlower>().SpikeFlowerDie();
    }
}

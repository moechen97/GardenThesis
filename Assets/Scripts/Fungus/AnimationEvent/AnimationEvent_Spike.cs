using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent_Spike : MonoBehaviour
{
    [SerializeField] private GameObject SpikeFlower;
    [SerializeField] private Transform SpikeFlowerPosition;
    [SerializeField] private Fungus_MaterialChange _materialChange;
    [SerializeField] private AudioSource spike_audiosource;
    [SerializeField] private AudioClip grow;
    [SerializeField] private AudioClip bloom;
    [SerializeField] private AudioClip withered;
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

    public void Grow()
    {
        spike_audiosource.PlayOneShot(grow);
    }
    
    public void Bloom()
    {
        spikeFlower.GetComponent<SpikeFlower>().SpikeFlowerBloom();
        spike_audiosource.PlayOneShot(bloom);
    }
    
    public void Withered()
    {
        spikeFlower.GetComponent<SpikeFlower>().SpikeFlowerWithered();
        _materialChange.MaterialWithered();
        spike_audiosource.PlayOneShot(withered);
    }

    public void Dead()
    {
        _materialChange.Die();
        spikeFlower.GetComponent<SpikeFlower>().SpikeFlowerDie();
    }
}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SingleLand : MonoBehaviour
{
    private bool hasplants = false;

    private AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void plantedOn()
    {
        hasplants = true;
    }

    public void ActiveLand()
    {
        StartCoroutine(Shrink());
        if (!hasplants)
            return;
        _audio.Play();
    }

    IEnumerator Shrink()
    {
        transform.DOScale(transform.localScale * 0.7f, 0.3f);
        yield return new WaitForSeconds(0.4f);
        transform.DOScale(transform.localScale / 0.7f, 0.4f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoTweenCapacity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DOTween.SetTweensCapacity(2000,100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

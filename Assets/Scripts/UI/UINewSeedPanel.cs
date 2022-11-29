using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class UINewSeedPanel : MonoBehaviour
{
    private void Start()
    {
        transform.GetComponent<CanvasGroup>().alpha = 0;
        transform.GetComponent<CanvasGroup>().DOFade(1, 1f);
    }

    public void GetNewSeedInfo(GameObject newSeed)
    {
        GameObject seedicon = Instantiate(newSeed);
        seedicon.GetComponent<UISizeAdjustment>().enabled = false;
        seedicon.transform.parent = transform;
        seedicon.GetComponent<RectTransform>().localPosition = new Vector3(-74, 52, 0);
        seedicon.GetComponent<RectTransform>().sizeDelta = new Vector2(150f,150f);
        seedicon.transform.localScale = new Vector3(1F, 1F, 1F);
        
    }

    public void Close()
    {
        StartCoroutine(ClosePanel());
    }

    IEnumerator ClosePanel()
    {
        transform.GetComponent<CanvasGroup>().DOFade(0, 1f);
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }
}

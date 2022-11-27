using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedButton : MonoBehaviour
{
    [SerializeField] private Animator uiAnimator;
    [SerializeField] private Animator Seedbar;
    private bool isclosed = false;
    private bool canInteract = true;

    public void TapButton()
    {
        if (!canInteract)
            return;
        isclosed = !isclosed;
        canInteract = false;
        uiAnimator.SetBool("IsClosed",isclosed);
        Seedbar.SetBool("IsShown",isclosed);
        StartCoroutine(Colddown());
    }

    IEnumerator Colddown()
    {
        yield return new WaitForSeconds(0.2f);
        canInteract = true;
    }

}

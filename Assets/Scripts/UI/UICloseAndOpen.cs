using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICloseAndOpen : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private bool isOpened = false;
    private bool canInteract = true;
    
    public void Tap()
    {
        if (!canInteract)
            return;
        canInteract = false;
        isOpened = !isOpened;
        _animator.SetBool("Open",isOpened);
        StartCoroutine(Colddown());
    }
    
    IEnumerator Colddown()
    {
        yield return new WaitForSeconds(0.2f);
        canInteract = true;
    }
    
}

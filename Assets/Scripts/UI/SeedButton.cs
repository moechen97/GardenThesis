using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting
{
    public class SeedButton : MonoBehaviour
    {
        [SerializeField] private Animator uiAnimator;
        [SerializeField] private Animator Seedbar;
        [SerializeField] private GameObject FingerHint;
        private bool isclosed = false;
        private bool canInteract = true;
        private bool hasPlanted = false;

        public void TapButton()
        {
            if (!canInteract)
                return;
            isclosed = !isclosed;
            canInteract = false;
            uiAnimator.SetBool("IsClosed",isclosed);
            Seedbar.SetBool("IsShown",isclosed);
            if (isclosed && !hasPlanted)
            {
                StartCoroutine(SetFingerHintActive());
            }
            else if (!isclosed && !hasPlanted)
            {
                StopCoroutine(SetFingerHintActive());
                FingerHint.SetActive(false);
            }
            StartCoroutine(Colddown());
        }

        IEnumerator Colddown()
        {
            yield return new WaitForSeconds(0.5f);
            canInteract = true;
        }

        IEnumerator SetFingerHintActive()
        {
            yield return new WaitForSeconds(1f);
            FingerHint.SetActive(true);
        }
        
        private void Update()
        {
            if(hasPlanted)
                return;
            float  resourceUsed = Resources.GetResourcesUsed();
            Debug.Log("resourceUsed:" + resourceUsed);
            if (resourceUsed > 0)
            {
                hasPlanted = true;
                FingerHint.SetActive(false);
            }
        }
    }

}

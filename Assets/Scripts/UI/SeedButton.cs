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
        private Coroutine fingerhint;

        public void TapButton()
        {
            
            if (!canInteract)
            {
                if (fingerhint != null)
                {
                    StopCoroutine(fingerhint);
                    fingerhint = null;
                }
                FingerHint.SetActive(false);
                return;
            }
            
            isclosed = !isclosed;
            canInteract = false;
            uiAnimator.SetBool("IsClosed",isclosed);
            Seedbar.SetBool("IsShown",isclosed);
            if (isclosed && !hasPlanted)
            {
                NewTutorialSequence.instance.ToggleSeedButton();
                 fingerhint = StartCoroutine(SetFingerHintActive());
            }
            else if (!isclosed && !hasPlanted)
            {
                if (fingerhint != null)
                {
                    StopCoroutine(fingerhint);
                    fingerhint = null;
                }
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
            if (resourceUsed > 0)
            {
                hasPlanted = true;
                FingerHint.SetActive(false);
            }
        }
    }

}

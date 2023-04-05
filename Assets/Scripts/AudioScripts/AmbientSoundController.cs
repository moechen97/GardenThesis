using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Planting
{
    public class AmbientSoundController : MonoBehaviour
    {
        public AudioClip[] DayClips;

        public AudioClip[] NightClips;

        public AudioClip[] duskClips;

        private AudioSource _audioSource;

        //state = 0 = night , state = 1 = day , state = 2 = dusk
        private int timeState = 1;

        private float timecount = 0;
        private float clipLength;
        private AudioClip playingClip;

        private bool canChangeClip = false;

        private void OnEnable()
        {
            TimeEventManager.NightStart += Night;
            TimeEventManager.DayStart += Day;
            TimeEventManager.DuskStart += Dusk;
        }

        private void OnDisable()
        {
            TimeEventManager.NightStart -= Night;
            TimeEventManager.DayStart -= Day;
            TimeEventManager.DuskStart -= Dusk;
        }

        void Night()
        {
            timeState = 0;
        }

        void Day()
        {
            timeState = 1;
        }

        void Dusk()
        {
            timeState = 2;
        }

        private void Start()
        {
            _audioSource = transform.GetComponent<AudioSource>();
            StartCoroutine(PlayClip());
        }

        private void Update()
        {
            if (canChangeClip)
            {
                StartCoroutine(PlayClip());
            }
        }



        IEnumerator PlayClip()
        {
            canChangeClip = false;
            ChangeClip();
            _audioSource.PlayOneShot(playingClip);
            Debug.Log("Playing:" + playingClip.name);
            yield return new WaitForSeconds(clipLength * 8 / 9);
            canChangeClip = true;
            yield return null;
        }

        void ChangeClip()
        {
            switch (timeState)
            {
                case 0:
                    if (NightClips != null)
                    {
                        int x = Random.Range(0, NightClips.Length);
                        playingClip = NightClips[x];
                        clipLength = NightClips[x].length;
                    }

                    break;
                case 1:
                    if (DayClips != null)
                    {
                        int x = Random.Range(0, DayClips.Length);
                        playingClip = DayClips[x];
                        clipLength = DayClips[x].length;
                    }

                    break;
                case 2:
                    if (duskClips != null)
                    {
                        int x = Random.Range(0, duskClips.Length);
                        playingClip = duskClips[x];
                        clipLength = duskClips[x].length;
                    }

                    break;
            }
        }
    }
}

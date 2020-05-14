
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    [RequireComponent(typeof(Interactable))]
    public class NonThrowable : MonoBehaviour
    {
        public UnityEvent onHandHoverBegin;
        public UnityEvent onHandHoverEnd;
        public UnityEvent onAttachedToHand;
        public UnityEvent onDetachedFromHand;

        public AudioSource narration;
        bool pickedUp = false;
        public GameObject toEnableOrDisableHovering;
        public GameObject toPlayWhisper;
        public GameObject toStopWhisper;
        AudioSource whisperSource;
        AudioSource whisperStop;

        void Start()
        {
            narration = GetComponent<AudioSource>();
            whisperSource = toPlayWhisper.GetComponent<AudioSource>();
            whisperStop = toStopWhisper.GetComponent<AudioSource>();
        }

        void Update()
        {
            playWhisper();
        }

        void playWhisper()
        {
            if (pickedUp == true && !narration.isPlaying)
            {
                StartCoroutine(EnableNextCoroutine());
                pickedUp = false;
            }
        }

        IEnumerator EnableNextCoroutine()
        {
            yield return new WaitForSeconds(3);

            Destroy(toEnableOrDisableHovering.GetComponent<IgnoreHovering>());


            yield return new WaitForSeconds(4);

            whisperSource.Play();

        }

        void PlayNarration()
        {
            if (pickedUp == false)
            {
                whisperStop.Stop();
                narration.Play();
                pickedUp = true;

                // toEnableOrDisableHovering.AddComponent<AudioSource>();
            }
            else
            {
                Debug.Log("Audio already played");
            }
        }
        //-------------------------------------------------
        private void OnHandHoverBegin()
        {
            onHandHoverBegin.Invoke();
            Debug.Log("yaas");
            PlayNarration();

    }


        //-------------------------------------------------
        private void OnHandHoverEnd()
        {
            onHandHoverEnd.Invoke();
        }


    }
}
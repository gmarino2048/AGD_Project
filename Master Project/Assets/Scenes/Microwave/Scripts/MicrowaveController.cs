using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Microwave
{
    public class MicrowaveController : MonoBehaviour
    {

        [Header("Microwave Animation Controller")]
        public Sprite StartImage;
        public Sprite ClosedImage;
        public Sprite OpenImage;
        public SpriteRenderer Overlay;
        public string FinishedState = "Finished";
        public Animator MicrowaveAnimator;
        public float CriticalTime;

        [Header("Animation Parameters")]
        public string StartTrigger;
        public string Continue = "Continue";
        public string Opened = "Opened";

        [Header("Scene Controllers")]
        public TimerBehavior Timer;
        public Camera MainCamera;
        public BoxCollider2D MicrowaveButton;
        public SFXController SFX;

        bool CriticalPlayed;

        // Use this for initialization
        void Start()
        {
            Overlay.gameObject.SetActive(true);
            Overlay.sprite = StartImage;

            CriticalPlayed = false;
        }

        public void Play ()
        {
            SFX.PlayStart();
            Overlay.gameObject.SetActive(false);
            MicrowaveAnimator.SetTrigger(StartTrigger);
        }

        public void Finish ()
        {
            if (Timer.NotOpened)
            {
                Overlay.sprite = ClosedImage;
                SFX.PlayBeep();
            }
            else Overlay.sprite = OpenImage;

            Overlay.gameObject.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            if (Timer.GameActive && Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector3 worldPosition = MainCamera.ScreenToWorldPoint(mousePosition);
                worldPosition.z = MicrowaveButton.transform.position.z;

                if (MicrowaveButton.bounds.Contains(worldPosition))
                {
                    MicrowaveAnimator.SetTrigger(Opened);
                    SFX.PlayOpen();
                    Timer.NotOpened = false;
                    Timer.Stop();
                    CriticalPlayed = true;
                }
            }
            if (Timer.GameActive && Timer.TimeRemaining <= CriticalTime && !CriticalPlayed)
            {
                SFX.PlayExtra();
                MicrowaveAnimator.SetBool(Continue, true);
                CriticalPlayed = true;
            }
        }
    }
}
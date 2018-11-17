using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Microwave
{
    public class MicrowaveController : MonoBehaviour
    {

        [Header("Microwave Animation Controller")]
        public Sprite StartImage;
        public Animator MicrowaveAnimator;
        public float CriticalTime;

        SpriteRenderer Renderer;

        [Header("Animation Parameters")]
        public string StartTrigger;
        public string Continue = "Continue";
        public string Opened = "Opened";

        [Header("Scene Controllers")]
        public TimerBehavior Timer;
        public Camera MainCamera;
        public BoxCollider2D MicrowaveButton;

        bool CriticalPlayed;

        // Use this for initialization
        void Start()
        {
            Renderer = GetComponent<SpriteRenderer>();
            Renderer.sprite = StartImage;

            CriticalPlayed = false;
        }

        public void Play ()
        {
            MicrowaveAnimator.SetTrigger(StartTrigger);
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
                    Timer.Stop();
                    CriticalPlayed = true;
                }
            }
            if (Timer.GameActive && Timer.TimeRemaining <= CriticalTime && !CriticalPlayed)
            {
                MicrowaveAnimator.SetBool(Continue, true);
                CriticalPlayed = true;
            }
        }
    }
}
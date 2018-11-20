using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stirring
{
    public class BowlBehavior : MonoBehaviour
    {
        [Header("Game Controllers")]
        public TimerBehavior Timer;
        public Animator BowlAnimator;
        public SpoonBehavior Spoon;
        public SpriteRenderer Overlay;

        [Header("Bowl Settings")]
        public float Scaler = 2.25f;
        float DirectionMultiplier;

        [Header("Animator Parameters")]
        public string IngredientSelector;
        public string TransitionTrigger = "Transition";

        public float TransitionTime = 15f;
        bool TransitionDone;

        public float Acceleration = 0.25f;
        float Speed;
        public float MaxSpeed;

        // Use this for initialization
        void Start()
        {
            Speed = 0f;
            BowlAnimator.SetTrigger(IngredientSelector);

            TransitionDone = false;
            Overlay.gameObject.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            if (Timer.GameActive && Overlay.gameObject.activeSelf) Overlay.gameObject.SetActive(false);

            UpdateAnimator();

            if (Timer.TimeRemaining < TransitionTime && ! TransitionDone)
            {
                BowlAnimator.SetTrigger(TransitionTrigger);
                TransitionDone = true;
            }
        }

        void UpdateAnimator ()
        {
            // Set animator speed
            if (Speed >= 0) Speed += Spoon.Dragging ?
                                          (Spoon.Direction ? Acceleration * Time.deltaTime : -2 * Acceleration * Time.deltaTime) :
                                          (-1 * Acceleration * Time.deltaTime);
            else Speed += Spoon.Dragging ?
                               (Spoon.Direction ? 2 * Acceleration * Time.deltaTime : -1 * Acceleration * Time.deltaTime) :
                               (Acceleration * Time.deltaTime);

            Speed = Mathf.Clamp(Speed, -1 * MaxSpeed, MaxSpeed);

            DirectionMultiplier = Speed >= 0.01 ? 1 : DirectionMultiplier;
            DirectionMultiplier = Speed <= -0.01 ? -1 : DirectionMultiplier;

            float animSpeed = Mathf.Abs(Speed);

            BowlAnimator.speed = animSpeed;

            float xScale = Scaler * DirectionMultiplier;

            gameObject.transform.localScale = new Vector3(xScale, gameObject.transform.localScale.y, 1);
        }
    }
}
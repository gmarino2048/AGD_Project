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

        [Header("Bowl Settings")]
        public float Scaler = 2.25f;
        float DirectionMultiplier;

        [Header("Animator Parameters")]
        public string IngredientSelector;
        public string TransitionTrigger;

        public float Acceleration = 0.25f;
        float Speed;
        public float MaxSpeed;

        // Use this for initialization
        void Start()
        {
            Speed = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            // Set animator speed
            if (Speed >= 0) Speed += Spoon.Dragging ?
                                          (Spoon.Direction ? Acceleration * Time.deltaTime : -2 * Acceleration * Time.deltaTime) :
                                          (-1 * Acceleration * Time.deltaTime);
            else Speed += Spoon.Dragging ?
                               (Spoon.Direction ? 2 * Acceleration * Time.deltaTime : -1 * Acceleration * Time.deltaTime) :
                               (Acceleration * Time.deltaTime);

            Speed = Mathf.Clamp(Speed, -1 * MaxSpeed, MaxSpeed);

            DirectionMultiplier = Speed >= 0 ? 1 : -1;

            float animSpeed = Mathf.Abs(Speed);

            BowlAnimator.speed = animSpeed;

            float xScale = Scaler * DirectionMultiplier;
            float yScale = xScale;

            gameObject.transform.localScale = new Vector3(xScale, yScale, 1);


        }
    }
}
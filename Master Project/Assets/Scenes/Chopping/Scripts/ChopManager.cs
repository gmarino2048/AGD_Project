﻿using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Chopping
{
    public class ChopManager : MonoBehaviour
    {

        #region Parameters

        [Header("Game Controllers")]
        public TimerBehavior Timer;

        [Header("Chop configurations")]
        public float ChopWidth = 0.25f; // The width of the chop stroke for calculating whether there's a knife collision

        [SerializeField]
        public KnifeBehavior Reference; // The KnifeBehavior object to reference for position, etc... So that this script can run anywhere
        [SerializeField]
        public GameObject ReferencePosition; // The position to insert the chop at
        [SerializeField]
        public KeyCode InputKey = KeyCode.Space; // The input key for the knife to chop on

        [Header("Chop Renderer")]
        [SerializeField]
        public GameObject SpriteParent;
        public SpriteControllerBehavior SpriteController;
        public float ZIndex = 5;

        public int TotalChops { get; private set; } // The total number of attempted chops on the object
        public List<Chop> AlreadyChopped { get; private set; } // A list of valid chops for calculating collisions

        /// <summary>
        /// A struct to contain the actual position of the chop, as well as the range of values that the chop encompasses so that
        /// collisions between knife chops can be calculated.
        /// </summary>
        public struct Chop
        {
            public float LowerBound;
            public float ActualPosition;
            public float UpperBound;
        }



        /// <summary>
        /// Contains the information about whether each chop is a Hit (valid position on the choppable object), a Miss (outside
        /// the choppable object sprite), or a collision (at the same place as another knife chop).
        /// </summary>
        public enum HitOrMiss
        {
            Hit,
            Miss,
            Collision
        }

        #endregion

        #region MonoBehavior

        /// <summary>
        /// Runs once at the start of the scene.
        /// </summary>
        void Start()
        {
            AlreadyChopped = new List<Chop>();
            TotalChops = 0;
        }


        /// <summary>
        /// Runs every time the frame updates.
        /// </summary>
        void Update()
        {
            // Listen for space bar input
            if (Timer.GameActive && Input.GetKeyDown(InputKey))
            {
                Debug.Log(ReferencePosition.transform.position.x);
                InsertChop(ReferencePosition.transform.position.x);
            }
        }

        #endregion

        #region Auxiliary

        /// <summary>
        /// Inserts a single Chop into a list if it was made in a valid position. Otherwise, it's ignored.
        /// </summary>
        /// <param name="position">The X position of the knife at the time when the chop was made.</param>
        void InsertChop(float position)
        {
            Chop currentChop = new Chop
            {
                LowerBound = position - ChopWidth,
                ActualPosition = position,
                UpperBound = position + ChopWidth
            };
            
            HitOrMiss status = ValidPosition(currentChop);

            PrintHitOrMiss(status);

            // Only add if it was a valid knife chop.
            if (status == HitOrMiss.Hit)
            {
                StartCoroutine(Reference.DoAnimation(1));

                AlreadyChopped.Add(currentChop);

                SpriteController.DrawChop();

                TotalChops++;
            }
            if (status == HitOrMiss.Collision) TotalChops++;
        }


        /// <summary>
        /// Checks to make sure that the position is valid on the choppable object. A position is defined as
        /// valid if it 1. is within the bounds of the choppable objece and 2. Is not within the bounds of
        /// any other valid chop.
        /// </summary>
        /// <param name="current">The Chop structure to check.</param>
        /// <returns>The HitOrMiss status of the current Chop.</returns>
        HitOrMiss ValidPosition(Chop current)
        {
            // TODO: Check if bounds overlap??

            // Get all overlaps with Linq query
            List<Chop> cutWithin = AlreadyChopped.Where(chopValue =>
                                                        current.UpperBound >= chopValue.LowerBound &&
                                                        current.LowerBound <= chopValue.UpperBound)
                                                 .ToList();

            if (cutWithin.Any())
            {
                return HitOrMiss.Collision;
            }

            float lowerBound = Reference.ItemToChop.LeftBound;
            float upperBound = Reference.ItemToChop.RightBound;

            return current.ActualPosition < lowerBound || current.ActualPosition > upperBound
                          ? HitOrMiss.Miss : HitOrMiss.Hit;
        }

        #endregion

        #region Debug

        /// <summary>
        /// Draws the chop bounds in the scene for visual debug.
        /// </summary>
        /// <param name="current">The chop to be drawn.</param>
        void DebugDrawChop(Chop current)
        {
            float upperY = 10;
            float lowerY = -10;

            // Draw Center Line
            Vector3 centerStart = new Vector3(current.ActualPosition, upperY, 0);
            Vector3 centerEnd = new Vector3(current.ActualPosition, lowerY, 0);

            Color centerColor = Color.red;

            Debug.DrawLine(centerStart, centerEnd, centerColor, Mathf.Infinity, false);

            // Draw Left line
            Vector3 leftStart = new Vector3(current.ActualPosition - ChopWidth, upperY, 0);
            Vector3 leftEnd = new Vector3(current.ActualPosition - ChopWidth, lowerY, 0);

            Color leftColor = Color.green;

            Debug.DrawLine(leftStart, leftEnd, leftColor, Mathf.Infinity, false);

            // Draw Right Line
            Vector3 rightStart = new Vector3(current.ActualPosition + ChopWidth, upperY, 0);
            Vector3 rightEnd = new Vector3(current.ActualPosition + ChopWidth, lowerY, 0);

            Color rightColor = Color.green;

            Debug.DrawLine(rightStart, rightEnd, rightColor, Mathf.Infinity, false);
        }


        /// <summary>
        /// Prints the HitOrMiss value in plain English.
        /// </summary>
        /// <param name="status">The status of some Chop structure.</param>
        void PrintHitOrMiss(HitOrMiss status)
        {
            string message;

            switch (status)
            {
                case HitOrMiss.Hit:
                    message = "Valid Position";
                    break;

                case HitOrMiss.Collision:
                    message = "Knife Cut Collision";
                    break;

                default:
                    message = "Target Missed";
                    break;
            }

            Debug.Log(message);
        }

        #endregion
    }
}

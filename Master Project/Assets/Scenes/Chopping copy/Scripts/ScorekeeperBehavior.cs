﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chopping
{
    public class ScorekeeperBehavior : MonoBehaviour
    {

        #region Parameters

        [Header("Scorekeeping Info")]
        [SerializeField]
        public ChopManager ChopManager; // The chop manager with a record of all the valid chops.

        public float TimeInSeconds = 30f; // The amount of time to start the countdown with. Default is 30 seconds.

        public Text TimerText; // The text object used to display time remaining.
        public Button FinishButton; // The button used to finish and exit this minigame early.

        float CurrentTime; // The current time on the countdown
        public bool TimerActive { get; private set; } // Is the timer currently active and counting down?
        public bool TimerDone { get; private set; } // Has the timer finished counting down?

        public float ScoreScaler = 2;
        public float Score { get; private set; } // The score for this minigame


        [Header("Final Score Display")]
        [SerializeField]
        public CanvasGroup FinalScoreDisplay; // The canvas group used to display the final score of the game.
        [SerializeField]
        public Text FinalScore; // The text object to set the final score to.

        #endregion

        #region MonoBehaviour

        /// <summary>
        /// Run once at the start of the scene. Gets the UI elements from the scene and attaches a
        /// simple listener to the button. Activates the timer based on the value of TimeInSeconds.
        /// </summary>
        void Start()
        {
            CurrentTime = TimeInSeconds;
            TimerActive = true;
            TimerDone = false;

            FinishButton.onClick.AddListener(OnFinishButtonPressed);

            FinalScoreDisplay.alpha = 0;
            FinalScoreDisplay.gameObject.SetActive(false);
        }


        /// <summary>
        /// Run once per frame update. This function manages the time remaining on the counter
        /// and calculates the score once the finish button is pressed or the time remaining on the
        /// timer runs out.
        /// </summary>
        void Update()
        {
            if (CurrentTime > 0 && TimerActive)
            {
                CurrentTime -= Time.deltaTime;
            }
            else if (TimerActive)
            {
                TimerDone = true;
                TimerActive = false;
            }
            if (TimerDone)
            {
                EndGame();
                TimerDone = false;
            }

            TimerText.text = ((int)CurrentTime).ToString();
        }

        #endregion

        #region Auxiliary

        /// <summary>
        /// Calculates the player's score. Is currently just an average of the distances between chops.
        /// </summary>
        /// <returns>Should return a float between 0 and 1 representing the minigame's score. Doesn't
        /// do that yet though.</returns>
        void CalculateScore()
        {
            float validChops = ChopManager.AlreadyChopped.Count;
            float totalChops = ChopManager.TotalChops;

            float precisionScaler = validChops / totalChops;
            //precisionScaler = precisionScaler / ScoreScaler;

            float scaledValid =  validChops * precisionScaler;

            Score = scaledValid < 1 ? 1 : 1 / scaledValid;
        }


        /// <summary>
        /// Creates an ascending list of Chops used to calculate the average distance between each valid
        /// chop. The sorting guarantees that two chops next to each other in the list will be next to each
        /// other in the scene.
        /// </summary>
        /// <param name="manager">The ChopManager used to get the list of chops.</param>
        /// <returns>A sorted float list of chop positions.</returns>
        List<float> SortChops(ChopManager manager)
        {
            List<float> chops = new List<float>();

            manager.AlreadyChopped.ForEach(chop => chops.Add(chop.ActualPosition));

            chops.Sort();

            return chops;
        }


        /// <summary>
        /// Calculates the average distance between each chop object.
        /// </summary>
        /// <param name="values">The list of chop positions.</param>
        /// <returns>The float representing the average distance between each chop.</returns>
        float AverageDistance(List<float> values)
        {
            if (values.Count > 1)
            {
                int current = 1;
                int total = values.Count;

                float average = values[1] - values[0];

                if (values.Count > 2)
                {
                    current++;

                    while (current < total)
                    {
                        float val = values[current] - values[current - 1];
                        average = ((average * (current - 1)) + val) / current;
                        current++;
                    }
                }

                return average;
            }
            return 0;
        }

        #endregion

        #region UI

        /// <summary>
        /// The function which defines what should happen once the finish button is pressed.
        /// </summary>
        void OnFinishButtonPressed()
        {
            TimerDone = true;
            TimerActive = false;
        }

        /// <summary>
        /// Ends this minigame by setting each of the different components to their finished
        /// state. 
        /// </summary>
        void EndGame () 
        {
            FinalScoreDisplay.gameObject.SetActive(true);

            CurrentTime = 0f;
            TimerActive = false;
            TimerDone = true;

            CalculateScore();

            float scaledScore = (1 - Score) * 1000;

            FinalScore.text = Mathf.RoundToInt(scaledScore).ToString() + "/1000";
            StartCoroutine(FadeCanvas(FinalScoreDisplay, 0, 5, 1));
        }

        /// <summary>
        /// Fades in the canvas group containing the final score display once
        /// the minigame has finished. 
        /// </summary>
        /// <returns>An IEnumerator used to enable the coroutine.</returns>
        /// <param name="canvas">The Canvas Group to fade in.</param>
        /// <param name="startAlpha">The starting opacity of the canvas.</param>
        /// <param name="duration">The duration of the fade effect.</param>
        /// <param name="endAlpha">The final opacity of the Canvas Group.</param>
        IEnumerator FadeCanvas(CanvasGroup canvas, float startAlpha, float duration, float endAlpha)
        {
            float startTime = Time.time;

            float change = (endAlpha - startAlpha) / duration;

            while (Time.time - startTime <= duration)
            {
                float currentTime = Time.time - startTime;
                canvas.alpha = startAlpha + (change * currentTime);

                yield return new WaitForEndOfFrame();
            }
        }

        float ScoreGame () {
            return 0f;
        }

        #endregion
    }
}
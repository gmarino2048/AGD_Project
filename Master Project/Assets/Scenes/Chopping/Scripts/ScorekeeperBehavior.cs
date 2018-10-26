using System.Collections.Generic;
using System.Linq;
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

        Text TimerText; // The text object used to display time remaining.
        Button FinishButton; // The button used to finish and exit this minigame early.

        float CurrentTime; // The current time on the countdown
        bool TimerActive; // Is the timer currently active and counting down?
        bool TimerDone; // Has the timer finished counting down?


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
            TimerText = GetComponentInChildren<Text>();
            FinishButton = GetComponentInChildren<Button>();

            CurrentTime = TimeInSeconds;
            TimerActive = true;
            TimerDone = false;

            FinishButton.onClick.AddListener(OnFinishButtonPressed);

            FinalScoreDisplay.alpha = 0;
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
                Score();
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
        float Score()
        {
            float average = AverageDistance(SortChops(ChopManager));
            Debug.Log(average);
            return average;
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
            CurrentTime = 0f;
            TimerActive = false;
            TimerDone = true;
        }

        #endregion
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Shaking
{
    public class ScorekeeperBehavior : MonoBehaviour
    {

        [Header("Scene Objects")]
        [SerializeField]
        public ShakerBehavior Shaker; // The shaker object tracking the number of shakes.
        [SerializeField]
        public TimerBehavior Timer; // The timer counting the time remaining in the minigame.

        [Header("Score Display Options")]
        [SerializeField]
        public Text ShakerLabel; // The label showing the description of the minigame score.
        public string ShakerInitial; // The initial value of the score label.
        public string ShakerFinal; // The final value of the score label when the 
        [SerializeField]
        public Text ShakerScore; // The number of shakes that the user has made.

        [Header("Score Splash Screen Options")]
        [SerializeField]
        public CanvasGroup ScoreDisplay;// The place to display the final score.
        [SerializeField]
        public Text FinalScoreText; // The text used to display the user's final score.

        public float FinalScore { get; private set; } // The final score of the minigame.
        private bool Finished; // Tells whether this minigame scoring has finished.

        /// <summary>
        /// Sets the Shaker label to its initial value and sets the shaker score
        /// to 0 in case the Shaker has not been inititalized yet.
        /// </summary>
        void Start()
        {
            ShakerLabel.text = ShakerInitial;
            ShakerScore.text = 0.ToString();

            ScoreDisplay.alpha = 0f;
        }

        /// <summary>
        /// Updates the shaker score until the timer runs out of time, then 
        /// displays the final score and the end of game text on the label.
        /// </summary>
        void Update()
        {
            if (!Timer.Finished)
            {
                ShakerScore.text = Shaker.Shakes.ToString();
            }
            else if (!Finished)
            {
                ShakerLabel.text = ShakerFinal;
                float score = GetScore();
                Debug.Log(score);

                // TODO: Report the score here
                FinalScoreText.text = GetScoreText(score);

                Finished = true;
            }
        }

        /// <summary>
        /// Calculates the score of the game. The final score is 1/(1+x), where
        /// x is the number of shakes that the user has made. This way the score
        /// converges to 0 as x approaches infinity, denoting a perfect game.
        /// </summary>
        /// <returns>The score of this minigame.</returns>
        float GetScore () {
            return 1f / (1f + Shaker.Shakes);
        }

        string GetScoreText (float score) {
            int scaledScore = Mathf.RoundToInt((1 - score) * 1000);
            return scaledScore + "/1000";
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Shaking
{
    public class ScorekeeperBehavior : MonoBehaviour, IDishScoreKeeper
    {
        /// <summary>
        /// The dish preparation manager
        /// </summary>
        private DishPreparationManager _DishPreparationManager;

        /// <summary>
        /// The dish score manager
        /// </summary>
        private DishScoreManager _DishScoreManager;

        private GameNarrativeManager _GameNarrativeManager;

        [Header("Scoring Settings")]
        public uint TargetShakes; // The target number of shakes for the scene.

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
            _DishPreparationManager = GameObject.FindObjectOfType<DishPreparationManager>();
            _DishScoreManager = GameObject.FindObjectOfType<DishScoreManager>();
            _GameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();

            ShakerLabel.text = ShakerInitial;
            ShakerScore.text = 0.ToString();

            ScoreDisplay.alpha = 0f;
            FinalScoreText.text = "";
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

                FinalScoreText.text = GetScoreText(score);
                StartCoroutine(FadeCanvas(ScoreDisplay, 0, 2f, 1f));

                Finished = true;
                StartCoroutine(EndMiniGame());
            }
        }

        /// <summary>
        /// Calculates the score of the game. The final score is 1/(1+x), where
        /// x is the number of shakes that the user has made. This way the score
        /// converges to 0 as x approaches infinity, denoting a perfect game.
        /// </summary>
        /// <returns>The score of this minigame.</returns>
        public float GetScore() {
            return 1f / (1f + ((float)Shaker.Shakes / (float)TargetShakes));
        }

        /// <summary>
        /// Gets the score text.
        /// </summary>
        /// <returns>The score text.</returns>
        /// <param name="score">Score.</param>
        string GetScoreText (float score) {
            int scaledScore = Mathf.RoundToInt((1 - score) * 1000);
            return scaledScore + "/1000";
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

            while (Time.time - startTime <= duration){
                float currentTime = Time.time - startTime;
                canvas.alpha = startAlpha + (change * currentTime);

                yield return new WaitForEndOfFrame();
            }
        }

        /// <summary>
        /// Sends the score and goes to the next scene
        /// </summary>
        private IEnumerator EndMiniGame()
        {
            yield return new WaitForSeconds(10);

            if (_DishPreparationManager != null)
            {
                if (_DishScoreManager != null)
                {
                    _DishScoreManager.AddIngredientToDish(_GameNarrativeManager.CurrentStage.MonsterID, _DishPreparationManager.currentIngredient, GetScore());
                }

                _DishPreparationManager.GoToNextScene();
            }
        }
    }
}

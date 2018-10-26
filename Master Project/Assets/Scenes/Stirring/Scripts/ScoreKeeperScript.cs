using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Stirring
{
    public class ScoreKeeperScript : MonoBehaviour, IDishScoreKeeper
    {
        private readonly Guid _NESSIE_GUID = new Guid("{060F70EA-8A92-4117-AB65-75DE3458E407}");

        /// <summary>
        /// The dish preparation manager
        /// </summary>
        private DishPreparationManager _DishPreparationManager;

        /// <summary>
        /// The dish score manager
        /// </summary>
        private DishScoreManager _DishScoreManager;

        public Text FinalScoreText; // The text used to display the user's final score.

        public CanvasGroup ScoreDisplay;// The place to display the final score.

        // Used for initialization
        void Start()
        {
            _DishPreparationManager = GameObject.FindObjectOfType<DishPreparationManager>();
            _DishScoreManager = GameObject.FindObjectOfType<DishScoreManager>();

            ScoreDisplay.alpha = 0f;
            FinalScoreText.text = "";
        }

        /// <summary>
        /// Sends the score to dish score manager
        /// </summary>
        public void SendScore()
        {
            StartCoroutine(EndMiniGame());
        }
        
        /// <summary>
        /// takes distance and converts to 1-0 scale with 0 being the best
        /// </summary>
        public float GetScore()
        {
            float distance = GameObject.Find("spoon").GetComponent<SpoonScript>().travelDistance;
            return 1 / (distance + 1);
        }

        /// <summary>
        /// Gets the score text.
        /// </summary>
        /// <returns>The score text.</returns>
        /// <param name="score">Score.</param>
        string GetScoreText()
        {
            int scaledScore = Mathf.RoundToInt((1 - GetScore()) * 1000);
            return scaledScore + "/1000";
        }

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

        /// <summary>
        /// Sends the score and goes to the next scene
        /// </summary>
        private IEnumerator EndMiniGame()
        {
            //Debug.Log(GetScoreText());
            FinalScoreText.text = GetScoreText();
            StartCoroutine(FadeCanvas(ScoreDisplay, 0, 2f, 1f));
            yield return new WaitForSeconds(10);

            if (_DishPreparationManager != null)
            {
                if (_DishScoreManager != null)
                {
                    _DishScoreManager.AddIngredientToDish(_NESSIE_GUID, _DishPreparationManager.currentIngredient, GetScore());
                }

                _DishPreparationManager.GoToNextScene();
            }
        }
    }
}


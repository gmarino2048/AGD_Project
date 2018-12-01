using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ingredients
{
    public class UIManager : MonoBehaviour {

        [Header("Tutorial")]
        public CanvasGroup Tutorial;
        public Button TutorialButton;

        [Header("FinalScore")]
        public CanvasGroup FinalScore;
        public Button FinalScoreButton;

        [Header("Game Objects")]
        public IngredientManager Manager;

        // Use this for initialization
        void Start()
        {
            Tutorial.alpha = 1;
            Tutorial.gameObject.SetActive(true);

            FinalScore.alpha = 0;
            FinalScore.gameObject.SetActive(false);

            TutorialButton.onClick.AddListener(() => StartCoroutine(StartGame()));

            FinalScoreButton.onClick.AddListener(() =>
            {
                try
                {
                    DishPreparationManager dishPreparation = FindObjectOfType<DishPreparationManager>();
                    dishPreparation.GoToNextScene();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                    Debug.Log("Shaking Not Running in Game");
                }
            });
        }

        IEnumerator StartGame()
        {
            yield return FadeCanvas(Tutorial, 1, 1, 0);
            Tutorial.gameObject.SetActive(false);

            Manager.Activate();
        }

        public IEnumerator EndGame ()
        {
            yield return new WaitForSeconds(1);
            FinalScore.gameObject.SetActive(true);
            yield return FadeCanvas(FinalScore, 0, 1, 1);
            FinalScore.alpha = 1;
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
    }
}
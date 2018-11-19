using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shaking
{
    public class UIController : MonoBehaviour
    {

        [Header("Game Controller")]
        public TimerBehavior GameController;

        [Header("Canvases")]
        public CanvasGroup Tutorial;
        public CanvasGroup MainInfo;
        public CanvasGroup FinalScore;

        [Header("Buttons")]
        public Button GameStart;
        public Button NextButton;

        // Use this for initialization
        void Start()
        {
            Tutorial.alpha = 1;

            Tutorial.gameObject.SetActive(true);
            MainInfo.gameObject.SetActive(false);
            FinalScore.gameObject.SetActive(false);

            NextButton.onClick.AddListener(() =>
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

        void Update()
        {
            if (!GameController.GameActive && ! GameController.Finished)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartGame();
                }
            }
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

        public void StartGame () {
            StartCoroutine(FadeTutorial());
        }

        IEnumerator FadeTutorial () {
            yield return FadeCanvas(Tutorial, 1, 1, 0);
            Tutorial.gameObject.SetActive(false);
            MainInfo.gameObject.SetActive(true);
            GameController.StartGame();
        }

        public void EndGame () {
            FinalScore.alpha = 0;
            FinalScore.gameObject.SetActive(true);
            StartCoroutine(FadeCanvas(FinalScore, 0, 1, 1));
        }
    }
}

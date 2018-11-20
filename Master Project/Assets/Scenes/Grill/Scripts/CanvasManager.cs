using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grill
{
    public class CanvasManager : MonoBehaviour
    {

        [Header("Canvases")]
        public CanvasGroup Tutorial;
        public CanvasGroup Main;
        public CanvasGroup ScoreDisplay;

        [Header("Buttons")]
        public Button StartGame;
        public Button NextScene;

        [Header("Score Text")]
        public Text ScoreValue;

        [Header("Game Controller")]
        public TimerBehavior Timer;

        // Use this for initialization
        void Start()
        {
            Tutorial.alpha = 1;
            Tutorial.gameObject.SetActive(true);

            Main.alpha = 0;
            Main.gameObject.SetActive(false);

            ScoreDisplay.alpha = 0;
            ScoreDisplay.gameObject.SetActive(false);

            StartGame.onClick.AddListener(() => StartCoroutine(StartMinigame()));
            NextScene.onClick.AddListener(() =>
            {
                try
                {
                    DishPreparationManager sceneChanger = FindObjectOfType<DishPreparationManager>();
                    sceneChanger.GoToNextScene();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                    Debug.Log("Grill Scene not Playing in Game");
                }
            });
        }

        IEnumerator StartMinigame () {
            yield return FadeCanvas(Tutorial, 1, 1, 0);
            Tutorial.gameObject.SetActive(false);

            yield return new WaitForSeconds(1);

            Main.gameObject.SetActive(true);
            yield return FadeCanvas(Main, 0, 1, 1);
            Main.alpha = 1;

            Timer.Activate();
        }

        public IEnumerator ShowScore (float scoreValue)
        {
            yield return FadeCanvas(Main, 1, 0, 0);
            Main.gameObject.SetActive(false);

            yield return new WaitForSeconds(1);

            int score = Mathf.RoundToInt(1000f * (1f - scoreValue));
            ScoreValue.text = score.ToString() + "/1000";

            ScoreDisplay.gameObject.SetActive(true);
            yield return FadeCanvas(ScoreDisplay, 0, 1, 1);
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
        public  IEnumerator FadeCanvas(CanvasGroup canvas, float startAlpha, float duration, float endAlpha)
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

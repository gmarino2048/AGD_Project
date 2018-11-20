using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stirring
{
    public class UIManager : MonoBehaviour
    {

        [Header("Canvas Objects")]
        public CanvasGroup Tutorial;
        public CanvasGroup MainDisplay;
        public CanvasGroup ScoreDisplay;

        [Header("Buttons")]
        public Button StartButton;
        public Button NextScene;

        [Header("Game Controls")]
        public TimerBehavior Timer;

        // Use this for initialization
        void Start()
        {
            Tutorial.alpha = 1;
            Tutorial.gameObject.SetActive(true);

            MainDisplay.alpha = 0;
            ScoreDisplay.alpha = 0;

            MainDisplay.gameObject.SetActive(false);
            ScoreDisplay.gameObject.SetActive(false);

            StartButton.onClick.AddListener(() => StartCoroutine(StartGame()));
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
                    Debug.Log("Stirring Scene not Playing in Game");
                }
            });
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator StartGame()
        {
            yield return FadeCanvas(Tutorial, 1, 1, 0);
            Tutorial.gameObject.SetActive(false);

            yield return new WaitForSeconds(1);

            MainDisplay.alpha = 0;
            MainDisplay.gameObject.SetActive(true);
            yield return FadeCanvas(MainDisplay, 0, 1, 1);
            MainDisplay.alpha = 1;

            Timer.Activate();
        }

        public IEnumerator EndGame () 
        {
            yield return FadeCanvas(MainDisplay, 1, 1, 0);
            MainDisplay.gameObject.SetActive(false);

            yield return new WaitForSeconds(1);

            ScoreDisplay.alpha = 0;
            ScoreDisplay.gameObject.SetActive(true);
            yield return FadeCanvas(ScoreDisplay, 0, 1, 1);
            ScoreDisplay.alpha = 1;
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
        public IEnumerator FadeCanvas(CanvasGroup canvas, float startAlpha, float duration, float endAlpha)
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
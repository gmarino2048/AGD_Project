using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Microwave
{
    public class UIManager : MonoBehaviour
    {

        [Header("Canvases")]
        public CanvasGroup Tutorial;
        public CanvasGroup ScoreDisplay;

        [Header("Buttons")]
        public Button StartGame;
        public Button NextScene;

        [Header("Game Controllers")]
        public TimerBehavior GameTimer;
        public ScorekeeperBehavior Scorekeeper;

        // Use this for initialization
        void Start()
        {
            StartGame.onClick.AddListener(() => StartCoroutine(StartMinigame()));

            Tutorial.alpha = 1;
            Tutorial.gameObject.SetActive(true);
            ScoreDisplay.gameObject.SetActive(false);
        }

        IEnumerator StartMinigame () 
        {
            yield return FadeCanvas(Tutorial, 1, 1, 0);
            Tutorial.gameObject.SetActive(false);
            yield return new WaitForSeconds(1);

            GameTimer.Activate();
        }

        public IEnumerator EndGame ()
        {
            Scorekeeper.SetScore();
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
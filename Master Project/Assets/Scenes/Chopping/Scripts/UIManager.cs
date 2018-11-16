using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Chopping
{
    public class UIManager : MonoBehaviour
    {

        [Header("Canvas Groups")]
        public CanvasGroup Tutorial;
        public CanvasGroup MainDisplay;
        public CanvasGroup FinalScore;

        [Header("Game Controllers")]
        public TimerBehavior Timer;

        [Header("Start Button")]
        public Button NextButton;

        // Use this for initialization
        void Start()
        {
            Tutorial.alpha = 1f;
            Tutorial.gameObject.SetActive(true);

            MainDisplay.gameObject.SetActive(false);
            FinalScore.gameObject.SetActive(false);

            NextButton.onClick.AddListener(() => StartCoroutine(StartGame()));
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator StartGame()
        {
            yield return FadeCanvas(Tutorial, 1, 1, 0);
            Tutorial.gameObject.SetActive(false);

            MainDisplay.alpha = 0f;
            MainDisplay.gameObject.SetActive(true);
            yield return FadeCanvas(MainDisplay, 0, 0.5f, 1);

            Timer.Activate();
        }

        public IEnumerator EndGame() 
        {
            FinalScore.alpha = 0;
            FinalScore.gameObject.SetActive(true);

            yield return FadeCanvas(FinalScore, 0, 1, 1);
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

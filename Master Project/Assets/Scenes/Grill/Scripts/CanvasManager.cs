using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grill
{
    public class CanvasManager : MonoBehaviour
    {

        [Header("Canvases")]
        public CanvasGroup Tutorial;
        public CanvasGroup Main;
        public CanvasGroup ScoreDisplay;

        [Header("Game Controller")]
        public TimerBehavior Timer;

        // Use this for initialization
        void Start()
        {
            Tutorial.alpha = 1;
            Tutorial.gameObject.SetActive(true);

            ScoreDisplay.alpha = 0;
            ScoreDisplay.gameObject.SetActive(false);

            Main.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!Timer.GameActive && !Timer.GameOver)
                    StartCoroutine(StartMinigame());

            }
        }

        IEnumerator StartMinigame () {
            yield return FadeCanvas(Tutorial, 1, 1, 0);
            Main.gameObject.SetActive(true);
            Tutorial.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            Timer.Activate();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class OverlayController : MonoBehaviour {

        [Header("Flat Color")]
        public CanvasGroup ColorCanvas;
        public Image FlatColor;

        public IEnumerator ShowColor(Color color, float duration)
        {
            FlatColor.color = color;
            ColorCanvas.alpha = 0;

            ColorCanvas.gameObject.SetActive(true);
            yield return FadeCanvas(ColorCanvas, 0, duration, 1);
            ColorCanvas.alpha = 1;
        }

        public IEnumerator HideColor(float duration)
        {
            yield return FadeCanvas(ColorCanvas, ColorCanvas.alpha, duration, 0);
            ColorCanvas.alpha = 0;
            ColorCanvas.gameObject.SetActive(false);
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

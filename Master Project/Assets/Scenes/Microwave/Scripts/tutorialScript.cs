using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Microwave
{
    public class tutorialScript : MonoBehaviour
    {
        public CanvasGroup TutorialGroup;

        private void OnMouseDown()
        {
            StartCoroutine(FadeTutorial(TutorialGroup, 0, 2f, 1f));
            GameObject.Find("TimerText").GetComponent<MicrowaveTimerScript>().StartGame();
        }

        public IEnumerator FadeTutorial(CanvasGroup tutorial, float startAlpha, float duration, float endAlpha)
        {
            float startTime = Time.time;

            float change = (endAlpha - startAlpha) / duration;

            while (Time.time - startTime <= duration)
            {
                float currentTime = Time.time - startTime;
                tutorial.alpha = startAlpha + (change * currentTime);

                yield return new WaitForEndOfFrame();
            }
        }
    }


}

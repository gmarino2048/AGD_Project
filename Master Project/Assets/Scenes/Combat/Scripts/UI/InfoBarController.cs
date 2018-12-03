using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class InfoBarController : MonoBehaviour
    {

        [Header("Object Settings")]
        public Image BarFill;
        public float MinimumValueUI;
        public float MaximumValueUI;
        public float Duration = 2;

        [Header("Start Values")]
        public float StartPercentage;

        public const float MAX_PERCENTAGE = 100f;
        public const float MIN_PERCENTAGE = 0f;
        public float Percentage { get; private set; }

        float OldPercentage;
        Coroutine Changing;

        // Use this for initialization
        void OnEnable()
        {
            Percentage = 0;
            UpdateBarFill(Percentage);

            SetValue(StartPercentage);
        }

        public void SetValue (float newValue)
        {
            newValue = Mathf.Clamp(newValue, MIN_PERCENTAGE, MAX_PERCENTAGE);

            OldPercentage = Percentage;
            Percentage = newValue;

            if (Changing != null) StopCoroutine(Changing);
            UpdateBarFill(OldPercentage);
            Changing = StartCoroutine(ChangeValue(newValue, Duration));
        }

        IEnumerator ChangeValue(float newValue, float duration)
        {

            float startTime = Time.time;

            float multiplier = OldPercentage <= newValue ? 1 : -1;

            float change = Mathf.Abs(newValue - OldPercentage) / duration;

            while (Time.time - startTime <= duration)
            {
                float currentTime = Time.time - startTime;
                UpdateBarFill(OldPercentage + (multiplier * change * currentTime));

                yield return new WaitForEndOfFrame();
            }

            UpdateBarFill(newValue);
        }

        void UpdateBarFill(float percentage)
        {
            percentage = percentage / 100f;

            float barLength = MaximumValueUI - MinimumValueUI;
            float newLength = barLength * percentage;

            Vector2 max = BarFill.rectTransform.offsetMax;
            float xVal = MinimumValueUI + newLength;
            BarFill.rectTransform.offsetMax = new Vector2(-1 * xVal, max.y);
        }
    }
}
